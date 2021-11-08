﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DlibDotNet;
using Rectangle = System.Drawing.Rectangle;
using Point = DlibDotNet.Point;

namespace face_analyzer
{
    public class FaceReader
    {
        private static object obj = new object();
        private static volatile FaceReader instance;
        private const int x = 0;
        private const int y = 1;

        private FaceReader() { }

        public static FaceReader getInstance
        {
            get
            {
                if(instance==null)
                {
                    lock (obj)
                    {
                        if (instance == null)
                        {
                            instance = new FaceReader();
                        }
                    }
                }
                return instance;
            }
        }

        public List<Face> read(Bitmap bitmap)
        {
            List<Face> _faces = new List<Face>();
            Thread read = new Thread(() =>
            {
                //get facial landmarks
                int[][] points = new int[68][];
                   using (FrontalFaceDetector faceDetector = Dlib.GetFrontalFaceDetector())
                   {
                       using (ShapePredictor shapePredictor = ShapePredictor.Deserialize("shapes.dat"))
                       {
                           int width = bitmap.Width;
                           int height = bitmap.Height;
                           Rectangle rect = new Rectangle(0, 0, width, height);
                           BitmapData data = bitmap.LockBits(rect, ImageLockMode.ReadWrite, bitmap.PixelFormat);
                           byte[] bytes = new byte[data.Stride * data.Height];
                           Marshal.Copy(data.Scan0, bytes, 0, bytes.Length);
                           Array2D<BgrPixel> imageData = Dlib.LoadImageData<BgrPixel>(bytes, (uint)height, (uint)width, (uint)data.Stride);
                           bitmap.UnlockBits(data);
                           DlibDotNet.Rectangle[] faces = faceDetector.Operator(imageData);
                           for (int i = 0; i < faces.Length; i++)
                           {
                               Face newface = new Face();
                               FullObjectDetection shape = shapePredictor.Detect(imageData, faces[i]);
                               for (int j = 0; j < shape.Parts; j++)
                               {
                                   Point point = shape.GetPart((uint)j);
                                   newface.addPoint(point.X, point.Y);
                               }
                               _faces.Add(newface);
                           }
                       }
                   }
                   //check emotion



               });
            read.Start();
            read.Join();
            return _faces;
        }

        public Emotion readEmotion(Face f)
        {
            double[] vals = new double[4] { rateHappy(f), rateSad(f), rateAngry(f), rateNeutral(f) };
            int index = 0;

            for(int i=1;i<vals.Length; i++)
            {
                if(vals[i]>vals[i-1])
                {
                    index = i;
                }
            }
            return (Emotion)index;
        }

        private double curve(params FacePoint[] points)
        {
            //f(x)=ax^2+by+c
            double[][] eq = new double[3][];
            FacePoint left = points[48];
            FacePoint mid = points[57];
            FacePoint right = points[54];
        }

        private double rateHappy(Face f)
        {
            double rating = 0;

            return rating;
        }
        private double rateSad(Face f)
        {
            double rating = 0;

            return rating;
        }
        private double rateAngry(Face f)
        {
            double rating = 0;

            return rating;
        }
        private double rateNeutral(Face f)
        {
            double rating = 0;

            return rating;
        }
    }
}
