using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESRI.ArcGIS.Geometry;

namespace 基于空间模式匹配的地理场景自动识别系统
{
    //识别出的特殊河段的存储结构
    class SpecRiver
    {
        //河段的点集
        public List<IPoint> RiverLine;

        //河段的属性
        public List<String> Attributes;

        //属性1
        private double _attri1;
        public double Attri1
        {
            get { return _attri1; }
            set { _attri1 = value; }
        }

        //属性2
        private double _attri2;
        public double Attri2
        {
            get { return _attri2; }
            set { _attri2 = value; }
        }

        //属性3
        private double _attri3;
        public double Attri3
        {
            get { return _attri3; }
            set { _attri3 = value; }
        }

        //构造函数
        public SpecRiver()
        {
            RiverLine = new List<IPoint>();
            Attributes = new List<String>();
        }

      
    }
}

