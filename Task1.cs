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
    public partial class Task1 : Form
    {
        public Task1()
        {
            InitializeComponent();
        }


        private void DrawBezierCurve()
        {
            Graphics g = pictureBox1.CreateGraphics();
            g.Clear(Color.White);
            Pen p = new Pen(Color.Black);
            g.DrawLine(p, new Point(pictureBox1.Width / 2, 0), new Point(pictureBox1.Width / 2, pictureBox1.Height));
            g.DrawLine(p, new Point(0, pictureBox1.Height / 2), new Point(pictureBox1.Width, pictureBox1.Height / 2));

            for (int i = (int)-Math.Max((pictureBox1.Width - pictureBox1.Width / 2), pictureBox1.Width / 2); i <= Math.Max((pictureBox1.Width - pictureBox1.Width / 2), pictureBox1.Width / 2); i++)
            {
                g.DrawLine(p, new Point(pictureBox1.Width / 2 + 10 * i, pictureBox1.Height / 2 - 5), new Point(pictureBox1.Width / 2 + 10 * i, pictureBox1.Height / 2 + 5));
            }
            for (int i = (int)-Math.Max((pictureBox1.Height - pictureBox1.Height / 2), pictureBox1.Height / 2); i <= Math.Max((pictureBox1.Height - pictureBox1.Height / 2), pictureBox1.Height / 2); i++)
            {
                g.DrawLine(p, new Point(pictureBox1.Width / 2 - 5, pictureBox1.Height / 2 + 10 * i), new Point(pictureBox1.Width / 2 + 5, pictureBox1.Height / 2 + 10 * i));
            }
            try
            {
                PointF P1 = new PointF(int.Parse(textBox1.Text), int.Parse(textBox2.Text));
                PointF P2 = new PointF(int.Parse(textBox3.Text), int.Parse(textBox4.Text));
                PointF P3 = new PointF(int.Parse(textBox5.Text), int.Parse(textBox6.Text));
                PointF P4 = new PointF(int.Parse(textBox7.Text), int.Parse(textBox8.Text));

                DrawBezier(g, P1, P2, P3, P4);
            }
            catch (Exception)
            {
                MessageBox.Show("Wrong input");
            }

        }
    private PointF CalculateBezierPoint(float t, PointF P1, PointF P2, PointF P3, PointF P4)
        {
            float oneMinusT = 1 - t;
            float tSquared = t * t;
            float oneMinusTSquared = oneMinusT * oneMinusT;
            float oneMinusTCubed = oneMinusTSquared * oneMinusT;
            float tCubed = tSquared * t;

            float x = oneMinusTCubed * P1.X;
            x += 3 * oneMinusTSquared * t * P2.X;
            x += 3 * oneMinusT * tSquared * P3.X;
            x += tCubed * P4.X;

            float y = oneMinusTCubed * P1.Y;
            y += 3 * oneMinusTSquared * t * P2.Y;
            y += 3 * oneMinusT * tSquared * P3.Y;
            y += tCubed * P4.Y;

            return new PointF(pictureBox1.Width / 2 + x, pictureBox1.Height / 2 - y);
        }
        private void DrawBezier(Graphics g, PointF P1, PointF P2, PointF P3, PointF P4)
        {
            int steps = 100;
            PointF[] points = new PointF[steps + 1];

            for (int i = 0; i <= steps; i++)
            {
                float t = (float)i / steps;
                points[i] = CalculateBezierPoint(t, P1, P2, P3, P4);
            }
            Pen dashed = new Pen(Color.Black);
            dashed.DashStyle = System.Drawing.Drawing2D.DashStyle.DashDot;
            P1.X += pictureBox1.Width / 2;
            P2.X += pictureBox1.Width / 2;
            P3.X += pictureBox1.Width / 2;
            P4.X += pictureBox1.Width / 2;
            P1.Y = pictureBox1.Height / 2 - P1.Y;
            P2.Y = pictureBox1.Height / 2 - P2.Y;
            P3.Y = pictureBox1.Height / 2 - P3.Y;
            P4.Y = pictureBox1.Height / 2 - P4.Y;
            g.DrawLine(dashed, P1, P2);
            g.DrawLine(dashed, P2, P3);
            g.DrawLine(dashed, P3, P4);
            g.DrawLine(dashed, P4, P1);
            g.DrawLines(Pens.Black, points);
        }

        
        private void Task1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            DrawBezierCurve();
        }
    }
}
