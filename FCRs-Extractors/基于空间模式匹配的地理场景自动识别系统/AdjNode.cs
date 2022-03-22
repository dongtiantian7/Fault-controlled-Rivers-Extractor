using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace 基于空间模式匹配的地理场景自动识别系统
{
    public class AdjNode
    {
        private int _adjVexId;
        public int AdjVexId
        {
            get { return _adjVexId; }
            set { _adjVexId = value; }
        }

        private AdjNode _next;
        public AdjNode Next
        {
            get { return _next; }
            set { _next = value; }
        }

        private ArgLine _edgeValue;
        public ArgLine EdgeValue
        {
            get { return _edgeValue; }
            set { _edgeValue = value; }
        }

      
        public AdjNode()
        {
            _adjVexId = -1;
            _next = null;
        }

        public AdjNode(int adjVexId)
        {
            _adjVexId = adjVexId;
            _next = null;
        }

        public AdjNode(int adjVexId, ArgLine edgeValue)
        {
            _adjVexId = adjVexId;
            _next = null;
            _edgeValue = edgeValue;
        }

       
    }
}
