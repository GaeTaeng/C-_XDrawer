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


    public abstract class Figure
    {
        protected PictureBox _view;
        protected Region _region;
        private Popup _popup;
        
        public Figure()
        {
            _view = null;
            _region = null;
        }

        public Figure(PictureBox view)
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
        public abstract void makeRegion();
        public abstract void draw(Graphics g);
        public abstract void draw(Graphics g, Pen pen);
        public abstract void drawing(Graphics g, int newX, int newY);
    }
}
