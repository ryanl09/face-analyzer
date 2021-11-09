using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace face_analyzer
{
    public class Equation
    {
        private double _a;
        private double _b;
        private double? _c;

        public Equation(double a, double b, double? c = null)
        {
            _a = a;
            _b = b;
            _c = c;
        }

        public double a
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

        public double b
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

        public double? c
        {
            get
            {
                return _c;
            }
            set
            {
                _c = value;
            }
        }
    }
}
