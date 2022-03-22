using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;


namespace 基于空间模式匹配的地理场景自动识别系统
{
    class XmlUtil
    {

        //生成模式的Xml文件
        public static void PgenerateXmlFile(string saveFilePath, GraphStruct PatternGraph)
        {
            XmlDocument myXmlDoc = new XmlDocument();
            XmlDeclaration xmldecl = myXmlDoc.CreateXmlDeclaration("1.0", "utf-8", null);
            XmlElement rootElement = myXmlDoc.CreateElement("RiverPattern");
            myXmlDoc.AppendChild(rootElement);

            XmlElement vNum = myXmlDoc.CreateElement("VexNodeNum");
            vNum.InnerText = PatternGraph.VexNum.ToString();
            rootElement.AppendChild(vNum);
            XmlElement eNum = myXmlDoc.CreateElement("EdgeNum");
            eNum.InnerText = PatternGraph.EdgeNum.ToString();
            rootElement.AppendChild(eNum);

            XmlElement VerLists = myXmlDoc.CreateElement("VexNodes");
            for (int i = 0; i < PatternGraph.VexNum; i++)
            {
                XmlElement vex = myXmlDoc.CreateElement("VexNode");
                vex.SetAttribute("id", i.ToString());

                XmlElement nType = myXmlDoc.CreateElement("NodeType");
                nType.InnerText = PatternGraph.VexList[i].Data.NodeType.ToString();
                vex.AppendChild(nType);
                XmlElement nCt = myXmlDoc.CreateElement("CT");
                nCt.InnerText = PatternGraph.VexList[i].Data.CT.ToString();
                vex.AppendChild(nCt);
                XmlElement nLt = myXmlDoc.CreateElement("LT");
                nLt.InnerText = PatternGraph.VexList[i].Data.LT.ToString();
                vex.AppendChild(nLt);
                XmlElement nIsStore = myXmlDoc.CreateElement("IsStore");
                nIsStore.InnerText = PatternGraph.VexList[i].Data.IsStore;
                vex.AppendChild(nIsStore);

                if (PatternGraph.VexList[i].FirstAdj != null)
                {
                    XmlElement EdgeLists = myXmlDoc.CreateElement("Edges");
                    AdjNode aNode = PatternGraph.VexList[i].FirstAdj;

                    while (aNode != null)
                    {
                        XmlElement edge = myXmlDoc.CreateElement("Edge");
                        edge.SetAttribute("adjVex", aNode.AdjVexId.ToString());

                        XmlElement minA = myXmlDoc.CreateElement("MinAngle");
                        minA.InnerText = aNode.EdgeValue.MinAngle.ToString();
                        edge.AppendChild(minA);
                        XmlElement maxA = myXmlDoc.CreateElement("MaxAngle");
                        maxA.InnerText = aNode.EdgeValue.MaxAngle.ToString();
                        edge.AppendChild(maxA);
                        XmlElement eType = myXmlDoc.CreateElement("EdgeType");
                        eType.InnerText = aNode.EdgeValue.LineType.ToString();
                        edge.AppendChild(eType);
                        XmlElement midL = myXmlDoc.CreateElement("MidLength");
                        midL.InnerText = aNode.EdgeValue.MidLength.ToString();
                        edge.AppendChild(midL);

                        EdgeLists.AppendChild(edge);
                        aNode = aNode.Next;
                    }

                    vex.AppendChild(EdgeLists);
                }


                VerLists.AppendChild(vex);
            }
            rootElement.AppendChild(VerLists);

            myXmlDoc.Save(saveFilePath);
        }

