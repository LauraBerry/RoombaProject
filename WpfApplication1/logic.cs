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
    }
}
