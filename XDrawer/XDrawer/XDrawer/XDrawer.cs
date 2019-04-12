using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace XDrawer
{
    public partial class XDrawer : Form // Xdrawer가 손으로 짜는 코드 따로, 자동으로 짜주는 코드 따로 해주기 위해서 partial 가 붙어야 함.
    {
        static int  DRAW_RECT    =1;
        static int  DRAW_LINE    =2;
        static int  DRAW_CIRCLE = 3;
        static int  DRAW_POINT = 4;
        int whatToDraw;
        
        Figure _selectedFigure;
        Figure[] figures;
        int nFigure;
        bool bMousePressed;
        public XDrawer()
        {
            InitializeComponent();
            figures = new Figure[100];
            nFigure = 0;
            bMousePressed = false;
            whatToDraw = DRAW_RECT;
        }
        [System.Runtime.InteropServices.DllImportAttribute("gdi32.dll")]
        private static extern bool BitBlt(
            IntPtr hdcDest,
            int nXDest,
            int nYDest,
            int nWidth,
            int nHeight,
            IntPtr hdcSrc,
            int nXSrc,
            int nYSrc,
            System.Int32 dwRop
            );

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void fileToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void 새로만들기ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void pictureBox1_Click_1(object sender, EventArgs e)
        {

        }

        private void canvas_MouseClick(object sender, MouseEventArgs e)
        {

        }

        private void canvas_Paint(object sender, PaintEventArgs e)
        {

            SolidBrush backBrush = new SolidBrush(BackColor);
            e.Graphics.FillRectangle(backBrush, canvas.ClientRectangle);
            backBrush.Dispose();

            Rectangle a = new Rectangle(10, 20, 30, 30);


            Graphics g = e.Graphics;
            if (canvas.Image != null)
            {
                Rectangle r = canvas.ClientRectangle;
                Graphics g1 = e.Graphics;
                Graphics g2 = Graphics.FromImage(canvas.Image);
                IntPtr dc1 = g1.GetHdc();
                IntPtr dc2 = g2.GetHdc();
                BitBlt(dc2, 0, 0, r.Width, r.Height,
                    dc1, 0, 0, 0x00CC0020);
                g1.ReleaseHdc(dc1);
                g2.ReleaseHdc(dc2);
                g2.Dispose();


                for (int i = 0; i < nFigure; i++)
                {
                    figures[i].draw(g);
                }

            }
        }

        private void canvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (bMousePressed)
            {
                Graphics g = canvas.CreateGraphics();

                _selectedFigure.drawing(g, e.X, e.Y);
                /*
                g.CompositingMode = CompositingMode.SourceOver;
                TextureBrush brush = new TextureBrush(canvas.Image);
                Pen pen = new Pen(brush, 1);
                pen.DashStyle = DashStyle.Solid;

                g.DrawRectangle(pen, ox, oy, ex  - ox, ey - oy);
                pen.Dispose();
                ex = e.X;
                ey = e.Y;
                Pen p = new Pen(Color.Black,1);
                p.DashStyle = DashStyle.Solid;
                g.DrawRectangle(p, ox, oy, ex - ox, ey - oy);
                p.Dispose();
                brush.Dispose();
                */
                g.Dispose();
            }
        }

        private void canvas_MouseDown(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Right) {
                ContextMenu main = new ContextMenu();
                main.MenuItems.Add("모양");

            }else {
                
            Graphics g = canvas.CreateGraphics();
            Pen aPen = new Pen(Color.Black,2);
            if (whatToDraw == DRAW_RECT)
            {
                _selectedFigure = new Box(canvas, e.X, e.Y, e.X, e.Y);
            }
            else if (whatToDraw == DRAW_LINE)
            {
                _selectedFigure = new Line(canvas, e.X, e.Y, e.X, e.Y);
            }
            else if (whatToDraw == DRAW_CIRCLE)
            {
                _selectedFigure = new Circle(canvas, e.X, e.Y, e.X, e.Y);

            }
            else if (whatToDraw == DRAW_POINT)
            {
                //_selectedFigure = new Point(canvas, e.X, e.Y);
            }
            _selectedFigure.draw(g);
            bMousePressed = true;
            }
        }

        private void canvas_MouseUp(object sender, MouseEventArgs e)
        {
            Graphics g = canvas.CreateGraphics();

            Pen aPen = new Pen(Color.Black,2);

            figures[nFigure++] = _selectedFigure;
            canvas.Invalidate();
            bMousePressed = false;
            _selectedFigure = null;
        }

        private void XDrawer_Load(object sender, EventArgs e)
        {

            int h = SystemInformation.PrimaryMonitorMaximizedWindowSize.Height;
            int w = SystemInformation.PrimaryMonitorMaximizedWindowSize.Width;
            // 스크린으 ㅣ사이즈를 받아옴

            this.SetBounds(50, 50, w-100, h-100);

            // This is for rubberbanding. C#
            Rectangle r = this.ClientRectangle;
            Graphics g1 = canvas.CreateGraphics();
            Image img = new Bitmap(r.Width, r.Height, g1);
            canvas.Image = img;
            Graphics g2 = Graphics.FromImage(img);
            IntPtr dc1 = g1.GetHdc();
            IntPtr dc2 = g2.GetHdc();
            BitBlt(dc2, 0, 0, r.Width, r.Height,
                dc1, 0, 0, 0x00CC0020);
            
            g1.ReleaseHdc(dc1);
            g2.ReleaseHdc(dc2);
            g1.Dispose();
            g2.Dispose();
        }

        private void 사각형ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            whatToDraw = DRAW_RECT;
        }

        private void 선ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            whatToDraw = DRAW_LINE;

        }


        private void 원ToolStripMenuItem_Click(object sender, EventArgs e)
        {

            whatToDraw = DRAW_CIRCLE;
        }

        private void 점ToolStripMenuItem_Click(object sender, EventArgs e)
        {

            whatToDraw = DRAW_POINT;
        }

    }
}


