using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MatchDll;
using ESRI.ArcGIS.Geometry;

namespace 基于空间模式匹配的地理场景自动识别系统
{
    class Matching
    {
        //
        public GraphStruct dataGraph = new GraphStruct();
        public GraphStruct pGraph = new GraphStruct();

        public List<GraphStruct> subGraphs = new List<GraphStruct>();

        private List<bool> visit = new List<bool>();

        private List<List<bool>> visit2 = new List<List<bool>>();

        //构造函数
        public Matching()
        {
            //初始化数据图G中所有节点的访问状态
            for (int i = 0; i < dataGraph.VexNum; i++)
            {
                this.visit.Add(false);

                AdjNode aNode = this.dataGraph.VexList[i].FirstAdj;
                List<bool> tempVisit = new List<bool>();

                while (aNode != null)
                {
                    tempVisit.Add(false);
                    aNode = aNode.Next;
                }
                this.visit2.Add(tempVisit);

            }
        }

        //构造函数
        public Matching(GraphStruct _dataGraph, GraphStruct _pGraph)
        {
            this.dataGraph = _dataGraph;
            this.pGraph = _pGraph;

            //初始化数据图G中所有节点的访问状态
            for (int i = 0; i < dataGraph.VexNum; i++)
            {
                this.visit.Add(false);

                AdjNode aNode = this.dataGraph.VexList[i].FirstAdj;
                List<bool> tempVisit = new List<bool>();

                while (aNode != null)
                {
                    tempVisit.Add(false);
                    aNode = aNode.Next;
                }
                this.visit2.Add(tempVisit);

            }
        }

        //初始化数据图G中所有节点的访问状态
        private void InitVisit()
        {
            for (int i = 0; i < this.dataGraph.VexNum; i++)
            {
                this.visit[i] = false;

                InitVisitNode(dataGraph.VexList[i]);
            }

        }

        //更新子图G中所有节点的访问状态
        private void InitVisit(GraphStruct subG)
        {
            for (int i = 0; i < subG.VexNum; i++)
            {
                int gId = -1;
                gId = subG.VexList[i].Data.PID;       
                this.visit[gId] = true;
            }
        }


        private void InitVisitNode(VexNode vex)
        {
            AdjNode aNode = vex.FirstAdj;

            int j = 0;

            while (aNode != null)
            {
                this.visit2[this.dataGraph.GetIndex(vex.Data)][j++] = false;
                aNode = aNode.Next;
            }
        }

