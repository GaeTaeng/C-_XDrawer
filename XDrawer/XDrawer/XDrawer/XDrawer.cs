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
using System.Collections;
namespace XDrawer
{
    public partial class XDrawer : Form // Xdrawer가 손으로 짜는 코드 따로, 자동으로 짜주는 코드 따로 해주기 위해서 partial 가 붙어야 함.
    {
        static Popup mainPopup      = null;
        static Popup pointPopup     = null;
        static Popup linePopup      = null;
        static Popup boxPopup       = null;
        static Popup circlePopup    = null;
        static int  DRAW_RECT       = 1;
        static int  DRAW_LINE       = 2;
        static int  DRAW_CIRCLE     = 3;
        static int  DRAW_POINT      = 4;
        int whatToDraw;

        int _currentX   = 0;
        int _currentY   = 0;

        static int NOTHING = 0;
        static int DRAWING = 1;
        static int MOVING = 2;
        int _actionMode;

        FigureList _figures;
        Figure _selectedFigure;
        Figure[] figures;
        bool bMousePressed;
        public XDrawer()
        {
            _actionMode = NOTHING;
            InitializeComponent();
            figures = new Figure[100];
            bMousePressed = false;
            whatToDraw = DRAW_RECT;

            _figures = new FigureList();
            mainPopup = new MainPopup(this);
            pointPopup = new FigurePopup(this, "점", false);
            linePopup = new FigurePopup(this, "선", false);
            boxPopup = new FigurePopup(this, "사각형", true);
            circlePopup = new FigurePopup(this, "원", true);
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


            for (int i = 0; i < _figures.Count; i++)
            {
                Figure fig = _figures.getAt(i);
                fig.draw(g);
            }

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



            }
        }

        private void canvas_MouseMove(object sender, MouseEventArgs e)
        {
            Graphics g = canvas.CreateGraphics();
            int x = e.X;
            int y = e.Y;
            if (_actionMode == DRAWING)
            {
                _selectedFigure.drawing(g, x, y);
            }
            else if (_actionMode == MOVING)
            {
                _selectedFigure.move(g, x -  _currentX, y - _currentY);
                _currentX = x;
                _currentY = y;
            }
            g.Dispose();
        }

        private void canvas_MouseDown(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Right) {
                _selectedFigure = null;
                for (int i = 0; i < _figures.Count; i++)
                {
                    Figure fig = _figures.getAt(i);
                    if (fig.ptInRegion(e.X, e.Y))
                    {
                        _selectedFigure = fig;
                        break;
                    }
                }
                if (_selectedFigure != null)
                {
                    _selectedFigure.popup(canvas, e.Location);
                }
                else
                {
                    mainPopup.popup(canvas, e.Location);
                }
            }else {
                _selectedFigure = null;
                for (int i = 0; i < _figures.Count; i++)
                {
                    Figure fig = _figures.getAt(i);
                    if (fig.ptInRegion(e.X, e.Y))
                    {
                        _selectedFigure = fig;
                        break;
                    }
                }
                if (_selectedFigure != null)
                {
                    _figures.removeFigure(_selectedFigure);
                    _currentX = e.X;
                    _currentY = e.Y;
                    _actionMode = MOVING;
                    canvas.Invalidate();
                    return;
                }
            Graphics g = canvas.CreateGraphics();
            Pen aPen = new Pen(Color.Black,2);
            if (whatToDraw == DRAW_RECT)
            {
                _selectedFigure = new Box(canvas, e.X, e.Y, e.X, e.Y);
                _selectedFigure.setPopup(boxPopup);
            }
            else if (whatToDraw == DRAW_LINE)
            {
                _selectedFigure = new Line(canvas, e.X, e.Y, e.X, e.Y);
                _selectedFigure.setPopup(linePopup);
            }
            else if (whatToDraw == DRAW_CIRCLE)
            {
                _selectedFigure = new Circle(canvas, e.X, e.Y, e.X, e.Y);
                _selectedFigure.setPopup(circlePopup);
            }
            else if (whatToDraw == DRAW_POINT)
            {
                _selectedFigure = new Point(canvas, e.X, e.Y);
                _selectedFigure.setPopup(pointPopup);
            }
            _selectedFigure.draw(g);
            bMousePressed = true;
            _actionMode = DRAWING;
            }
        }

        private void canvas_MouseUp(object sender, MouseEventArgs e)
        {
            Graphics g = canvas.CreateGraphics();

            _figures.addTail(_selectedFigure);
            bMousePressed = false;

            _selectedFigure.makeRegion();

            _selectedFigure = null;

            _actionMode = NOTHING;
            canvas.Invalidate();
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

        public void onCreateBox(object sender, EventArgs e)
        {
            whatToDraw = DRAW_RECT;
        }

        public void onCreateLine(object sender, EventArgs e)
        {
            whatToDraw = DRAW_LINE;

        }


        public void onCreateCircle(object sender, EventArgs e)
        {

            whatToDraw = DRAW_CIRCLE;
        }

        public void onCreatePoint(object sender, EventArgs e)
        {

            whatToDraw = DRAW_POINT;
        }
        public void onCreateNew(object sender, EventArgs e)
        {
            _figures.Clear();
            Invalidate();
        }

        public void Delete_Click(object sentder, EventArgs e)
        {
            if (_selectedFigure == null) return;
            _figures.removeFigure(_selectedFigure);
            _selectedFigure = null;
            canvas.Invalidate();
        }
        public void Copy_Click(object sentder, EventArgs e)
        {
            if (_selectedFigure == null) return;
            Figure newFigure = _selectedFigure.clone();
            newFigure.makeRegion();
            _figures.addTail(newFigure);
            _selectedFigure = null;
            canvas.Invalidate();
        }


    }
}


