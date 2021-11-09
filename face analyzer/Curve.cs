using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace face_analyzer
{
    public class Curve
    {
        private Line line;
        private FacePoint mid;

        public Curve() { }
        public Curve(FacePoint left, FacePoint right, FacePoint mid)
        {
            line = new Line(left, right);
            this.mid = mid;
            calculate();
        }

        private void calculate()
        {


            //determinant for t area to get radius
            /*
            double a = dist(line.right,mid);
            double b = dist(line.left,mid);
            double c = dist(line.left,line.right);
            double d1 = line.left.x * (line.right.y * mid.y);
            double d2 = line.right.x * (mid.y - line.left.y);
            double d3 = mid.x * (line.left.y - line.right.y);
            double S = 0.5 * Math.Abs(d1+d2+d3);
            double radius = (a * b * c) / (4 * S);
            */

            Line line1 = new Line(line.left, line.right);
            Line line2 = new Line(mid, line.right);

            Equation eq1 = new Equation(-1*(1/line1.slope), yInt(line.right.x, line.right.y, line1.slope));
            Equation eq2 = new Equation(-1*(1/line2.slope), yInt(line.right.x, line.right.y, line2.slope));

            double a1 = -1 * eq1.a;
            double a2 = -1 * eq2.a;
            double delta = a1 - a2;
            if (delta == 0)
            { 
                return;
            }
            double cX = (eq1.b-eq2.b)/delta;
            double cY = (a1*eq2.b-a2*eq1.b)/delta;
            FacePoint center = new FacePoint(cX, cY);
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
