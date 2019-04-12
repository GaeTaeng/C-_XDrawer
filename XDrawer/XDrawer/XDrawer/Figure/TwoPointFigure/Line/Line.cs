using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace XDrawer
{
    public class Line : TwoPointFigure
    {
        public Line()
            : base()
        {
        }

        public Line(PictureBox view, int x1, int y1, int x2, int y2)
            :base(view, x1, y1, x2, y2)
        {
        }

        public override void draw(Graphics g)
        {
            Pen pPen = new Pen(Color.Black,2);
            g.DrawLine(pPen, _x1, _y1, _x2, _y2);
            pPen.Dispose();
        }
        public override void draw(Graphics g, Pen pen)
        {
            g.DrawLine(pen, _x1, _y1, _x2, _y2);
            pen.Dispose();
        }
        public void move(int x, int y)
        {
            _x2 = x;
            _y2 = y;
        }
        public override void drawing(Graphics g, int x, int y)
        {

            g.CompositingMode = CompositingMode.SourceOver;
            TextureBrush brush = new TextureBrush(_view.Image);
            Pen pPen = new Pen(brush, 1);
            draw(g, pPen);
            pPen.Dispose();
            move(x, y);

            Pen pp = new Pen(Color.Black, 1);
            draw(g, pp);
            pPen.Dispose();
        }
    }
}
