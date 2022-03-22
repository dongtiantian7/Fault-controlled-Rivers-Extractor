using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace 基于空间模式匹配的地理场景自动识别系统
{
    class VexNode
    {
        private ArgPoint _data;
        public ArgPoint Data
        {
            get { return _data; }
            set { _data = value; }
        }

        //该节点的度
        private int _enum;
        public int ENum
        {
            get { return _enum; }
            set { _enum = value; }
        }

        private AdjNode _firstAdj;
        public AdjNode FirstAdj
        {
            get { return _firstAdj; }
            set { _firstAdj = value; }
        }


        public VexNode()
        {
            _data = null;
            _firstAdj = null;
            _enum = 0;
        }

        public VexNode(ArgPoint data)
        {
            _data = data;
            _firstAdj = null;
            _enum = 0;
        }

       
    }
}

