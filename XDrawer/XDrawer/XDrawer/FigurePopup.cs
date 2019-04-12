using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace XDrawer
{
    class FigurePopup : Popup 
    {

    // Operations
        public FigurePopup(XDrawer view,String title,bool fillButtonFlag) 
        : base(view,title)
        {
              MenuItem deleteItem = new MenuItem(" 지우기 ");
              deleteItem.Click += new EventHandler(view.Delete_Click);
              _popupPtr.MenuItems.Add(deleteItem);

              MenuItem copyItem = new MenuItem(" 복사하기 ");
              copyItem.Click += new EventHandler(view.Copy_Click);
              _popupPtr.MenuItems.Add(copyItem);

              MenuItem[] colorPopup = new MenuItem[4];
              colorPopup[0] = new MenuItem(" 검정색 ");
              //colorPopup[0].Click += new EventHandler(view.onBlackColor);
              colorPopup[1] = new MenuItem(" 빨강색 ");
              //colorPopup[1].Click += new EventHandler(view.onRedColor);
              colorPopup[2] = new MenuItem(" 초록색 ");
             // colorPopup[2].Click += new EventHandler(view.onGreenColor);
              colorPopup[3] = new MenuItem(" 파랑색 ");
              //colorPopup[3].Click += new EventHandler(view.onBlueColor);
              _popupPtr.MenuItems.Add(" 색 정하기 ", colorPopup);

              if (fillButtonFlag == true) {
                  MenuItem fillItem = new MenuItem(" 채우기 ");
                 // fillItem.Click += new EventHandler(view.onFillFigure);
                  _popupPtr.MenuItems.Add(fillItem);
              }
        }
    }
}