        //生成数据图的Xml文件
        public static void DgenerateXmlFile(string saveFilePath, GraphStruct DataGraph)
        {
            XmlDocument myXmlDoc = new XmlDocument();
            XmlDeclaration xmldecl = myXmlDoc.CreateXmlDeclaration("1.0", "utf-8", null);
            XmlElement rootElement = myXmlDoc.CreateElement("DataModel");
            myXmlDoc.AppendChild(rootElement);

            XmlElement vNum = myXmlDoc.CreateElement("VexNodeNum");
            vNum.InnerText = DataGraph.VexNum.ToString();
            rootElement.AppendChild(vNum);
            XmlElement eNum = myXmlDoc.CreateElement("EdgeNum");
            eNum.InnerText = DataGraph.EdgeNum.ToString();
            rootElement.AppendChild(eNum);

            XmlElement VerLists = myXmlDoc.CreateElement("VexNodes");
            for (int i = 0; i < DataGraph.VexNum; i++)
            {
                XmlElement vex = myXmlDoc.CreateElement("VexNode");
                vex.SetAttribute("id", i.ToString());

                XmlElement nType = myXmlDoc.CreateElement("NodeType");
                nType.InnerText = DataGraph.VexList[i].Data.NodeType.ToString();
                vex.AppendChild(nType);
                XmlElement nCurb = myXmlDoc.CreateElement("Curb");
                nCurb.InnerText = DataGraph.VexList[i].Data.Curb.ToString();
                vex.AppendChild(nCurb);
                XmlElement nLength = myXmlDoc.CreateElement("Length");
                nLength.InnerText = DataGraph.VexList[i].Data.Length.ToString();
                vex.AppendChild(nLength);


                XmlElement nEnum = myXmlDoc.CreateElement("AdjEdgeCount");
                nEnum.InnerText = DataGraph.VexList[i].ENum.ToString();
                vex.AppendChild(nEnum);
                XmlElement nRiverID = myXmlDoc.CreateElement("RiverID");
                nRiverID.InnerText = DataGraph.VexList[i].Data.RiverID.ToString();
                vex.AppendChild(nRiverID);
                XmlElement nBPointId = myXmlDoc.CreateElement("BPointId");
                nBPointId.InnerText = DataGraph.VexList[i].Data.BPointId.ToString();
                vex.AppendChild(nBPointId);
                XmlElement nDPointId = myXmlDoc.CreateElement("DPointId");
                nDPointId.InnerText = DataGraph.VexList[i].Data.DPointId.ToString();
                vex.AppendChild(nDPointId);
               

                if (DataGraph.VexList[i].FirstAdj != null)
                {
                    XmlElement EdgeLists = myXmlDoc.CreateElement("Edges");
                    AdjNode aNode = DataGraph.VexList[i].FirstAdj;

                    while (aNode != null)
                    {
                        XmlElement edge = myXmlDoc.CreateElement("Edge");
                        edge.SetAttribute("adjVex", aNode.AdjVexId.ToString());

                        XmlElement midA = myXmlDoc.CreateElement("MidAngle");
                        midA.InnerText = aNode.EdgeValue.MidAngle.ToString();
                        edge.AppendChild(midA);
                        XmlElement eType = myXmlDoc.CreateElement("EdgeType");
                        eType.InnerText = aNode.EdgeValue.LineType.ToString();
                        edge.AppendChild(eType);
                        XmlElement midL = myXmlDoc.CreateElement("MidLength");
                        midL.InnerText = aNode.EdgeValue.MidLength.ToString();
                        edge.AppendChild(midL);

                        XmlElement midLineBid = myXmlDoc.CreateElement("MidLineBid");
                        midLineBid.InnerText = aNode.EdgeValue.MidLineBid.ToString();
                        edge.AppendChild(midLineBid);
                        XmlElement midLineDid = myXmlDoc.CreateElement("MidLineDid");
                        midLineDid.InnerText = aNode.EdgeValue.MidLineDid.ToString();
                        edge.AppendChild(midLineDid);

                        EdgeLists.AppendChild(edge);
                        aNode = aNode.Next;
                    }

                    vex.AppendChild(EdgeLists);
                }


                VerLists.AppendChild(vex);
            }
            rootElement.AppendChild(VerLists);
            myXmlDoc.Save(saveFilePath);
        }


