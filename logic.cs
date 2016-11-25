using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApplication1
{
    class logic
    {
        public bool done = false;
        public object_seen blue1;
        public object_seen blue2;
        public object_seen blue3;
        public object_seen red1;
        public object_seen red2;
        public object_seen red3;
        public object_seen green1;
        public object_seen green2;
        public object_seen green3;
        arrayClass arr = new arrayClass();

        public struct object_seen
        {
            public int xCoord;
            public int yCoord;
            public int height;
            public int width;
            public System.Drawing.Color color;
            public bool taken;
        }
        public void init()
        {
            blue1 = new object_seen();
            blue2 = new object_seen();
            blue3 = new object_seen();
            blue1.taken = false;
            blue2.taken = false;
            blue3.taken = false;

            red1 = new object_seen();
            red2 = new object_seen();
            red3 = new object_seen();
            red1.taken = false;
            red2.taken = false;
            red3.taken = false;

            green1 = new object_seen();
            green2 = new object_seen();
            green3 = new object_seen();
            green1.taken = false;
            green2.taken = false;
            green3.taken = false;
        }
        //add thing to compair sizes
        public object_seen[] findBiggest(object_seen a, object_seen b, object_seen c)
        {
            object_seen[] inOrder;
            inOrder= new object_seen[3];
            if (a.height>b.height)
            {
                if(b.height>c.height)
                {
                    inOrder[0] = a;
                    inOrder[1] = b;
                    inOrder[2] = c;
                }
                else if(c.height>a.height)
                {
                    inOrder[0] = c;
                    inOrder[1] = a;
                    inOrder[2] = b;
                }
            }
            else if (a.height>c.height)
            {
                inOrder[0] = b;
                inOrder[1] = a;
                inOrder[2] = c;
            }
            else if (b.height>c.height)
            {
                inOrder[0] = b;
                inOrder[1] = c;
                inOrder[2] = a;
            }
            else
            {
                inOrder[0] = c;
                inOrder[1] = b;
                inOrder[2] = a;
            }
            return inOrder;
        }
        public bool three_filled(object_seen a, object_seen b, object_seen c)
        {
            if (a.taken==true && b.taken==true && c.taken==true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
