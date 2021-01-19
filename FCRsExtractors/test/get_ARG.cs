using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESRI.ArcGIS.Geometry;

namespace test
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

        public  List<ArgPoint> ListArgPoint = new List<ArgPoint>();
        public  List<ArgLine> ListArgLine = new List<ArgLine>();
        public  GraphStruct Graph = new GraphStruct();

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

            //检查河流逆转
            //for (int i = 0; i < Linelist.Count(); i++)
            //{
            //    if (source_river[i].Count() < 1) continue;

            //    int cid1 = i;
            //    int cid2 = source_river[i][0];

            //    int flag = util.adjacent(Linelist[cid1], Linelist[cid2]);
            //    if (flag > 2)
            //    {
            //        Linelist[cid1] = reverse(Linelist[cid1]);
            //    }

            //    for (int j = 0; j < source_river[i].Count(); j++)
            //    {
            //        cid2 = source_river[i][j];

            //        flag = util.adjacent(Linelist[cid1], Linelist[cid2]);
            //        if ((flag == 0)||(flag == 2))
            //        {
            //            Linelist[cid2] = reverse(Linelist[cid2]);
            //        }
            //    }
            //}
        }


        //将河流的fid与from_id形成对应关系
        private int RiverId2Id( int key)
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

            for (int i = 0; i < Linelist.Count(); i++)
            {
                //当前线中的折点个数
                int pointcount = Linelist[i].Count();

                double SumDis = util.sumdis_line(Linelist[i], 0, pointcount-1);

                //如果河流过短，则不记录。
                if (SumDis < 2 * LT) continue;

                double S = SumDis / util.getDistanceOfTwoPoints(Linelist[i][0], Linelist[i][pointcount - 1]);

                int LM = -1; 

                //当前河段若为直线河段
                if (S < _st)
                {
                    LM = (0 + pointcount - 1) / 2;

                    //记录ARG点
                    Entity.arg_point[i].Add(Linelist[i][LM]);
                    
                    //Entity.priver_id.Add(i);
                    //Entity.bpoint_id.Add(0);
                    //Entity.sstr_length.Add(sum_dis);

                    //Entity.sstr_curb.Add(S);

                    //int c = 
                    ArgPoint argPoint1 = new ArgPoint(pNum, Linelist[i][LM], i, 0, pointcount - 1, SumDis, S);
                   // argPoint1.NodeType = 0;
                    argPoint1.NodeType = 11;
                    argPoint1.Direction.X = Linelist[i][pointcount - 1].X - Linelist[i][0].X;
                    argPoint1.Direction.Y = Linelist[i][pointcount - 1].Y - Linelist[i][0].Y;

                    ListArgPoint.Add(argPoint1);
                    RiverPoint[i].Add(argPoint1);

                    VexNode v = new VexNode(argPoint1);
                    Graph.VexList.Add(v);

                    pNum++;
                }
                else//当前河段若发生转弯
                {
                    int bArgPointId = pNum ;
                    
                    //生成ARG点
                    getCurbArgPoint(i, 0, pointcount - 1);

                    //若没有生成新节点
                    if (ListArgPoint.Count == bArgPointId) continue;

                    //点的类型
                    ListArgPoint[bArgPointId].NodeType = -1;
                    RiverPoint[i][0].NodeType = -1;

                    int dArgPointId = pNum - 1;

                    VexNode v1 = new VexNode(ListArgPoint[bArgPointId]);
                    Graph.VexList.Add(v1);

                    for (int j = bArgPointId + 1; j <= dArgPointId; j++)
                    { 
                        //点的类型
                        ListArgPoint[j].NodeType = 0;
                        RiverPoint[i][j - bArgPointId].NodeType = 0;

                        VexNode v = new VexNode(ListArgPoint[j]);
                        Graph.VexList.Add(v);

                        //生成 河流内部边
                       //getEdgeOfTwoPoints(j-1, j);

                        ArgLine argLine1 = getEdgeOfTwoPoints(j - 1, j);

                        ListArgLine.Add(argLine1);

                        Graph.SetEgde(ListArgPoint[j - 1], ListArgPoint[j], argLine1);
                        lNum++;
                    }

                    //点的类型
                    ListArgPoint[dArgPointId].NodeType = 1;
                    RiverPoint[i][RiverPoint[i].Count-1].NodeType = 1;

                }//else当前河段若发生转弯

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
            //生成ARG边（外部边）
            for (int i = 0; i < Linelist.Count(); i++)
            {
                //pcount += Entity.arg_point[i].Count();
                //单条河流流向单条河流
                if (Entity.source_river[i].Count() == 1)
                {
                    int cid1 = Entity.source_river[i][0];
                    int cid2 = i;

                    ArgLine argLine1 = getEdgeOfTwoPoints(RiverPoint[cid1][RiverPoint[cid1].Count - 1], RiverPoint[cid2][0]);

                    ListArgLine.Add(argLine1);

                    Graph.SetEgde(RiverPoint[cid1][RiverPoint[cid1].Count - 1], RiverPoint[cid2][0], argLine1);
                    lNum++;  
                }
                else if (Entity.source_river[i].Count() > 1)
                {
                    List<ArgPoint> Points = new List<ArgPoint>();

                    if (RiverPoint[i].Count == 0)
                        continue;
                    Points.Add(RiverPoint[i][0]);

                     //河流交汇处的ARG点集
                    for (int m = 0; m < Entity.source_river[i].Count; m++)
                    {

                        int upId = Entity.source_river[i][m];
                        int upCount = RiverPoint[upId].Count;
                        if (upCount == 0)
                            continue;
                        Points.Add(RiverPoint[upId][upCount-1]);
                    }

                    //生成河流交汇处的ARG边
                    for (int m = 0; m < Points.Count; m++)
                    {
                        for (int n = m + 1; n < Points.Count(); n++)
                        {
                            ArgLine argLine1 = getEdgeOfTwoPoints(Points[m], Points[n]);

                            ListArgLine.Add(argLine1);

                            Graph.SetEgde(Points[m], Points[n], argLine1);
                            lNum++;
                        }
                    }
                    
                }//else
            }//for
          }
  
        ////找到最近邻接河段
        //public static int mindisriver(List<List<IPoint>> source, List<int> sourceid, int baseid, int cidl, double ST)
        //{
        //    int nearid = -1;

        //    int minangle = 200;

        //    for (int i = 0; i < sourceid.Count(); i++)
        //    {
        //        int cidr = sourceid[i];
        //        if (cidr == baseid || cidr == cidl) continue;

        //        double a = util.neariver_ang(source[cidr], source[baseid], ST);

        //        if (a < minangle)
        //            nearid = cidr;
        //    }

        //    return nearid;
        //}

        ////连接指定两条边
        //public static void getConline(List<List<IPoint>> Linelist, int cid1, int cid2, double ST)
        //{
        //    List<IPoint> Linecon = new List<IPoint>();

        //    int LBf = -1, LEf = -1, LMf = -1, LBt = -1, LEt = -1, LMt = -1;
         
        //    //当前线中的折点个数
        //    int pcount1 = Linelist[cid1].Count();
        //    int pcount2 = Linelist[cid2].Count();

        //    switch (util.adjacent(Linelist[cid1], Linelist[cid2]))
        //    {
        //        case 1:
        //            {
        //                LBf = 0;
        //                LEf = util.minstra(Linelist[cid1], LBf, ST, 1);

        //                LBt = 0;
        //                LEt = util.minstra(Linelist[cid2], LBt, ST, 1);
        //            } break;
        //        case 2:
        //            {
        //                LBf = 0;
        //                LEf = util.minstra(Linelist[cid1], LBf, ST, 1);

        //                LBt = pcount2 - 1;
        //                LEt = util.minstra(Linelist[cid2], LBt, ST, -1);
        //            } break;
        //        case 3:
        //            {
        //                LBf = pcount1 - 1;
        //                LEf = util.minstra(Linelist[cid1], LBf, ST, -1);

        //                LBt = 0;
        //                LEt = util.minstra(Linelist[cid2], LBt, ST, 1);
        //            }
        //            break;
        //        case 4:
        //            {
        //                LBf = pcount1 - 1;
        //                LEf = util.minstra(Linelist[cid1], LBf, ST, -1);

        //                LBt = pcount2 - 1;
        //                LEt = util.minstra(Linelist[cid2], LBt, ST, -1);
        //            } break;
        //    }//switch

        //    LMf = (LBf + LEf) / 2;
        //    LMt = (LBt + LEt) / 2;
        //    double a = util.neariver_ang(Linelist[cid1], Linelist[cid2], ST);

        //    Linecon.Add(Linelist[cid1][LMf]);
        //    Linecon.Add(Linelist[cid2][LMt]);

        //    arg_line.Add(Linecon);
        //    friver_id.Add(cid1);
        //    triver_id.Add(cid2);
        //    turn_angle.Add(a);

        //    //line_type.Add("邻接");
        //}

        ///// <summary>
        ///// 根据ARG点坐标获取点ID
        ///// </summary>
        ///// <param name="po"></param>
        ///// <param name="arg_point"></param>
        ///// <returns></returns>
        //public static int ArgPointID(IPoint po, List<List<IPoint>> arg_point)
        //{
        //    int id = -1;

        //    for (int i = 0; i < arg_point.Count; i++)
        //    {
        //        for (int j = 0; j < arg_point[i].Count; j++)
        //        {
        //            if (Math.Abs(arg_point[i][j].X - po.X) < 0.001)
        //                return id;

        //            id++;
        //        }
        //    }
                

        //    return id;
        //}

        ///// <summary>
        ///// 获得指定的两条河段间的边
        ///// </summary>
        ///// <param name="arg_point"></param>
        ///// <param name="cid1"></param>
        ///// <param name="cid2"></param>
        ///// <returns></returns>
        //public static List<IPoint> getConline( int cid1, int cid2)
        //{
        //    List<IPoint> Linecon = new List<IPoint>();

        //    //当前线中的折点个数
        //    int pcount1 = Entity.arg_point[cid1].Count();
        //    int pcount2 = Entity.arg_point[cid2].Count();

        //    switch (util.adjacent(Linelist[cid1], Linelist[cid2]))
        //    {
        //        case 1:
        //            {
        //                Linecon.Add(Entity.arg_point[cid1][0]);
        //                Linecon.Add(Entity.arg_point[cid2][0]);
        //            } break;
        //        case 2:
        //            {
        //                Linecon.Add(Entity.arg_point[cid1][0]);
        //                Linecon.Add(Entity.arg_point[cid2][pcount2 - 1]);
        //            } break;
        //        case 3:
        //            {
        //                Linecon.Add(Entity.arg_point[cid1][pcount1 - 1]);
        //                Linecon.Add(Entity.arg_point[cid2][0]);
        //            }
        //            break;
        //        case 4:
        //            {
        //                Linecon.Add(Entity.arg_point[cid1][pcount1 - 1]);
        //                Linecon.Add(Entity.arg_point[cid2][pcount2 - 1]);
        //            } break;
        //    }//switch

        //    return Linecon;
        //}

    //    //连接指定的三条边得到三角形
    //    public static List<IPoint> getTriline(List<List<IPoint>> Linelist, List<List<IPoint>> arg_point, int cid1, int cid2, int cid, double ST)
    //    {
    //        List<IPoint> Linetri = new List<IPoint>();

    //        List<IPoint> line1 = new List<IPoint>();
    //        line1 = getConline(Linelist, Entity.arg_point, cid, cid1);

    //        List<IPoint> line2 = new List<IPoint>();
    //        line2 = getConline(Linelist, Entity.arg_point, cid1, cid2);
             
    //        Linetri.Add(line1[0]);
    //        Linetri.Add(line1[1]);
    //        Linetri.Add(line2[1]);
    //        Linetri.Add(line1[0]);

    //        return Linetri;
    //    }

        /// <summary>
        /// 获取指定曲线中的转折点ID
        /// </summary>
        /// <param name="bid"></param>
        /// <param name="did"></param>
        /// <param name="line"></param>
        /// <returns></returns>
        private int getFastPoint(int bid,int did,List<IPoint> line)
        {
            int MaxId = -1;
            double MaxDis = 0;

            double a = line[bid].Y - line[did].Y;
            double b = line[did].X - line[bid].X;
            double c = line[bid].X * line[did].Y - line[did].X * line[bid].Y;

            for (int i = bid + 1; i < did; i++)
            {
                double dis = Math.Abs(a * line[i].X + b * line[i].Y + c) / Math.Sqrt(a * a + b * b);

                if (dis > MaxDis)
                {
                    MaxId = i;
                    MaxDis = dis;
                }
                    
            }

            return MaxId;
        }

        //生成弯曲河流上的ARG点
        private void getCurbArgPoint(int RiverId, int bid, int did)
        {
            double SumDis = util.sumdis_line(Linelist[RiverId], bid, did);
            
            //若河段过短，则不记录。
            if (SumDis < 2 * _lt) return;

            double S = SumDis / util.getDistanceOfTwoPoints(Linelist[RiverId][bid], Linelist[RiverId][did]);

            //若河段已经是近似直线河段，直接生成ARG点
            if (S < ST)
            {
                int LM = (bid + did) / 2;

                //记录ARG点
                Entity.arg_point[RiverId].Add(Linelist[RiverId][LM]);

                ArgPoint argPoint1 = new ArgPoint(pNum, Linelist[RiverId][LM], RiverId, bid, did, SumDis, S);
                argPoint1.Direction.X = Linelist[RiverId][did].X - Linelist[RiverId][bid].X;
                argPoint1.Direction.Y = Linelist[RiverId][did].Y - Linelist[RiverId][bid].Y;

                ListArgPoint.Add(argPoint1);
                RiverPoint[RiverId].Add(argPoint1);
                pNum++;
            }
            else
            {
                int Mid = getFastPoint(bid, did, Linelist[RiverId]);
                getCurbArgPoint(RiverId, bid, Mid);

                getCurbArgPoint(RiverId, Mid, did);
            }

        }

        //生成指定两ARG点间的边
        private ArgLine getEdgeOfTwoPoints(int bid, int did)
        {
            ArgPoint p1 = ListArgPoint[bid];
            ArgPoint p2 = ListArgPoint[did];
            return this.getEdgeOfTwoPoints(p1, p2);

           // List<IPoint> Linetp = new List<IPoint>();
           // Linetp.Add(ListArgPoint[bid].PointL);
           // Linetp.Add(ListArgPoint[did].PointL);

           // int riverId1 = ListArgPoint[bid].RiverID;          
           // int riverId2 = ListArgPoint[did].RiverID;

           // //计算夹角
           // double a = util.getangle(ListArgPoint[bid].Direction, ListArgPoint[did].Direction);

           //// ArgLine argLine1 = new ArgLine(lNum, a, util.sumdis_line(Linelist[riverId1], LE1, LB2));
           // ArgLine argLine1 = new ArgLine(lNum, a);
           // argLine1.LineL = Linetp;

           // if (riverId1 == riverId2)
           // {
           //     argLine1.LineType = 0;
           // }//若其中某条河流流出流域范围
           // else if ((this.RiverId2Id(riverId1) < 0) || this.RiverId2Id(riverId2) < 0)
           // {
           //     argLine1.LineType = -1;
           // }
           // else
           // {
           //     int Tid1 = int.Parse(this.ToId[this.RiverId2Id(riverId1)]);
           //     int Tid2 = int.Parse(this.ToId[this.RiverId2Id(riverId2)]);

           //     if (Tid1 == Tid2)
           //         argLine1.LineType = 1;
           //     else
           //         argLine1.LineType = -1;
           // }

           // return argLine1;
        }

        //生成指定两ARG点间的边
        private ArgLine getEdgeOfTwoPoints(ArgPoint p1, ArgPoint p2)
        {
            List<IPoint> Linetp = new List<IPoint>();
            Linetp.Add(p1.PointL);
            Linetp.Add(p2.PointL);

            int riverId1 = p1.RiverID;
            int riverId2 = p2.RiverID;

            //计算夹角
            double a = util.getangle(p1.Direction, p2.Direction);

            // ArgLine argLine1 = new ArgLine(lNum, a, util.sumdis_line(Linelist[riverId1], LE1, LB2));
            ArgLine argLine1 = new ArgLine(lNum, a);
            argLine1.LineL = Linetp;

            if (riverId1 == riverId2)
            {
                argLine1.LineType = 0;
                
                argLine1.MidLine = util.PartLine(Linelist[riverId1], p1.DPointId, p2.BPointId);
                argLine1.MidLength = util.sumdis_line(Linelist[riverId1], p1.DPointId, p2.BPointId);
            }//若其中某条河流流出流域范围
            else
            {
                int Tid1 = this.RiverId2Id(int.Parse(this.ToId[riverId1]));
                int Tid2 = this.RiverId2Id(int.Parse(this.ToId[riverId2]));

                if (Tid1 == Tid2)
                {
                    argLine1.LineType = 1;
                    argLine1.MidLength = util.sumdis_line(Linelist[riverId1], p1.DPointId, Linelist[riverId1].Count - 1) + util.sumdis_line(Linelist[riverId2], p2.DPointId, Linelist[riverId2].Count - 1);
                }
                else
                {
                    argLine1.LineType = -1;
                    if(Tid1 == riverId2)
                        argLine1.MidLength = util.sumdis_line(Linelist[riverId1], p1.DPointId, Linelist[riverId1].Count - 1) 
                                            + util.sumdis_line(Linelist[riverId2], 0,p2.BPointId)
                                            + util.getDistanceOfTwoPoints(Linelist[riverId1][Linelist[riverId1].Count -1], Linelist[riverId2][0]);
                    else
                        argLine1.MidLength = util.sumdis_line(Linelist[riverId1],0, p1.BPointId)
                                           + util.sumdis_line(Linelist[riverId2], p2.DPointId, Linelist[riverId2].Count - 1)
                                           + util.getDistanceOfTwoPoints(Linelist[riverId1][0], Linelist[riverId2][Linelist[riverId2].Count - 1]);
                   
                }
                    
            }

            return argLine1;
        }

        //河流逆转
        private List<IPoint> reverse(List<IPoint> line)
        {
            List<IPoint> temp = new List<IPoint>();

            for (int i = line.Count-1; i >= 0; i--)
            {
                temp.Add(line[i]); 
            }
            return temp;
        }

    }
}

