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
    public abstract class Figure
    {
        //JAVA trangiant
        [NonSerialized] protected PictureBox _view;
        [NonSerialized] protected Region _region;
        [NonSerialized] protected Popup _popup;
        public Figure()
        {
            _view = null;
            _region = null;
        }

        public Figure(PictureBox view)
        {
            _view = view;
        }
        public void setView(PictureBox view)
        {
            _view = view;
        }
        public void setPopup(Popup p)
        {
            _popup = p;
        }
        public bool ptInRegion(int x, int y)
        {
            return _region != null ? _region.IsVisible(x, y) : false;
        }
        public void popup(PictureBox canvas, System.Drawing.Point pos)
        {
            _popup.popup(canvas, pos);
        }
        public virtual void move(Graphics g, int dx, int dy)
        {
            g.CompositingMode = CompositingMode.SourceOver;
            TextureBrush brush = new TextureBrush(_view.Image);
            Pen pen = new Pen(brush, 2);
            pen.DashStyle = DashStyle.Solid;

            draw(g, pen);

            pen.Dispose();

            move(dx, dy);

            Pen p = new Pen(Color.Black, 2);
            p.DashStyle = DashStyle.Solid;

            draw(g, p);

            brush.Dispose();
            p.Dispose();
        }
        public abstract Figure clone();
        public abstract void makeRegion();
        public abstract void draw(Graphics g);
        public abstract void draw(Graphics g, Pen pen);
        public abstract void drawing(Graphics g, int newX, int newY);
        public abstract void move(int dx, int dy);
        
    }
}
