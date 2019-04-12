using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace XDrawer
{
    public class Popup 
    {
        // Attributes
        protected ContextMenu _popupPtr;
        protected Form _pView;

        // Operations
        public Popup(Form view,String title) {
              _pView = view;
              _popupPtr = new ContextMenu();
              if (title != null) {
                      _popupPtr.MenuItems.Add(title);
                      _popupPtr.MenuItems.Add("-");
              }
        }
        public void popup(PictureBox canvas, System.Drawing.Point pos) {
            _popupPtr.Show(canvas, pos);
        }
    }
}
