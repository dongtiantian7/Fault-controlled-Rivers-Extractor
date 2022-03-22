using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace 基于空间模式匹配的地理场景自动识别系统
{
    //邻接表 存储图结构
    class GraphStruct
    {
      
        private List<VexNode> _vexList;
        public List<VexNode> VexList
        {
            get { return _vexList; }
            set { _vexList = value; }
        }

        private int _vexNum;
        public int VexNum
        {
            get { return _vexList.Count; }
            set { _vexNum = value; }
        }


        private int _edgeNum;
        public int EdgeNum
        {
            get { return _edgeNum; }
            set { _edgeNum = value; }
        }

        private int _existRiver;
        public int ExistRiver
        {
            get { return _existRiver; }
            set { _existRiver = value; }
        }

        //构造函数
        public GraphStruct()
        {
            _vexList = new List<VexNode>();
            _vexNum = _vexList.Count;

        }

        //构造函数
        public GraphStruct(List<VexNode> vexList)
        {
            _vexList = vexList;
            _vexNum = _vexList.Count;
        }

        //
        public void CopyGraph(GraphStruct graph)
        {
            for (int i = 0; i < graph.VexNum; i++)
            {
                this.AddVex(graph.VexList[i].Data);
            }

            for (int i = 0; i < graph.VexNum; i++)
            {
                AdjNode aNode = graph.VexList[i].FirstAdj;

                while (aNode != null)
                {
                    int adjId = aNode.AdjVexId;

                    this.SetEgde(this.VexList[i].Data, this.VexList[adjId].Data, aNode.EdgeValue);

                    aNode = aNode.Next;
                }
            }
        }

       
        public int GetIndex(ArgPoint vex1)
        {
            //原本是为了方便模式的xml还原成graph，现在被数据图用上了啊啊啊
            //if (vex1.PointL == null)
            //    return vex1.PID;

            for (int i = 0; i < this.VexNum; i++)
            {
                if(this._vexList[i].Data.PID == vex1.PID)
                    return i;

                //if (util.getDistanceOfTwoPoints(this._vexList[i].Data.PointL, vex1.PointL) < 0.1)
                //    return i;
            }

            return -1;
        }

        //添加点
        public void AddVex(ArgPoint vex)
        {
            //if (vex.PID >= 0)
            //    vex.PID = this._vexNum;

            VexNode vNode = new VexNode(vex);
            this._vexList.Add(vNode);
            this._vexNum++;
        }

        //删除vex节点
        public void DeleteVex(ArgPoint vex)
        {
            int vexId = this.GetIndex(vex);
            AdjNode aNode = this._vexList[vexId].FirstAdj;

            while (aNode != null)
            {
                //删除vex有关的邻接边
                this.DeleteEdge(this._vexList[aNode.AdjVexId].Data, vexId);

                aNode = aNode.Next;
            }

            this._vexList.Remove(this._vexList[GetIndex(vex)]);
            this._vexNum--;
        }


        public bool ExistVex(ArgPoint vex1)
        {
            if (this.GetIndex(vex1) < 0)
                return false;
            else
                return true;
        }
        
        public bool ExistEdge(ArgPoint vex1, ArgPoint vex2)
        {
            int nnn = this.GetIndex(vex1);

            AdjNode aNode = this._vexList[this.GetIndex(vex1)].FirstAdj;
            while (aNode != null)
            {
                if (aNode.AdjVexId == this.GetIndex(vex2))
                    return true;
                aNode = aNode.Next;
            }

            return false;
        }

        //删除vex1中与vexId2的邻接边
        private bool DeleteEdgeOfTwoPoint(ArgPoint vex1, int vexId2)
        {
            int vexId1 = this.GetIndex(vex1);

            AdjNode aNode = this._vexList[vexId1].FirstAdj;

            //若删除第一个节点
            if (aNode.AdjVexId == vexId2)
            {
                this._vexList[vexId1].FirstAdj = aNode.Next;
                this._vexNum--;

                return true;
            }

            while (aNode != null)
            {
                if (aNode.Next.AdjVexId == vexId2)
                {
                    aNode.Next = aNode.Next.Next;
                    this._vexNum--;

                    return true;
                }

                aNode = aNode.Next;
            }

            return false;
        }

        public bool DeleteEdge(ArgPoint vex1, int vexId2)
        {
            bool flag1 = this.DeleteEdgeOfTwoPoint(vex1, vexId2);

            int vexId1 = this.GetIndex(vex1);

            bool flag2 = this.DeleteEdgeOfTwoPoint(this._vexList[vexId2].Data, vexId1);

            if (flag1 && flag2)
                return true;

            return false;
        }

        public ArgLine GetEdgeValue(ArgPoint vex1, ArgPoint vex2)
        {
            AdjNode aNode = this._vexList[this.GetIndex(vex1)].FirstAdj;
            while (aNode != null)
            {
                if (aNode.AdjVexId == this.GetIndex(vex2))
                    return aNode.EdgeValue;

                aNode = aNode.Next;
            }

            return null;
        }

        //添加边
        public void SetEgde(ArgPoint vex1, ArgPoint vex2, ArgLine ALine)
        {
            if (this.ExistEdge(vex1, vex2))
                return;

            AdjNode tempE1 = new AdjNode(GetIndex(vex2), ALine);
            if (_vexList[GetIndex(vex1)].FirstAdj == null)
            {
                _vexList[GetIndex(vex1)].FirstAdj = tempE1;
            }
            else
            {
                tempE1.Next = _vexList[GetIndex(vex1)].FirstAdj;
                _vexList[GetIndex(vex1)].FirstAdj = tempE1;
            }
            _vexList[GetIndex(vex1)].ENum++;

            AdjNode tempE2 = new AdjNode(GetIndex(vex1), ALine);
            if (_vexList[GetIndex(vex2)].FirstAdj == null)
            {
                _vexList[GetIndex(vex2)].FirstAdj = tempE2;
            }
            else
            {
                tempE2.Next = _vexList[GetIndex(vex2)].FirstAdj;
                _vexList[GetIndex(vex2)].FirstAdj = tempE2;
            }
            _vexList[GetIndex(vex2)].ENum++;

            _edgeNum++;
        }

        
      
    }
}