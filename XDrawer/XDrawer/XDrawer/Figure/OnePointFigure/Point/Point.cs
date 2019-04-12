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
    [Serializable]
    public class Point : OnePointFigure
    {
        public Point()
            : base()
        {

        }
        public Point(PictureBox view, int x1, int y1)
            : base(view, x1, y1)
        {

        }
        public override void draw(Graphics g)
        {
            Pen pen = new Pen(Color.Black);
            g.DrawRectangle(pen, _x1 - 3, _y1 - 3, 6, 6);

        }
        public override void move(int x, int y)
        {
            _x1 += x;
            _y1 += y;
        }
        public override void draw(Graphics g, Pen pen)
        {
           g.DrawRectangle(pen, _x1 - 3, _y1 - 3, 6, 6);
        }
        public override void drawing(Graphics g, int newX, int newY)
        {
            
        }

        public override Figure clone()
        {
            Point newFigure = new Point(_view, _x1, _y1);
            newFigure._popup = _popup;
            newFigure.move(10, 20);
            return newFigure;
        }
    }
}
