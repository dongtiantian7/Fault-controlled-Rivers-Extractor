using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESRI.ArcGIS.Geometry;

namespace 基于空间模式匹配的地理场景自动识别系统
{
    class get_ARG
    {
        //
        private double _lt;
        public double LT
        {
            get { return _lt; }
            set { _lt = value; }
        }

        private double _st;
        public double ST
        {
            get { return _st; }
            set { _st = value; }
        }

        //public List<List<int>> source_river = new List<List<int>>();

        public List<ArgPoint> ListArgPoint = new List<ArgPoint>();
        public List<ArgLine> ListArgLine = new List<ArgLine>();
        public GraphStruct Graph = new GraphStruct();

        public static List<List<ArgPoint>> RiverPoint = new List<List<ArgPoint>>();

        private int pNum = 0;
        private int lNum = 0;

        public List<List<IPoint>> Linelist;
        public List<String> FromId;
        public List<String> ToId;

        public get_ARG(double lt, double st, List<List<IPoint>> _linelist, List<String> _fromId, List<String> _toId)
        {
            this._lt = lt;
            this._st = st;
            this.Linelist = _linelist;
            this.FromId = _fromId;
            this.ToId = _toId;
        }

        private void Initialize()
        {
            Entity.source_river.Clear();

            for (int j = 0; j < Linelist.Count(); j++)
            {
                List<int> temp_lineid = new List<int>();

                Entity.source_river.Add(temp_lineid);

                List<IPoint> temp_riverpoint = new List<IPoint>();
                Entity.arg_point.Add(temp_riverpoint);

                List<ArgPoint> triverPoint = new List<ArgPoint>();
                RiverPoint.Add(triverPoint);
            }

            for (int i = 0; i < Linelist.Count(); i++)
            {
                //记录每条河流的源头河流序号
                // int trid = int.Parse(this.ToId[i]);
                int tiid = this.RiverId2Id(int.Parse(this.ToId[i]));

                if (tiid >= 0)
                    Entity.source_river[tiid].Add(i);
            }

        }


        //将河流的fid与from_id形成对应关系
        private int RiverId2Id(int key)
        {
            for (int i = 0; i < this.FromId.Count; i++)
            {
                if (int.Parse(this.FromId[i]) == key)
                    return i;
            }

            return -1;
        }