        //结构匹配
        public void StruMatchGraph()
        {
            VexNode lastDataNode;
            VexNode curDataNode;

            VexNode patNode;

            for (int i = 0; i < dataGraph.VexNum; i++)
            {
                curDataNode = dataGraph.VexList[i];
                patNode = pGraph.VexList[0];

                //G中 开始就匹配失败
                if ((curDataNode.ENum < patNode.ENum))
                    continue;

                List<List<bool>> visitStatus = new List<List<bool>>();

                InitVisit(); this.visit[i] = true;

                visitStatus.Add(this.visit2[i]);

                //判断匹配状态
                bool flag = false;

                GraphStruct subGraph = new GraphStruct();

                subGraph.AddVex(curDataNode.Data);

                if (pGraph.VexNum == 1)
                {
                    //若语义匹配成功
                    if (SemanticMatch(subGraph))
                    {
                        GraphStruct tempGraph = new GraphStruct();
                        tempGraph.CopyGraph(subGraph);
                        subGraphs.Add(tempGraph);
                    }

                    continue;
                }

                for (int j = 1; j < pGraph.VexNum; j++)
                {
                    flag = false;

                    //模式图P 取下一个节点
                    patNode = pGraph.VexList[j];

                    //数据图G 取下一个节点
                    lastDataNode = curDataNode;
                    curDataNode = GetBroVexNode(lastDataNode, visitStatus[j - 1]);

                    while (curDataNode != null)
                    {
                        if (MatchVexNode(curDataNode, patNode, subGraph))
                        { flag = true; break; }

                        curDataNode = GetBroVexNode(lastDataNode, visitStatus[j - 1]);
                    }

                    if (flag)
                    {
                        visitStatus.Add(this.visit2[this.dataGraph.GetIndex(curDataNode.Data)]);
                        subGraph.AddVex(curDataNode.Data);
                        this.visit[this.dataGraph.GetIndex(curDataNode.Data)] = true;

                        AddNodeEdge(curDataNode, patNode, subGraph);

                        if (j == pGraph.VexNum - 1)
                        {
                            //若语义匹配成功
                            if (SemanticMatch(subGraph))
                            {
                                GraphStruct tempGraph = new GraphStruct();
                                tempGraph.CopyGraph(subGraph);
                                subGraphs.Add(tempGraph);
                            }

                            //删除匹配失败的备选节点 
                            visitStatus.RemoveAt(visitStatus.Count - 1);

                            subGraph.DeleteVex(curDataNode.Data);
                            this.visit[this.dataGraph.GetIndex(curDataNode.Data)] = false;

                            InitVisitNode(curDataNode);

                            //模式图P 回溯
                            j--;

                            //数据图G 回溯

                            int lastDataNodeId = 0;
                            lastDataNodeId = subGraph.VexList[subGraph.VexNum - 1].Data.PID;
                            lastDataNode = dataGraph.VexList[lastDataNodeId];
                            curDataNode = lastDataNode;
                        }

                    }
                    else//回溯
                    {
                        //用break回溯到最外层
                        if (j == 1)
                            break;

                        //删除匹配失败的备选节点 
                        visitStatus.RemoveAt(visitStatus.Count - 1);

                        subGraph.DeleteVex(lastDataNode.Data);
                        this.visit[this.dataGraph.GetIndex(lastDataNode.Data)] = false;

                        InitVisitNode(lastDataNode);

                        //模式图P 回溯
                        j -= 2;

                        //数据图G 回溯
                        int lastDataNodeId = -1;
                        lastDataNodeId = subGraph.VexList[subGraph.VexNum - 1].Data.PID;

                        lastDataNode = dataGraph.VexList[lastDataNodeId];
                        curDataNode = lastDataNode;
                    }

                }//for (int j = 0; j < pGraph.VexNum; j++)
            }

            this.UpdataSubgraph();
        }

        //更新子图中节点的信息（添加节点是否需要存储的记录，为下一步还原成矢量数据提供依据）
        private void UpdataSubgraph()
        {
            for (int i = 0; i < this.subGraphs.Count; i++)
            {
                GraphStruct tempGraph = subGraphs[i];

                for (int j = 0; j < tempGraph.VexNum; j++)
                {
                    tempGraph.VexList[j].Data.IsStore = this.pGraph.VexList[j].Data.IsStore;
                }
            }
        }

        private void NotVisit(VexNode lastNode, GraphStruct subG)
        {
            AdjNode broAdjNode = lastNode.FirstAdj;

            while (broAdjNode != null)
            {
                int brotherID = broAdjNode.AdjVexId;

                //若该节点在子图中
                if (!subG.ExistVex(this.dataGraph.VexList[brotherID].Data))
                    this.visit[brotherID] = false;

                broAdjNode = broAdjNode.Next;
            }
        }
       
        //获取lastNode的 一个未遍历过的 邻接节点
        private VexNode GetBroVexNode(VexNode lastNode, List<bool> visitAdj)
        {
            int lastNodeId = 0;

            lastNodeId = this.dataGraph.GetIndex(lastNode.Data);

            AdjNode broAdjNode = lastNode.FirstAdj;
            int j = 0;

            while (broAdjNode != null)
            {
                int brotherID = broAdjNode.AdjVexId;

                //若该节点未遍历过
                if ((!visit[brotherID]) && (!visitAdj[j]))
                {
                    visitAdj[j] = true;
                    return dataGraph.VexList[brotherID];
                }

                broAdjNode = broAdjNode.Next; j++;
            }
            return null;
        }

