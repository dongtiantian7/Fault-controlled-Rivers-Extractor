using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESRI.ArcGIS.Geometry;

namespace 基于空间模式匹配的地理场景自动识别系统
{
    //ARG的点结构
    public class ArgPoint
    {
        //数据图属性：ARG点ID
        private int _pID;
        public int PID
        {
            get { return _pID; }
            set { _pID = value; }
        }

        //数据图属性：点坐标
        private IPoint _pointL;
        public IPoint PointL
        {
            get { return _pointL; }
            set { _pointL = value; }
        }

        //中间数据：点的所属河流ID
        private int _riverID;
        public int RiverID
        {
            get { return _riverID; }
            set { _riverID = value; }
        }

        //数据图属性：平直河段的弯曲度
        private double _curb;
        public double Curb
        {
            get { return _curb; }
            set { _curb = value; }
        }
        
        //数据图属性：平直河段的长度
        private double _length;
        public double Length
        {
            get { return _length; }
            set { _length = value; }
        }


        //数据图属性：平直河段的走向
        private IPoint _direction;
        public IPoint Direction
        {
            get { return _direction; }
            set { _direction = value; }
        }

        //模式图和数据图属性：平直河段的位置类型
        private int _nodeType;
        public int NodeType
        {
            get { return _nodeType; }
            set { _nodeType = value; }
        }

        //模式图属性：平直河段的弯曲度阈值
        private double _ct;
        public double CT
        {
            get { return _ct; }
            set { _ct = value; }
        }

        //模式图属性：平直河段的长度阈值
        private double _lt;
        public double LT
        {
            get { return _lt; }
            set { _lt = value; }
        }

        //数据图属性：平直河段的开始点ID
        private int _bPointId;
        public int BPointId
        {
            get { return _bPointId; }
            set { _bPointId = value; }
        }

        //数据图属性：平直河段的结束点ID
        private int _dPointId;
        public int DPointId
        {
            get { return _dPointId; }
            set { _dPointId = value; }
        }

        //模式图属性：该节点是否需要记录
        private string _isStore;
        public string IsStore
        {
            get { return _isStore; }
            set { _isStore = value; }
        }


        //构造函数
        public ArgPoint()
        {
            _pID = -99;
            _isStore = "No";
        }

        public ArgPoint(int pID, IPoint pointL, int riverId, int bPointId, int dPointId, double length, double curbS)
        {
            _pID = pID;
            _pointL = pointL;
            _riverID = riverId;
            _bPointId = bPointId;
            _dPointId = dPointId;

            _length = length;
            _direction = new PointClass();
            _curb = curbS;
        }

    }
}
