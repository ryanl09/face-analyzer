using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace face_analyzer
{
    public class Line
    {
        private FacePoint _a;
        private FacePoint _b;
        private double slope;

        public Line() { }
        public Line(FacePoint left, FacePoint right) 
        {
            _a = left;
            _b = right;
        }

        public FacePoint left
        {
            get
            {
                return _a;
            }
            set
            {
                _a = value;
            }
        }

        public FacePoint right
        {
            get
            {
                return _b;
            }
            set
            {
                _b = value;
            }
        }

        private void calcSlope()
        {
            double y = right.y - left.y;
            double x = right.x - left.x;
            slope = y / x;
        }
    }
}
