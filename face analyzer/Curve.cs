using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace face_analyzer
{
    public class Curve
    {
        private Line line;
        private FacePoint mid;
        private FacePoint _center;
        private double _radius = 1;

        public Curve() { }
        public Curve(FacePoint left, FacePoint right, FacePoint mid)
        {
            line = new Line(left, right);
            this.mid = mid;
            calculate();
        }

        public Line getLine
        {
            get
            {
                return line;
            }
        }

        public FacePoint getMid
        {
            get
            {
                return mid;
            }
        }

        public FacePoint center
        {
            get
            {
                return _center;
            }
        }

        public double radius
        {
            get
            {
                return _radius;
            }
        }

        private void calculate()
        {
            double x12 = line.left.x - line.right.x;
            double x13 = line.left.x - mid.x;
            double y12 = line.left.y - line.right.y;
            double y13 = line.left.y - mid.y;
            double y31 = mid.y - line.left.y;
            double y21 = line.right.y - line.left.y;
            double x31 = mid.x - line.left.x;
            double x21 = line.right.x - line.left.x;

            double sx13 = (double)(Math.Pow(line.left.x, 2) - Math.Pow(mid.x, 2));
            double sy13 = (double)(Math.Pow(line.left.y, 2) - Math.Pow(mid.y, 2));
            double sx21 = (double)(Math.Pow(line.right.x, 2) - Math.Pow(line.left.x, 2));
            double sy21 = (double)(Math.Pow(line.right.y, 2) - Math.Pow(line.left.y, 2));

            double a = ((sx13) * (y12) + (sy13) * (y12) + (sx21) * (y13) + (sy21) * (y13))
                    / (2 * ((x31) * (y12) - (x21) * (y13)));
            double b = ((sx13) * (x12) + (sy13) * (x12) + (sx21) * (x13) + (sy21) * (x13))
                    / (2 * ((y31) * (x12) - (y21) * (x13)));
            double c = -(double)Math.Pow(line.left.x, 2) - (double)Math.Pow(line.left.y, 2) - 2 * a * line.left.x - 2 * b * line.left.y;

            double h = -a;
            double k = -b;
            double sqr_of_r = h * h + k * k - c;
            double r = Math.Round(Math.Sqrt(sqr_of_r), 5);
            _center = new FacePoint(h, k);
            _radius = r;
        }

        private double yInt(double x, double y, double m)
        {
            return -1 * m * x + y;
        } 

        private double dist(FacePoint a, FacePoint b)
        {
            double x = Math.Abs(Math.Pow(b.x-a.x,2));
            double y = Math.Abs(Math.Pow(b.y-a.y,2));
            return Math.Sqrt(x+y);
        }
    }
}
