using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESRI.ArcGIS.Geometry;

namespace test
{
    //ARG的边
     public class ArgLine
    {
        private int _rID;
        public int RID
        {
            get { return _rID; }
            set { _rID = value; }
        }

        public List<IPoint> LineL;

        public List<IPoint> MidLine;
         
         //边对应的角度
         private double _midAngle;
         public double MidAngle
        {
            get { return _midAngle; }
            set { _midAngle = value; }
        }

         //边的位置
         private int _lineType;
         public int LineType
         {
             get { return _lineType; }
             set { _lineType = value; }
         }

        //边对应的过渡河段长度
        private double _midLength;
        public double MidLength
        {
            get { return _midLength; }
            set { _midLength = value; }
        }


        //边对应的角度最小值
        private double _minAngle;
        public double MinAngle
        {
            get { return _minAngle; }
            set { _minAngle = value; }
        }

        //边对应的角度最大值
        private double _maxAngle;
        public double MaxAngle
        {
            get { return _maxAngle; }
            set { _maxAngle = value; }
        }

        //构造函数
        public ArgLine()
        {
            this._minAngle = 0;
            this._maxAngle = 180;
        }

        public ArgLine(int rID, double midAngle, double midLength)
        {
            _rID = rID;
            LineL = new List<IPoint>();
            _midAngle = midAngle;
            _midLength = midLength;
        }

        public ArgLine(int rID, double midAngle)
        {
            _rID = rID;
            LineL = new List<IPoint>();
            MidLine = new List<IPoint>();

            _midAngle = midAngle;
            MidLength = 0;
        }

        public ArgLine(double midAngle,int lineType)
        {
            //_rID = rID;
            LineL = new List<IPoint>();
            MidLine = new List<IPoint>();

            _midAngle = midAngle;
            _lineType = lineType;
            this.MidLength = 0;
        }

        public ArgLine(double minAngle, double maxAngle, int lineType)
        {
            _minAngle = minAngle;
            _maxAngle = maxAngle;
            _lineType = lineType;
        }


    }
}
