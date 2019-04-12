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
    public abstract class TwoPointFigure : Figure
    {
       protected int _x1;
       protected int _y1;
       protected int _x2;
       protected int _y2;

       public TwoPointFigure()
           : base()
       {
           _x1 = _x1 = _x2 = _y2 = 0;
       }
       public TwoPointFigure(PictureBox view, int x1, int y1, int x2, int y2)
           : base(view)
        {
            _x1 = x1; _y1 = y1;
            _x2 = x2; _y2 = y2;
        }
       public override void drawing(Graphics g, int x, int y)
       {

           g.CompositingMode = CompositingMode.SourceOver;
           TextureBrush brush = new TextureBrush(_view.Image);
           Pen pPen = new Pen(brush, 1);
           draw(g, pPen);
           pPen.Dispose();

           Pen pp = new Pen(Color.Black, 1);
           draw(g, pp);
           pPen.Dispose();
       }
    }
}
