using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Runtime.InteropServices;
using DlibDotNet;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using Rectangle = System.Drawing.Rectangle;
using Point = DlibDotNet.Point;

namespace face_analyzer
{
    public partial class Form1 : Form
    {
        FaceReader reader = FaceReader.getInstance;
        Dictionary<Emotion, string> emotions = new Dictionary<Emotion, string>();
        public Form1()
        {
            InitializeComponent();
            emotions.Add(Emotion.HAPPY, "happy");
            emotions.Add(Emotion.SAD, "sad");
            emotions.Add(Emotion.ANGRY, "angry");
            emotions.Add(Emotion.NEUTRAL, "neutral");
        }

        private void btnRead_Click(object sender, EventArgs e)
        {
            OpenFileDialog o = new OpenFileDialog();

            if (o.ShowDialog() == DialogResult.OK)
            {
                int _f = 0;
                string emotionstring = "";
                Bitmap bitmap = new Bitmap(o.FileName);
                Bitmap imgp = new Bitmap(bitmap);
                Bitmap graph = new Bitmap(bitmap.Width, bitmap.Height);
                panel1.BackgroundImage = bitmap;

                List<Face> faces =  reader.read(bitmap);
                using (Graphics g = Graphics.FromImage(imgp))
                {
                    Brush b = Brushes.Black;
                    Pen pen = new Pen(b);
                    pen.Width = 9;
                    Pen pen2 = new Pen(Color.Red);
                    pen2.Width = 40;
                    foreach (Face f in faces)
                    {
                        for (int i = 0; i < f.points.Length-1; i++)
                        {
                            FacePoint p = f.points[i];
                            FacePoint next = f.points[i + 1];
                            g.DrawLine(pen, (float)p.x, (float)p.y, (float)next.x, (float)next.y);
                        }
                        using (Graphics g2 = Graphics.FromImage(graph))
                        {
                            Curve mouth = f.mouth;
                            FacePoint c = mouth.center;
                            double r = mouth.radius;
                            ///MessageBox.Show("r: " + r + ", center: " + c.ToString());
                            g2.DrawEllipse(pen2, new RectangleF((float)mouth.getLine.left.x - 5, (float)mouth.getLine.left.y - 5, 10, 10));
                            g2.DrawEllipse(pen2, new RectangleF((float)mouth.getLine.right.x - 5, (float)mouth.getLine.right.y - 5, 10, 10));
                            g2.DrawEllipse(pen2, new RectangleF((float)mouth.getMid.x - 5, (float)mouth.getMid.y - 5, 10, 10));
                            //g2.DrawEllipse(pen2, new RectangleF((float)f.points[50].x - 5, (float)f.points[50].y - 5, 10, 10));
                            g2.DrawEllipse(pen, (float)(c.x - r), (float)(c.y - r), (float)r *2, (float)r*2);
                            emotionstring += (_f>0?" | ":"") + "[" + _f + "]: " + emotions[f.emotion];
                        }
                        panel2.BackgroundImage = graph;
                    }
                    panel1.BackgroundImage = imgp;
                    lblEmotion.Text = emotionstring;
                }
            }
        }
    }
}
