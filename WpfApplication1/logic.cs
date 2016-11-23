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
        }
        public void init()
        {
            blue1 = new object_seen();
            blue2 = new object_seen();
            blue3 = new object_seen();

            red1 = new object_seen();
            red2 = new object_seen();
            red3 = new object_seen();

            green1 = new object_seen();
            green2 = new object_seen();
            green3 = new object_seen();
        }
        //add thing to compair sizes
        public object_seen findBiggest(object_seen a, object_seen b, object_seen c)
        {
            if (a.height>b.height)
            {
                if(b.height>c.height)
                {
                    return a;
                }
                else if(c.height>a.height)
                {
                    return c;
                }
            }
            else if (a.height>c.height)
            {
                return b;
            }
            else if (b.height>a.height)
            {
                if(c.height>b.height)
                {
                    return c;
                }
                else if (b.height>c.height)
                {
                    return b;
                }
            }
            else if (b.height>c.height)
            {
                return a;
            }
            else if (c.height>a.height)
            {
                if (c.height > b.height)
                {
                    return c;
                }
                else if (b.height > c.height)
                {
                    return b;
                }
            }
            else if (c.height>b.height)
            {
                return a;
            }
                return a;
        }
    }
}
