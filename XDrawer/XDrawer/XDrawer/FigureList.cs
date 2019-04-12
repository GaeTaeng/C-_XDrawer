using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;

namespace XDrawer
{
    [Serializable]
    public class FigureList : ArrayList
    {
        // Operations
        public FigureList() {
        }
        public Figure getAt(int index) {
                Object obj = this[index];
                if (obj is Figure) { // rtti Runtime Type idenfication // java의 instance of // isTypeof //
                    //obj 가 Figure이냐? 하고 물어보는 것.
                      return (Figure)obj;
                } else {
                      return null;
                }
        }
        public void addTail(Figure ptr)
        {
                base.Add(ptr);
        }
        public void removeFigure(Figure ptr)
        {
                base.Remove(ptr);
        }

       // public FigureList operator[](
    }
}