        //获取ARG图
        public void CreatARG()
        {
            //数据预处理
            Initialize();
            Graph.typeGraph = 'R';

            for (int i = 0; i < Linelist.Count(); i++)
            {
                //当前线中的折点个数
                int pointcount = Linelist[i].Count();

                double SumDis = util.sumdis_line(Linelist[i], 0, pointcount - 1);

                //如果河流过短，则不记录。
                if (SumDis < LT) continue;

                double S = SumDis / util.getDistanceOfTwoPoints(Linelist[i][0], Linelist[i][pointcount - 1]);

                int LM = (0 + pointcount - 1) / 2;

                //记录ARG点
                Entity.arg_point[i].Add(Linelist[i][LM]);

                ArgPoint argPoint1 = new ArgPoint(pNum, Linelist[i][LM], i, 0, pointcount - 1, SumDis, S);

                if (Entity.source_river[i].Count() == 0)
                    argPoint1.NodeType = 0;
                else
                    argPoint1.NodeType = 1;

                argPoint1.Direction.X = Linelist[i][pointcount - 1].X - Linelist[i][0].X;
                argPoint1.Direction.Y = Linelist[i][pointcount - 1].Y - Linelist[i][0].Y;

                ListArgPoint.Add(argPoint1);
                RiverPoint[i].Add(argPoint1);

                VexNode v = new VexNode(argPoint1);
                Graph.VexList.Add(v);

                pNum++;
            }//for

            //筛选掉过短的河段
            for (int i = 0; i < Linelist.Count(); i++)
            {
                //int trid = int.Parse(this.ToId[i]);
                int tiid = this.RiverId2Id(int.Parse(this.ToId[i]));

                //若河段过短
                if (Entity.arg_point[i].Count() == 0)
                {
                    if (tiid < 0)
                    {
                        Entity.source_river[i].Clear();
                        continue;
                    }
                    Entity.source_river[tiid].Remove(i);

                    for (int j = 0; j < Entity.source_river[i].Count(); j++)
                    {
                        Entity.source_river[tiid].Add(Entity.source_river[i][j]);
                        //int sid = source_river[i][j];
                        this.ToId[Entity.source_river[i][j]] = this.ToId[i];
                    }
                    Entity.source_river[i].Clear();
                }

            }
            //生成ARG边
            for (int i = 0; i < Linelist.Count(); i++)
            {
                //单条河流流向单条河流
                if (Entity.source_river[i].Count() == 1)
                {
                    int cid1 = Entity.source_river[i][0];
                    int cid2 = i;

                    //ArgLine argLine1 = getEdgeOfTwoPoints(RiverPoint[cid1][RiverPoint[cid1].Count - 1], RiverPoint[cid2][0]);
                    ArgLine argLine1 = getEdgeOfTwoRivers(cid1, cid2);
                    ListArgLine.Add(argLine1);

                    Graph.SetEgde(RiverPoint[cid1][0], RiverPoint[cid2][0], argLine1);
                    lNum++;
                }
                else if (Entity.source_river[i].Count() > 1)
                {
                    List<int> PointIds = new List<int>();

                    if (RiverPoint[i].Count == 0)
                        continue;

                    PointIds.Add(i);

                    //河流交汇处的ARG点集
                    for (int m = 0; m < Entity.source_river[i].Count; m++)
                    {

                        int upId = Entity.source_river[i][m];
                        if (RiverPoint[upId].Count == 0)
                            continue;
                        PointIds.Add(upId);
                    }

                    //生成河流交汇处的ARG边
                    for (int m = 0; m < PointIds.Count; m++)
                    {
                        for (int n = m + 1; n < PointIds.Count(); n++)
                        {
                            ArgLine argLine1 = getEdgeOfTwoRivers(PointIds[m], PointIds[n]);

                            ListArgLine.Add(argLine1);

                            Graph.SetEgde(RiverPoint[PointIds[m]][0], RiverPoint[PointIds[n]][0], argLine1);
                            lNum++;
                        }
                    }

                }//else
            }//for
        }

        private ArgLine getEdgeOfTwoRivers(int riverId1, int riverId2)
        {
            List<IPoint> Linetp = new List<IPoint>();
            Linetp.Add(Entity.arg_point[riverId1][0]);
            Linetp.Add(Entity.arg_point[riverId2][0]);

            int lType = -999;

            IPoint direction1 = new PointClass();

            IPoint direction2 = new PointClass();

            int Tid1 = this.RiverId2Id(int.Parse(this.ToId[riverId1]));
            int Tid2 = this.RiverId2Id(int.Parse(this.ToId[riverId2]));

            if (Tid1 == Tid2)
            {
                lType = 1;

                direction1 = util.getDirection(Linelist[riverId1], Linelist[riverId1].Count - 1, ST, LT, -1);
                direction2 = util.getDirection(Linelist[riverId2], Linelist[riverId2].Count - 1, ST, LT, -1);
            }
            else
            {
                lType = -1;

                if (Tid1 == riverId2)
                {
                    direction1 = util.getDirection(Linelist[riverId1], Linelist[riverId1].Count - 1, ST, LT, -1);
                    direction2 = util.getDirection(Linelist[riverId2], 0, ST, LT, 1);
                }
                else
                {
                    direction1 = util.getDirection(Linelist[riverId1], 0, ST, LT, 1);
                    direction2 = util.getDirection(Linelist[riverId2], Linelist[riverId2].Count - 1, ST, LT, -1);
                }
            }

            //计算夹角
            double a = util.getangle(direction1, direction2);

            // ArgLine argLine1 = new ArgLine(lNum, a, util.sumdis_line(Linelist[riverId1], LE1, LB2));
            ArgLine argLine1 = new ArgLine(lNum, a);
            argLine1.LineL = Linetp;
            argLine1.LineType = lType;
            //argLine1.MidLength = util.sumdis_line(Linelist[riverId1], LBf, LEf) + util.sumdis_line(Linelist[riverId2], p2.DPointId, Linelist[riverId2].Count - 1);

            return argLine1;
        }


    }
}
