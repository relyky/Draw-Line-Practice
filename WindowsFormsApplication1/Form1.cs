using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        const double K = .017453292519;
        //const double K = 1.0;

        static Pen myPen = null;
        static Point[] points = null;
        static Graphics g = null;

        public Form1()
        {
            InitializeComponent();

            // Init.
            int my_length = Int32.Parse(txtLength.Text);
            int my_increment = Int32.Parse(txtIncrement.Text);
            int my_angle = Int32.Parse(txtAngle.Text);
            int num_lines = Int32.Parse(txtLine.Text);

            int start_x = Canvas.Width / 2;
            int start_y = Canvas.Height / 2;
            points = CalculatePoints(start_x, start_y, my_angle, my_length, my_increment, num_lines);

            myPen = new Pen(Color.Black, 1.0f);
        }

        private void Canvas_Paint(object sender, PaintEventArgs e)
        {
            // ２點以上才能畫線。
            if (points.Length >= 2)
            {
                g = Canvas.CreateGraphics();
                g.DrawLines(myPen, points);
            }
        }

        private void btnGo_Click(object sender, EventArgs e)
        {
            int my_length = Int32.Parse(txtLength.Text);
            int my_increment = Int32.Parse(txtIncrement.Text);
            int my_angle = Int32.Parse(txtAngle.Text);
            int num_lines = Int32.Parse(txtLine.Text);

            int start_x = Canvas.Width / 2;
            int start_y = Canvas.Height / 2;
            points = CalculatePoints(start_x, start_y, my_angle, my_length, my_increment, num_lines);

            Canvas.Refresh();
        }

        private Point[] CalculatePoints(int start_x, int start_y, int angle, int length, int increment, int num_lines)
        {
            int end_x, end_y, angle_c;
            Point[] points = new Point[num_lines + 1];

            points[0].X = start_x;
            points[0].Y = start_y;
            angle_c = angle;
            for (int i = 1; i <= num_lines; i++)
            {
                angle_c += angle;
                length += increment;
                end_x = (int)(start_x + Math.Cos(angle_c * K) * length);
                end_y = (int)(start_y + Math.Sin(angle_c * K) * length);
                points[i].X = end_x;
                points[i].Y = end_y;

                // next round
                start_x = end_x;
                start_y = end_y;
            }

            return points;
        }
    }
}
