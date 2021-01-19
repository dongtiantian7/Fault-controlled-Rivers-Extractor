using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace test
{
    class PatternDefine
    {
        public string xmlFilePath;

        //构造函数
        public PatternDefine(string xFilePath)
        {
            this.xmlFilePath = xFilePath;

        }

        //生成所有模式的xml文件
        public void GenerateAll()
        {
            this.PgenerateXmlFile("BarPattern.xml", this.CreatBarb());
            this.PgenerateXmlFile("StrPattern.xml", this.CreatStr());
            this.PgenerateXmlFile("RightTurnPattern.xml", this.CreatRightTurn());
            this.PgenerateXmlFile("CouPattern1.xml", this.CreatCou1());
            this.PgenerateXmlFile("CouPattern2.xml", this.CreatCou2());
            this.PgenerateXmlFile("CapturePattern.xml", this.CreatCapture());
        }



        //生成倒钩河的模式图
        private GraphStruct CreatBarb()
        {
            GraphStruct patternGraph = new GraphStruct();

            ArgPoint vex1 = new ArgPoint();
            vex1.NodeType = -1; vex1.PID = 0; 
            patternGraph.AddVex(vex1);
            ArgPoint vex2 = new ArgPoint();
            vex2.NodeType = 1; vex2.PID = 1;
            patternGraph.AddVex(vex2);
            ArgPoint vex3 = new ArgPoint();
            vex3.NodeType = 1; vex3.PID = 2; vex3.IsStore = true;
            patternGraph.AddVex(vex3);

            ArgLine edge12 = new ArgLine();
            edge12.MinAngle = 0; edge12.MaxAngle = 30; edge12.LineType = -1; edge12.MidLength = 700;
            patternGraph.SetEgde(vex1, vex2, edge12);
            ArgLine edge13 = new ArgLine();
            edge13.MinAngle = 80; edge13.MaxAngle = 180; edge13.LineType = -1; edge13.MidLength = 700;
            patternGraph.SetEgde(vex1, vex3, edge13);
            ArgLine edge23 = new ArgLine();
            edge23.MinAngle = 80; edge23.MaxAngle = 180; edge23.LineType = 1; edge23.MidLength = 700;
            patternGraph.SetEgde(vex2, vex3, edge23);

            return patternGraph;
        }

        //生成直线河流的模式图
        private GraphStruct CreatStr()
        {
            GraphStruct strGraph = new GraphStruct();

            ArgPoint vex1 = new ArgPoint();
            vex1.PID = 0;
            vex1.ST = 1.05;
            vex1.LT = 7000;
            vex1.IsStore = true;
            //vex1.NodeType = 0;
            strGraph.AddVex(vex1);


            return strGraph;
        }

        //生成直角转弯的模式图
        private GraphStruct CreatRightTurn()
        {
            GraphStruct rigGraph = new GraphStruct();

            ArgPoint vex1 = new ArgPoint();
            vex1.PID = 0; vex1.IsStore = true;
            rigGraph.AddVex(vex1);
            ArgPoint vex2 = new ArgPoint();
            vex2.PID = 1; vex2.IsStore = true;
            rigGraph.AddVex(vex2);
           

            ArgLine edge12 = new ArgLine();
            edge12.MinAngle = 70; edge12.MaxAngle = 110; edge12.LineType = 0; edge12.MidLength = 2000;
            rigGraph.SetEgde(vex1, vex2, edge12);


            return rigGraph;
        }

        //生成对口河的模式图1
        private GraphStruct CreatCou1()
        {
            GraphStruct couGraph = new GraphStruct();

            ArgPoint vex1 = new ArgPoint();
            vex1.NodeType = 1; vex1.PID = 0; vex1.IsStore = true;
            couGraph.AddVex(vex1);
            ArgPoint vex2 = new ArgPoint();
            vex2.NodeType = 1; vex2.PID = 1; vex2.IsStore = true;
            couGraph.AddVex(vex2);
            ArgPoint vex3 = new ArgPoint();
            vex3.NodeType = -1; vex3.PID = 2;
            couGraph.AddVex(vex3);

            ArgLine edge12 = new ArgLine();
            edge12.MinAngle = 140; edge12.MaxAngle = 180; edge12.LineType = 1; edge12.MidLength = 400;
            couGraph.SetEgde(vex1, vex2, edge12);
            ArgLine edge13 = new ArgLine();
            edge13.MinAngle = 20; edge13.MaxAngle = 180; edge13.LineType = -1; edge13.MidLength = 400;
            couGraph.SetEgde(vex1, vex3, edge13);
            ArgLine edge23 = new ArgLine();
            edge23.MinAngle = 20; edge23.MaxAngle = 180; edge23.LineType = -1; edge23.MidLength = 400;
            couGraph.SetEgde(vex2, vex3, edge23);

            return couGraph;
        }

        //生成对口河的模式图2
        private GraphStruct CreatCou2()
        {
            GraphStruct couGraph = new GraphStruct();

            ArgPoint vex1 = new ArgPoint();
            vex1.NodeType = 1; vex1.PID = 0; vex1.NodeType = 1; 
            couGraph.AddVex(vex1);
            ArgPoint vex2 = new ArgPoint();
            vex2.NodeType = -1; vex2.PID = 1;
            couGraph.AddVex(vex2);
            ArgPoint vex3 = new ArgPoint();
            vex3.NodeType = 1; vex3.PID = 2;
            couGraph.AddVex(vex3);
            ArgPoint vex4 = new ArgPoint();
            vex4.NodeType = 1; vex4.PID = 3; vex4.NodeType = 1; 
            couGraph.AddVex(vex4);


            ArgLine edge12 = new ArgLine();
            edge12.MinAngle = 20; edge12.LineType = -1; edge12.MidLength = 400;
            couGraph.SetEgde(vex1, vex2, edge12);
            ArgLine edge13 = new ArgLine();
            edge13.LineType = 1; edge13.MidLength = 400;
            couGraph.SetEgde(vex1, vex3, edge13);
            ArgLine edge14 = new ArgLine();  //关键边
            edge14.MinAngle = 160; edge14.MaxAngle = 180; edge14.LineType = 1; edge14.MidLength = 400;
            couGraph.SetEgde(vex1, vex4, edge14);
            ArgLine edge23 = new ArgLine();
            edge23.LineType = -1; edge23.MidLength = 400;
            couGraph.SetEgde(vex2, vex3, edge23);
            ArgLine edge24 = new ArgLine();
            edge24.MinAngle = 20; edge24.LineType = -1; edge24.MidLength = 400;
            couGraph.SetEgde(vex2, vex4, edge24);
            ArgLine edge34 = new ArgLine();
            edge34.LineType = 1;
            couGraph.SetEgde(vex3, vex4, edge34); edge34.MidLength = 400;

            return couGraph;
        }



        //生成河流袭夺 的模式图
        private GraphStruct CreatCapture()
        {
            GraphStruct capGraph = new GraphStruct();

            ArgPoint vex1 = new ArgPoint();
            vex1.NodeType = 1; vex1.PID = 0; vex1.IsStore = true;
            capGraph.AddVex(vex1);
            ArgPoint vex2 = new ArgPoint();
            vex2.NodeType = 11; vex2.PID = 1; vex2.IsStore = true;
            capGraph.AddVex(vex2);
            ArgPoint vex3 = new ArgPoint();
            vex3.NodeType = -1; vex3.PID = 2; vex3.IsStore = true; 
            capGraph.AddVex(vex3);

            ArgLine edge12 = new ArgLine();
            edge12.MinAngle = 120; edge12.MaxAngle = 180; edge12.LineType = 1; edge12.MidLength = 700;
            capGraph.SetEgde(vex1, vex2, edge12);
            ArgLine edge13 = new ArgLine();
            edge13.MinAngle = 80; edge13.MaxAngle = 180; edge13.LineType = -1; edge13.MidLength = 700;
            capGraph.SetEgde(vex1, vex3, edge13);
            ArgLine edge23 = new ArgLine();
            edge23.MinAngle = 0; edge23.MaxAngle = 180; edge23.LineType = -1; edge23.MidLength = 700;
            capGraph.SetEgde(vex2, vex3, edge23);


            return capGraph;
        }
       
        //生成模式的Xml文件
        private void PgenerateXmlFile(string FileName, GraphStruct PatternGraph)
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

                XmlElement nSt = myXmlDoc.CreateElement("ST");
                nSt.InnerText = PatternGraph.VexList[i].Data.ST.ToString();
                vex.AppendChild(nSt);

                XmlElement nLt = myXmlDoc.CreateElement("LT");
                nLt.InnerText = PatternGraph.VexList[i].Data.LT.ToString();
                vex.AppendChild(nLt);

                XmlElement nIsStore = myXmlDoc.CreateElement("IsStore");
                nIsStore.InnerText = PatternGraph.VexList[i].Data.IsStore.ToString();
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

            myXmlDoc.Save(this.xmlFilePath + FileName);
        }

        //生成数据图的Xml文件
        public void GgenerateXmlFile(string FileName, GraphStruct DataGraph)
        {
            XmlDocument myXmlDoc = new XmlDocument();
            XmlDeclaration xmldecl = myXmlDoc.CreateXmlDeclaration("1.0", "utf-8", null);
            myXmlDoc.AppendChild(xmldecl);
            XmlElement rootElement = myXmlDoc.CreateElement("DataARG");
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

                XmlElement nLength = myXmlDoc.CreateElement("Length");
                nLength.InnerText = DataGraph.VexList[i].Data.Length.ToString();
                vex.AppendChild(nLength);

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

                        EdgeLists.AppendChild(edge);
                        aNode = aNode.Next;
                    }

                    vex.AppendChild(EdgeLists);
                }

                VerLists.AppendChild(vex);
            }
            rootElement.AppendChild(VerLists);

            myXmlDoc.Save(this.xmlFilePath + FileName);
        }

        //读取模式Xml文件
        public GraphStruct PreadXmlFile(string FileName)
        {
            GraphStruct pGraph = new GraphStruct();

            XmlDocument myXmlDoc = new XmlDocument();
            myXmlDoc.Load(this.xmlFilePath + FileName);

            XmlNode rootNode = myXmlDoc.SelectSingleNode("RiverPattern");

            int vNum = int.Parse(rootNode["VexNodeNum"].InnerText);
            int eNum = int.Parse(rootNode["EdgeNum"].InnerText);

            XmlNodeList VerLists = rootNode["VexNodes"].ChildNodes;
            foreach(XmlNode item in VerLists)
            {
               // XmlElement vexElement = (XmlElement)item;

                ArgPoint p = new ArgPoint();
                p.PID = int.Parse(item.Attributes["id"].Value);
                p.NodeType = int.Parse(item["NodeType"].InnerText);
                p.ST = double.Parse(item["ST"].InnerText);
                p.LT = int.Parse(item["LT"].InnerText);
                p.IsStore = bool.Parse(item["IsStore"].InnerText);

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
        public GraphStruct GreadXmlFile(string FileName)
        {
            GraphStruct dataGraph = new GraphStruct();

            XmlDocument myXmlDoc = new XmlDocument();
            myXmlDoc.Load(this.xmlFilePath + FileName);

            XmlNode rootNode = myXmlDoc.SelectSingleNode("RiverPattern");

            XmlNodeList VerLists = rootNode["VexNodes"].ChildNodes;
            foreach (XmlNode item in VerLists)
            {
                // XmlElement vexElement = (XmlElement)item;

                ArgPoint p = new ArgPoint();
                p.PID = int.Parse(item.Attributes["id"].Value);
                p.NodeType = int.Parse(item["NodeType"].InnerText);
                p.Length = int.Parse(item["Length"].InnerText);

                //添加节点
                dataGraph.AddVex(p);

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

                    dataGraph.SetEgde(p, dataGraph.VexList[adjVexId].Data, aLine);
                }

            }

            return dataGraph;
        }
    

    }
}
