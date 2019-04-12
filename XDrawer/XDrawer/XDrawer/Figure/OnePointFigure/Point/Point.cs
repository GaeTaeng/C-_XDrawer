using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace XDrawer.NewFolder1.OnePointFigure.Point
{
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
            g.DrawRectangle(pen, _x1 - 3, _y1 - 3, _x1 + 3, _y1 + 3);

        }

        public override void draw(Graphics g, Pen pen)
        {
           g.DrawRectangle(pen, _x1 - 3, _y1 - 3, _x1 + 3, _y1 + 3);
        }
        public override void drawing(Graphics g, int newX, int newY)
        {
            
        }
    }
}