        //结构匹配两节点(度匹配 + 邻接关系匹配)
        private Boolean MatchVexNode(VexNode gNode, VexNode pNode, GraphStruct subG)
        {
            int gID = gNode.Data.PID;

            // visit[gID] = true;

            //度匹配
            if (gNode.ENum < pNode.ENum)
                return false;

            //邻接关系匹配
            AdjNode aNode = pNode.FirstAdj;
            while (aNode != null)
            {
                //跳过pNode与P中未匹配节点间的邻接关系
                if (aNode.AdjVexId >= subG.VexNum)
                {
                    aNode = aNode.Next;
                    continue;
                }

                //判断数据图G中是否有对应边
                if (!dataGraph.ExistEdge(gNode.Data, subG.VexList[aNode.AdjVexId].Data))
                    return false;
                aNode = aNode.Next;
            }

            return true;
        }

     
        //根据模式图P中的pNode，在子图subG中为gNode添加邻接边
        private void AddNodeEdge(VexNode gNode, VexNode pNode, GraphStruct subG)
        {
            ArgLine edgeValue;
            AdjNode aNode = pNode.FirstAdj;
            while (aNode != null)
            {
                //跳过pNode与P中未匹配节点间的邻接关系
                if (aNode.AdjVexId >= subG.VexNum)
                {
                    aNode = aNode.Next;
                    continue;
                }

                //获取数据图G中对应边的权值
                edgeValue = dataGraph.GetEdgeValue(gNode.Data, subG.VexList[aNode.AdjVexId].Data);

                subG.SetEgde(gNode.Data, subG.VexList[aNode.AdjVexId].Data, edgeValue);
                aNode = aNode.Next;
            }
        }
      
        //语义匹配某个子图
        private bool SemanticMatch(GraphStruct subG)
        {
            //直线河段匹配
            if (subG.VexNum == 1)
            {
                //研究区一
                ////记录较直的直线河段
                //if ((subG.VexList[0].Data.CurbS < pGraph.VexList[0].Data.ST) && (subG.VexList[0].Data.Length * 3 > pGraph.VexList[0].Data.LT))
                //    return true;

                //if ((subG.VexList[0].Data.CurbS < 1.05) && (subG.VexList[0].Data.Length > 2500))
                //    return true;

                //if ((subG.VexList[0].Data.CurbS < 1.065) && (subG.VexList[0].Data.Length > 3300))
                //    return true;

                //if ((subG.VexList[0].Data.CurbS < 1.081) && (subG.VexList[0].Data.Length > 4000))
                //    return true;

                //if ((subG.VexList[0].Data.CurbS < 1.091) && (subG.VexList[0].Data.Length > 6000))
                //    return true;

                ////记录较长的直线河段
                //if (subG.VexList[0].Data.Length > pGraph.VexList[0].Data.LT)
                //    return true;

                //研究区二
                //记录较直的直线河段
                if ((subG.VexList[0].Data.Curb < pGraph.VexList[0].Data.CT) && (subG.VexList[0].Data.Length * 3 > pGraph.VexList[0].Data.LT))
                    return true;

                return false;
            }

            for (int i = 0; i < subG.VexNum; i++)
            {
                //节点的语义匹配
                if ((pGraph.VexList[i].Data.NodeType != 0)&&(subG.VexList[i].Data.NodeType != pGraph.VexList[i].Data.NodeType) && (subG.VexList[i].Data.NodeType != 2))
                    return false;

                //邻接边的语义匹配
                if (!SeMatchNode(subG.VexList[i], pGraph.VexList[i]))
                    return false;
            }

            return true;
        }
      