        //读取模式Xml文件
        public static GraphStruct PreadXmlFile(string openFilePath)
        {
            GraphStruct pGraph = new GraphStruct();

            XmlDocument myXmlDoc = new XmlDocument();
            myXmlDoc.Load(openFilePath);

            XmlNode rootNode = myXmlDoc.SelectSingleNode("RiverPattern");

            int vNum = int.Parse(rootNode["VexNodeNum"].InnerText);
            int eNum = int.Parse(rootNode["EdgeNum"].InnerText);

            XmlNodeList VerLists = rootNode["VexNodes"].ChildNodes;
            foreach (XmlNode item in VerLists)
            {
                ArgPoint p = new ArgPoint();
                p.PID = int.Parse(item.Attributes["id"].Value);
                p.NodeType = int.Parse(item["NodeType"].InnerText);
                p.IsStore  = item["IsStore"].InnerText;
                p.CT = double.Parse(item["CT"].InnerText);
                p.LT = double.Parse(item["LT"].InnerText);

                //添加节点
                pGraph.AddVex(p);

                if (eNum == 0)
                    continue;

                //添加节点的邻接边
                XmlNodeList EdgeLists = item["Edges"].ChildNodes;
                foreach (XmlNode Edge in EdgeLists)
                {
                    int adjVexId = int.Parse(Edge.Attributes["adjVex"].Value);

                    if (adjVexId > pGraph.VexNum - 1)
                        continue;

                    double minA = double.Parse(Edge["MinAngle"].InnerText);
                    double maxA = double.Parse(Edge["MaxAngle"].InnerText);
                    int eType = int.Parse(Edge["EdgeType"].InnerText);
                    double midL = double.Parse(Edge["MidLength"].InnerText);
                    ArgLine aLine = new ArgLine(minA, maxA, eType);
                    aLine.MidLength = midL;

                    pGraph.SetEgde(p, pGraph.VexList[adjVexId].Data, aLine);

                }

            }

            return pGraph;
        }


        //读取数据图的Xml文件
        public static GraphStruct DreadXmlFile(string openFilePath)
        {
            GraphStruct dataGraph = new GraphStruct();

            XmlDocument myXmlDoc = new XmlDocument();
            myXmlDoc.Load(openFilePath);

            XmlNode rootNode = myXmlDoc.SelectSingleNode("DataModel");

            XmlNodeList VerLists = rootNode["VexNodes"].ChildNodes;
            foreach (XmlNode item in VerLists)
            {
                ArgPoint p = new ArgPoint();
                p.PID = int.Parse(item.Attributes["id"].Value);
                p.NodeType = int.Parse(item["NodeType"].InnerText);
                p.Curb = double.Parse(item["Curb"].InnerText);
                p.RiverID = int.Parse(item["RiverID"].InnerText);
                p.BPointId = int.Parse(item["BPointId"].InnerText);
                p.DPointId = int.Parse(item["DPointId"].InnerText);
                p.Length = double.Parse(item["Length"].InnerText);

                //添加节点
                dataGraph.AddVex(p);

               //判断该节点是否存在邻接边
                if (int.Parse(item["AdjEdgeCount"].InnerText) == 0)
                    continue;           
 
                //添加节点的邻接边
                XmlNodeList EdgeLists = item["Edges"].ChildNodes;
                foreach (XmlNode Edge in EdgeLists)
                {
                    int adjVexId = int.Parse(Edge.Attributes["adjVex"].Value);

                    if (adjVexId > dataGraph.VexNum - 1)
                        continue;

                    double midA = double.Parse(Edge["MidAngle"].InnerText);
                    int eType = int.Parse(Edge["EdgeType"].InnerText);
                    ArgLine aLine = new ArgLine(midA, eType);
                    aLine.MidLineBid = int.Parse(Edge["MidLineBid"].InnerText);
                    aLine.MidLineDid = int.Parse(Edge["MidLineDid"].InnerText);
                    aLine.MidLength = double.Parse(Edge["MidLength"].InnerText);

                    dataGraph.SetEgde(p, dataGraph.VexList[adjVexId].Data, aLine);
                }

            }

            return dataGraph;
        }

    }
}
