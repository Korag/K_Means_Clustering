using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace K_Means_Clustering
{
    class Drawing
    {
        private static double SearchRange(double[][] BaseMatrix, int Dimension)
        {
            double range = 0;
            double maxValue = BaseMatrix[1][Dimension];

            for (int i = 1; i < BaseMatrix.Length; i++)
            {
                if (BaseMatrix[i][Dimension] > maxValue)
                {
                    maxValue = BaseMatrix[i][Dimension];
                }
            }

            range = maxValue + 2;

            return range;
        }


        public static void DrawClusterGraph(double[][] BaseMatrix, string PathToGraph, int GraphWidth, int GraphHeight, int FirstDimension, int SecondDimension)
        {
            Bitmap b = new Bitmap(GraphWidth, GraphHeight);
            Graphics g = Graphics.FromImage(b);
            Brush br = new SolidBrush(Color.White);

            g.FillRectangle(br, 0, 0, b.Width, b.Height);

            Pen p1 = new Pen(Color.Black, 3);
            g.DrawLine(p1, 0, b.Height / 2, b.Width, b.Height / 2);
            g.DrawLine(p1, b.Width / 2, 0, b.Width / 2, b.Height);

            double rangeX = Math.Ceiling(SearchRange(BaseMatrix, FirstDimension));
            double rangeY = Math.Ceiling(SearchRange(BaseMatrix, SecondDimension));
            double Unit = 30;

            double scaleX = b.Width / Unit;
            double scaleY = b.Height / Unit;

            double rangePerUnitX = rangeX / Unit;
            double rangePerUnitY = rangeY / Unit;

            Pen p11 = new Pen(Color.Black);
            Pen p2 = new Pen(Color.Red);

            int dx = (int)scaleX;
            int dy = (int)scaleY;

            Font f1 = new Font(FontFamily.GenericSansSerif, 12);
            Brush br2 = new SolidBrush(Color.Blue);

            double t1 = rangeX;
            double t2 = rangeY;

            for (int OY = 0; OY < b.Height; OY += dy ) 
            {
                g.DrawLine(p11, 0, OY, b.Width, OY);

                string s = (t1).ToString("F" + 1);
                g.DrawString(s, f1, br2, b.Width/2 + 1, OY);
                t1 -= 2*rangePerUnitX;
            }

            t1 = -rangeX;

            for (int OX = 0; OX < b.Width; OX += dx )
            {
                g.DrawLine(p11, OX, 0, OX, b.Height);

                string s = (t1).ToString("F" + 1);
                g.DrawString(s, f1, br2, OX, (b.Height / 2) - 1);
                t1 += 2*rangePerUnitX;
            }
          

            for (int i = 0; i < BaseMatrix.Length; i++)
            {
                float x = (float)(BaseMatrix[i][FirstDimension]);
                float y = (float)(BaseMatrix[i][SecondDimension]);

                
                g.DrawEllipse(p2, (int)(scaleX*x + b.Width/2), (int)(b.Height / 2 - scaleY * y), 8, 8);
            }












            b.Save(PathToGraph + ".png");
        }

    }
}
