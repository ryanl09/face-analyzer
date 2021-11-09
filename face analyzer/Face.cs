using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace face_analyzer
{
    public class Face
    {
        private FacePoint[] _points;
        private FacePoint[] _pointsR;
        private Emotion _emotion;
        private double _slant;
        private Curve _mouth;

        private const int x = 0;
        private const int y = 1;
        private const int FACELEFT = 0;
        private const int FACERIGHT = 16;
        private const int EYELEFT = 36;
        private const int EYERIGHT = 45;
        private const int NOSERIGHT = 35;
        private const int NOSELEFT = 31;
        private const int MOUTHRIGHT = 54;
        private const int MOUTHLEFT = 48;

        public Face() 
        {
            _emotion = new Emotion();
        }

        public FacePoint[] points
        {
            get
            {
                return _points;
            }
        }

        public FacePoint [] pointsR
        {
            get
            {
                return _pointsR;
            }
        }

        public void addPoint(double x, double y)
        {
            int length = _points.Length;
            _points[length] = new FacePoint(x, y);
            if (_points.Length == 68)
            {
                _slant = calculateSlant();
                rotate();
                calculateMouth();
            }
        }

        public Emotion emotion
        {
            get
            {
                return _emotion;
            }
            set
            {
                _emotion = value;
            }
        }

        public double slant
        {
            get
            {
                return _slant;
            }
        }

        public double width
        {
            get
            {
                return _points[16].x - _points[0].x;
            }
        }

        public double height
        {
            get
            {
                return (_points[19].y + _points[24].y)/2-_points[8].y;
            }
        }

        public Curve mouth
        {
            get
            {
                return _mouth;
            }
        }

        //get face rotation estimate by averaging the slopes of facial landmarks
        private double calculateSlant()
        {
            double s = 0;
            //face edge slope
            double x1 = _points[FACELEFT].x;
            double y1 = _points[FACELEFT].y;
            double x2 = _points[FACERIGHT].x;
            double y2 = _points[FACERIGHT].y;
            s += (y2 - y1) / (x2 - x1);
            // eye slope
            x1 = _points[EYELEFT].x;
            y1 = _points[EYELEFT].y;
            x2 = _points[EYERIGHT].x;
            y2 = _points[EYERIGHT].y;
            s += (y2 - y1) / (x2 - x1);
            ///nose slope
            x1 = _points[NOSELEFT].x;
            y1 = _points[NOSELEFT].y;
            x2 = _points[NOSERIGHT].x;
            y2 = _points[NOSERIGHT].y;
            s += (y2 - y1) / (x2 - x1);
            //mouth slope
            x1 = _points[MOUTHLEFT].x;
            y1 = _points[MOUTHLEFT].y;
            x2 = _points[MOUTHRIGHT].x;
            y2 = _points[MOUTHRIGHT].y;
            s += (y2 - y1) / (x2 - x1);
            //average
            s /= 4;
            return s;
        }

        //get angle the face is rotated in and reposition points 
        private void rotate()
        {
            double theta = Math.Tan(slant);
            for (int i = 0; i < _points.Length; i++)
            {
                double newX = _points[i].x * Math.Cos(theta) - _points[i].y * Math.Sin(theta);
                double newY = _points[i].y * Math.Cos(theta) + _points[i].x * Math.Sin(theta);
                _pointsR[i] = new FacePoint(newX, newY);
            }
        }

        private double py(double a, double b)
        {
            return Math.Sqrt(a*a+b*b);
        }

        public void calculateMouth()
        {
            FacePoint left = _points[48];
            FacePoint mid = _points[57];
            FacePoint right = _points[54];
            _mouth =  new Curve(left, right, mid);
        }
    }
}
