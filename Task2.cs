using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace oplab5
{
    public partial class Task2 : Form
    {
        public Task2()
        {
            InitializeComponent();
        }
        public static float CrossProductLength(PointF A, PointF B, PointF C)
        {
            float BAx = A.X - B.X;
            float BAy = A.Y - B.Y;
            float BCx = C.X - B.X;
            float BCy = C.Y - B.Y;
            return (BAx * BCy - BAy * BCx);
        }

        private void DrawKochFractal(Graphics g, PointF P1, PointF P2, PointF PControl, int K)
        {
            if (K == 0)
            {
                g.DrawLine(Pens.Black, P1, P2);
            }
            else
            {
                float dx = P2.X - P1.X;
                float dy = P2.Y - P1.Y;

                PointF P3 = new PointF(P1.X + dx / 3, P1.Y + dy / 3);
                PointF P4 = new PointF(P1.X + 2 * dx / 3, P1.Y + 2 * dy / 3);

                float angle = (float)(Math.PI / 3) * Math.Sign(CrossProductLength(P1, P2, PControl));

                float cosAngle = (float)Math.Cos(angle);
                float sinAngle = (float)Math.Sin(angle);

                PointF P5 = new PointF(
                    P3.X + (dx / 3) * cosAngle - (dy / 3) * sinAngle,
                    P3.Y + (dx / 3) * sinAngle + (dy / 3) * cosAngle
                );

                DrawKochFractal(g, P1, P3, PControl, K - 1);
                DrawKochFractal(g, P3, P5, PControl, K - 1);
                DrawKochFractal(g, P5, P4, PControl, K - 1);
                DrawKochFractal(g, P4, P2, PControl, K - 1);
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Point p1 = new Point(int.Parse(textBox1.Text), int.Parse(textBox2.Text));
            Point p2 = new Point(int.Parse(textBox3.Text), int.Parse(textBox4.Text));
            Point p3 = new Point(int.Parse(textBox5.Text), int.Parse(textBox6.Text));
            Graphics g = CreateGraphics();
            g.Clear(Color.White);
            DrawKochFractal(g, p1, p2, p3, int.Parse(textBox7.Text));
            DrawKochFractal(g, p2, p3, p1, int.Parse(textBox7.Text));
            DrawKochFractal(g, p1, p3, p2, int.Parse(textBox7.Text));
        }
    }
}
