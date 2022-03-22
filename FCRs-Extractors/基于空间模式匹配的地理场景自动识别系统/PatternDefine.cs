using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace 基于空间模式匹配的地理场景自动识别系统
{
    class PatternDefine
    {
        //构造函数
        public PatternDefine()
        {

        }


        //生成倒钩河的模式图
        public static GraphStruct CreatBarb(double pMinAngle, double pMidLength)
        {
            GraphStruct patternGraph = new GraphStruct();

            ArgPoint vex1 = new ArgPoint();
            vex1.NodeType = -1; vex1.PID = 0;
            patternGraph.AddVex(vex1);
            ArgPoint vex2 = new ArgPoint();
            vex2.NodeType = 1; vex2.PID = 1;
            patternGraph.AddVex(vex2);
            ArgPoint vex3 = new ArgPoint();
            vex3.NodeType = 1; vex3.PID = 2; vex3.IsStore = "Total";
            patternGraph.AddVex(vex3);

            ArgLine edge12 = new ArgLine();
            edge12.MinAngle = 0; edge12.MaxAngle = 30; edge12.LineType = -1; edge12.MidLength = pMidLength;
            patternGraph.SetEgde(vex1, vex2, edge12);
            ArgLine edge13 = new ArgLine();
            edge13.MinAngle = pMinAngle; edge13.MaxAngle = 180; edge13.LineType = -1; edge13.MidLength = pMidLength;
            patternGraph.SetEgde(vex1, vex3, edge13);
            ArgLine edge23 = new ArgLine();
            edge23.MinAngle = pMinAngle; edge23.MaxAngle = 180; edge23.LineType = 1; edge23.MidLength = pMidLength;
            patternGraph.SetEgde(vex2, vex3, edge23);

            return patternGraph;
        }


        //生成直线河流的模式图2
        public static GraphStruct CreatStr(double pCT, double pLT)
        {
            GraphStruct strGraph = new GraphStruct();

            ArgPoint vex1 = new ArgPoint();
            vex1.PID = 0;
            vex1.CT = pCT;
            vex1.LT = pLT;
            vex1.IsStore = "Part";
            //vex1.NodeType = 0;
            strGraph.AddVex(vex1);


            return strGraph;
        }


        //生成直角转弯的模式图
        public static GraphStruct CreatRightTurn(double pMinAngle, double pMaxAngle, double pMidLength)
        {
            GraphStruct rigGraph = new GraphStruct();

            ArgPoint vex1 = new ArgPoint();
            vex1.PID = 0; vex1.IsStore = "Part";
            rigGraph.AddVex(vex1);
            ArgPoint vex2 = new ArgPoint();
            vex2.PID = 1; vex2.IsStore = "Part";
            rigGraph.AddVex(vex2);


            ArgLine edge12 = new ArgLine();
            edge12.MinAngle = pMinAngle; edge12.MaxAngle = pMaxAngle; edge12.LineType = 0; edge12.MidLength = pMidLength;
            rigGraph.SetEgde(vex1, vex2, edge12);

            return rigGraph;
        }

     
        //生成对口河的模式图1
        public static GraphStruct CreatCou1(double pMinAngle, double pMaxAngle, double pMidLength)
        {
            GraphStruct couGraph = new GraphStruct();

            ArgPoint vex1 = new ArgPoint();
            vex1.NodeType = 1; vex1.PID = 0; vex1.IsStore = "Total";
            couGraph.AddVex(vex1);
            ArgPoint vex2 = new ArgPoint();
            vex2.NodeType = 1; vex2.PID = 1; vex2.IsStore = "Total";
            couGraph.AddVex(vex2);
            ArgPoint vex3 = new ArgPoint();
            vex3.NodeType = -1; vex3.PID = 2;
            couGraph.AddVex(vex3);

            ArgLine edge12 = new ArgLine();
            edge12.MinAngle = pMinAngle; edge12.MaxAngle = pMaxAngle; edge12.LineType = 1; edge12.MidLength = pMidLength;
            couGraph.SetEgde(vex1, vex2, edge12);
            ArgLine edge13 = new ArgLine();
            edge13.MinAngle = 20; edge13.MaxAngle = 180; edge13.LineType = -1; edge13.MidLength = pMidLength;
            couGraph.SetEgde(vex1, vex3, edge13);
            ArgLine edge23 = new ArgLine();
            edge23.MinAngle = 20; edge23.MaxAngle = 180; edge23.LineType = -1; edge23.MidLength = pMidLength;
            couGraph.SetEgde(vex2, vex3, edge23);

            return couGraph;
        }

    }
}
