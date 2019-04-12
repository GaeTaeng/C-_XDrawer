using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace XDrawer
{
    class MainPopup : Popup 
    {

        // Operations
        public MainPopup(XDrawer view)
           : base(view," 종류 ")
        {
               MenuItem pointItem = new MenuItem(" 점 ");
               pointItem.Click += new EventHandler(view.onCreatePoint);
               _popupPtr.MenuItems.Add(pointItem);

               MenuItem lineItem = new MenuItem(" 선 ");
               lineItem.Click += new EventHandler(view.onCreateLine);
               _popupPtr.MenuItems.Add(lineItem);

              MenuItem boxItem = new MenuItem(" 사각형 ");
              boxItem.Click += new EventHandler(view.onCreateBox);
              _popupPtr.MenuItems.Add(boxItem);

              MenuItem circleItem = new MenuItem(" 원 ");
              circleItem.Click += new EventHandler(view.onCreateCircle);
              _popupPtr.MenuItems.Add(circleItem);

              //MenuItem tvItem = new MenuItem(" TV ");
              //tvItem.Click += new EventHandler(view.onCreateTv);
              //_popupPtr.MenuItems.Add(tvItem);
        }
    }
}
