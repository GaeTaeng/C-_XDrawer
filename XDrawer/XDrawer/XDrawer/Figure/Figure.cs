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
        public Figure()
        {
            _view = null;
        }

        public Figure(PictureBox view)
        {
            _view = view;
        }
        public abstract void draw(Graphics g);
        public abstract void draw(Graphics g, Pen pen);
        public abstract void drawing(Graphics g, int newX, int newY);
    }
}