        //语义匹配两节点的邻接边
        private bool SeMatchNode(VexNode gNode, VexNode pNode)
        {
            MatchDll.ArgLine gEdgeValue;
            MatchDll.ArgLine pEdgeValue;

            for (AdjNode gAdjNode = gNode.FirstAdj; gAdjNode != null; gAdjNode = gAdjNode.Next)
            {
                for (AdjNode pAdjNode = pNode.FirstAdj; pAdjNode != null; pAdjNode = pAdjNode.Next)
                {
                    //获取对应边
                    if (gAdjNode.AdjVexId == pAdjNode.AdjVexId)
                    {
                        gEdgeValue = TransReflection<ArgLine, MatchDll.ArgLine>(gAdjNode.EdgeValue);
                        pEdgeValue = TransReflection<ArgLine, MatchDll.ArgLine>(pAdjNode.EdgeValue);

                        //对应边的类型
                        if (gEdgeValue.LineType != pEdgeValue.LineType)
                            return false;

                        //对应边的角度值
                        if (gEdgeValue.MidAngle < pEdgeValue.MinAngle)
                            return false;
                        if (gEdgeValue.MidAngle > pEdgeValue.MaxAngle)
                            return false;

                        if (gEdgeValue.MidLength > pEdgeValue.MidLength)
                            return false;

                        break;
                    }
                }

            }
            return true;
        }

        private static Toutput TransReflection<Tinput, Toutput>(Tinput tIn)
        {
            Toutput tOut = Activator.CreateInstance<Toutput>();
            var tInType = tIn.GetType();

            foreach (var itemOut in tOut.GetType().GetProperties())
            {
                //var itemIn = tInType.GetProperty(itemOut.Name); 
                //if (itemIn != null)
                //{
                //    itemOut.SetValue(tOut, itemIn.GetValue(tIn),null);

                //}

                object value = GetPropertyValue(tIn, itemOut.Name);
                if (value != DBNull.Value)
                {
                    Type tempType = itemOut.PropertyType;
                    itemOut.SetValue(tOut, GetDataByType(value, tempType), null);

                }
            }
            return tOut;
        }

        public static object GetPropertyValue(object info, string field)
        {
            if (info == null) return null;
            Type t = info.GetType();
            IEnumerable<System.Reflection.PropertyInfo> property = from pi in t.GetProperties() where pi.Name.ToLower() == field.ToLower() select pi;
            return property.First().GetValue(info, null);
        }

        /// <summary>
        /// 将数据转为指定类型
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data1"></param>
        /// <returns></returns>
        public static object GetDataByType(object data1, Type itype, params object[] myparams)
        {
            object result = new object();
            try
            {
                if (itype == typeof(decimal))
                {
                    result = Convert.ToDecimal(data1);
                    if (myparams.Length > 0)
                    {
                        result = Convert.ToDecimal(Math.Round(Convert.ToDecimal(data1), Convert.ToInt32(myparams[0])));
                    }
                }
                else if (itype == typeof(double))
                {

                    if (myparams.Length > 0)
                    {
                        result = Convert.ToDouble(Math.Round(Convert.ToDouble(data1), Convert.ToInt32(myparams[0])));
                    }
                    else
                    {
                        result = double.Parse(Convert.ToDecimal(data1).ToString("0.00"));
                    }
                }
                else if (itype == typeof(Int32))
                {
                    result = Convert.ToInt32(data1);
                }
                else if (itype == typeof(DateTime))
                {
                    result = Convert.ToDateTime(data1);
                }
                else if (itype == typeof(Guid))
                {
                    result = new Guid(data1.ToString());
                }
                else if (itype == typeof(string))
                {
                    result = data1.ToString();
                }
                else if (itype == typeof(IPoint))
                {
                    result = data1 as IPoint;
                }
                else if (itype == typeof(bool))
                {
                    result = Convert.ToBoolean(data1);
                }
            }
            catch
            {
                if (itype == typeof(decimal))
                {
                    result = 0;
                }
                else if (itype == typeof(double))
                {
                    result = 0;
                }
                else if (itype == typeof(Int32))
                {
                    result = 0;
                }
                else if (itype == typeof(DateTime))
                {
                    result = null;
                }
                else if (itype == typeof(Guid))
                {
                    result = Guid.Empty;
                }
                else if (itype == typeof(string))
                {
                    result = "";
                }
            }
            return result;
        }
    }
}

