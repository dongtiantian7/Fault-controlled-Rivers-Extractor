using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESRI.ArcGIS.Geometry;

namespace test
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

        //中间数据：平直河段
       // public List<IPoint> StrLine1;

        //平直河段的开始点ID
        private int _bPointId;
        public int BPointId
        {
            get { return _bPointId; }
            set { _bPointId = value; }
        }

        //平直河段的结束点ID
        private int _dPointId;
        public int DPointId
        {
            get { return _dPointId; }
            set { _dPointId = value; }
        }

        //数据图属性：平直河段的长度
        private double _length;
        public double Length
        {
            get { return _length; }
            set { _length = value; }
        }

        //数据图属性：平直河段的弯曲度
        private double _curbS;
        public double CurbS
        {
            get { return _curbS; }
            set { _curbS = value; }
        }

        //数据图属性：平直河段的走向
        private IPoint _direction;
        public IPoint Direction
        {
            get { return _direction; }
            set { _direction = value; }
        }

        //模式图和数据图属性：平直河段的位置
        private int _nodeType;
        public int NodeType
        {
            get { return _nodeType; }
            set { _nodeType = value; }
        }

        //模式图属性：平直河段的长度阈值
        private double _st;
        public double ST
        {
            get { return _st; }
            set { _st = value; }
        }

        //模式图属性：平直河段的长度阈值
        private double _lt;
        public double LT
        {
            get { return _lt; }
            set { _lt = value; }
        }

        //模式图属性：该节点是否需要记录
        private bool _isStore;
        public bool IsStore
        {
            get { return _isStore; }
            set { _isStore = value; }
        }

        //构造函数
        public ArgPoint()
        { }

        public ArgPoint(int pID, IPoint pointL, int riverId, int bPointId, int dPointId, double length, double curbS)
        {
            _pID = pID;
            _pointL = pointL;
            _riverID = riverId;
            _bPointId = bPointId;
            _dPointId = dPointId;

            _length = length;
            _curbS = curbS;
            _direction = new PointClass();
        }

        
        //public ArgPoint(int pID, IPoint pointL, int riverId, double length, double curbS)
        //{
        //    _pID = pID;
        //    _pointL = pointL;
        //    _riverID = riverId;
        //    StrLine = new List<IPoint>();

        //    _length = length;
        //    _curbS = curbS;
        //    //_nodeType = nodeType;
        //}

        //public void CaculateDirec(int flag)
        //{
        //    _direction = new PointClass();

        //    if (flag > 0)
        //    { 
                  
        //    }
        //    else
        //    { }
        //}

        //public void Reverse()
        //{
        //    _nodeType *= -1;
        //    _direction.X *= -1;
        //    _direction.Y *= -1;
        //}
    }
}
