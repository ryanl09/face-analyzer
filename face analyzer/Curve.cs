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
            double a = py(line.right.x-line.left.x, line.right.y - mid.y);
            double b = 0;
            double c = 0;

            double d1 = line.left.x * (line.right.y * mid.y);
            double d2 = line.right.x * (mid.y - line.left.y);
            double d3 = mid.x * (line.left.y - line.right.y);
            double S = 0.5 * Math.Abs(d1+d2+d3);
            double radius = (a * b * c) / (4 * S);

        }

        private double py(double a, double b)
        {
            return Math.Sqrt(a * a + b * b);
        }
    }
}
