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
       public override void move(int x, int y)
       {
           _x1 += x;
           _y1 += y;
           _x2 += x;
           _y2 += y;
       }
       public override void makeRegion()
       {
           System.Drawing.Point[] pt = new System.Drawing.Point[4];
           pt[0].X = _x1; pt[0].Y = _y1;
           pt[1].X = _x2; pt[1].Y = _y1;
           pt[2].X = _x2; pt[2].Y = _y2;
           pt[3].X = _x1; pt[3].Y = _y2;
           byte[] type = new byte[4];
           type[0] = (byte)PathPointType.Line;
           type[1] = (byte)PathPointType.Line;
           type[2] = (byte)PathPointType.Line;
           type[3] = (byte)PathPointType.Line;
           GraphicsPath gp = new GraphicsPath(pt, type);
           _region = new Region(gp);
       }
    }
}
