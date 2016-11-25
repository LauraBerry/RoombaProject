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
        //struct to hold things the camera recognizes in the frame
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
        //compare size of objects and put them in an array from biggest to smallest.
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
        //checks if any of the objects given are empty
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
        //figure out where each object is relative to the desired location.
        public void get_barring(object_seen a, object_seen b, object_seen c, int destinationX, int destinationY)
        {
            bool isNull = three_filled(a, b, c);
            if (isNull==false)
            {
                //not all are filled
                //do something
            }
            else
            {
                object_seen[] arr;
                arr = new object_seen[3];
                arr = findBiggest(a, b, c);
                //math stuff
                int distance_to_biggest_X;
                int distance_to_biggest_Y;
                int distance_to_middle_X;
                int distance_to_middle_Y;
                int distance_to_smallest_X;
                int distance_to_smallest_Y;
                distance_to_biggest_X = positiveResult(arr[0].xCoord , destinationX);
                distance_to_biggest_Y = positiveResult(arr[0].yCoord , destinationY);
                distance_to_middle_X = positiveResult(arr[1].xCoord , destinationX);
                distance_to_middle_Y = positiveResult(arr[1].yCoord , destinationY);
                distance_to_smallest_X = positiveResult(arr[2].xCoord , destinationX);
                distance_to_smallest_Y = positiveResult(arr[2].yCoord , destinationY);

                if((distance_to_smallest_X<distance_to_biggest_X && distance_to_middle_X<distance_to_biggest_X)||(distance_to_smallest_Y<distance_to_smallest_Y && distance_to_middle_Y<distance_to_biggest_Y))
                {
                        // the smaller 2 objects are closer to the 
                    if (positiveResult(distance_to_middle_X, distance_to_smallest_X) < 3 || positiveResult(distance_to_middle_Y, distance_to_smallest_Y) < 3)
                    {
                        //both smaller objects are looking generally at the destination
                    }
                    else if (distance_to_middle_X<distance_to_smallest_X || distance_to_middle_Y<distance_to_smallest_Y)
                    {
                        //middel is closer than smallest 
                        //turn towards middle
                    }
                    else if (distance_to_smallest_X<distance_to_middle_X || distance_to_smallest_Y<distance_to_middle_Y)
                    {
                        //smallest is closer
                        //turn towards the smallest
                    }

                }
                else
                {
                    //bigest is closer than the other two
                    //turn until this is not true
                }

            }
            return;
        }

        public int positiveResult(int a, int b)
        {
            int result;
            result = a - b;
            if (result<0)
            {
                result = result * -1;
            }
            return result;
        }
    }
}
