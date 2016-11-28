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
        public movement roombaControl = new movement();
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
                //wait
                return;
            }
            else
            {
                object_seen[] arr;
                arr = new object_seen[3];
                arr = findBiggest(a, b, c);
                //math stuff
                double distance_to_biggest;
                double distance_to_middle;  
                double distance_to_smallest;

                distance_to_biggest = System.Math.Sqrt(System.Math.Pow((arr[0].xCoord - destinationX), 2.0) + System.Math.Pow((arr[0].yCoord - destinationY), 2.0));
                distance_to_middle = System.Math.Sqrt(System.Math.Pow((arr[1].xCoord - destinationX), 2.0) + System.Math.Pow((arr[1].yCoord - destinationY), 2.0));
                distance_to_smallest = System.Math.Sqrt(System.Math.Pow((arr[2].xCoord - destinationX), 2.0) + System.Math.Pow((arr[2].yCoord - destinationY), 2.0));

                if (positiveResult(distance_to_middle, distance_to_smallest) < 10)
                {
                    if (distance_to_smallest < distance_to_biggest && distance_to_middle < distance_to_biggest)
                    {
                        roombaControl.go_forward(roombaControl.FaroreID);
                        //go forward
                    }
                    else
                    {
                        roombaControl.long_turn(roombaControl.FaroreID);
                        //long turn
                    }
                }
                else
                {
                    if (distance_to_middle < distance_to_smallest)
                    {
                        roombaControl.turn_towards_middle(roombaControl.FaroreID);
                        //turn towards middle
                    }
                    else if (distance_to_smallest<distance_to_middle)
                    {
                        roombaControl.turn_toward_small(roombaControl.FaroreID);
                        //turn towards smallest
                    }
                }
            }
            return;
        }

        public double positiveResult(double a, double b)
        {
            double result;
            result = a - b;
            if (result<0)
            {
                result = result * -1;
            }
            return result;
        }
    }
}
