using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApplication1
{
    //use socket not websocket
    //use 5 digit port number to avoid conflicts

    class logic
    {
        public bool Bluedone = false;
        public bool Reddone = false;
        public bool Blackdone = false;
        public object_seen blue1;
        public object_seen blue2;
        public object_seen blue3;
        public object_seen red1;
        public object_seen red2;
        public object_seen red3;
        public object_seen green1;
        public object_seen green2;
        public object_seen green3;
        public string FaroreID = "FireFly-9E83";
        public string DinID = "FireFly-E5E8";
        public string NayruID = "FireFly-E5AE";
        public string password = "1234";
        arrayClass arr = new arrayClass();
        MainWindow main;
        
        //struct to hold things the camera recognizes in the frame
        public struct object_seen
        {
            public int xCoord;                                  //location x-coordinate
            public int yCoord;                                  // location y-coordinate
            public int height;                                  //height of object
            public int width;                                   //width of object
            public System.Drawing.Color color;                  // color of object
            public bool taken;                                  //is this object filled?
        }
        /*
         * initalizes the object_seen structs such that the taken value is false
         */
        public void init()
        {
            blue1 = new object_seen();
            blue2 = new object_seen();
            blue3 = new object_seen();
            reInit(blue1, blue2, blue3);

            red1 = new object_seen();
            red2 = new object_seen();
            red3 = new object_seen();
            reInit(red1, red2, red3);

            green1 = new object_seen();
            green2 = new object_seen();
            green3 = new object_seen();
            reInit(green1, green2, green3);
        }

        public void reInit(object_seen a, object_seen b, object_seen c)
        {
            a.taken = false;
            a.height = 2; 
            b.taken = false;
            b.height = 5;
            c.taken = false;
            c.height = 7;
        }

        public bool helperMethod(object_seen a, object_seen b, object_seen c, int x, int y)
        {
            if (a.taken == false)
            {
                if (a.height != b.height)
                {
                    if (a.height != c.height)
                    {
                        return true;
                    }
                }
            }
            return false;
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
        public void get_barring(object_seen a, object_seen b, object_seen c, int destinationX, int destinationY, bool d, String color)
        {
            
            if(d==false)
            {
                return;
            }
            bool isNull = three_filled(a, b, c);
            if (isNull==false)
            {
                //not all are filled
                //wait
                return;
            }
            else
            {
                //math stuff
                double distance_to_biggest;
                double distance_to_middle;  
                double distance_to_smallest;

                distance_to_biggest = System.Math.Sqrt(System.Math.Pow((c.xCoord - destinationX), 2.0) + System.Math.Pow((c.yCoord - destinationY), 2.0));
                distance_to_middle = System.Math.Sqrt(System.Math.Pow((b.xCoord - destinationX), 2.0) + System.Math.Pow((b.yCoord - destinationY), 2.0));
                distance_to_smallest = System.Math.Sqrt(System.Math.Pow((a.xCoord - destinationX), 2.0) + System.Math.Pow((a.yCoord - destinationY), 2.0));

                if(positiveResult(distance_to_biggest, distance_to_middle)<20 && positiveResult(distance_to_biggest, distance_to_smallest)<20)
                {
                    if (color == "blue")
                    {
                        Bluedone = true;
                    }
                    else if (color == "red")
                    {
                        Reddone = true;
                    }
                    else if (color == "black")
                    {
                        Blackdone = true;
                    }
                    //at the destination
                    //stop
                }
                if (positiveResult(distance_to_middle, distance_to_smallest) < 25)
                {
                    if (distance_to_smallest < distance_to_biggest)
                    {
                        Console.WriteLine("go forward");
                        csclient.StartClient(color, "f");
                        //go forward
                    }
                    else
                    {
                        Console.WriteLine("long turn");
                        csclient.StartClient(color, "a");
                        //long turn
                    }
                }
                else
                {
                    if (distance_to_middle < distance_to_smallest)
                    {
                        Console.WriteLine("turn towards middle");
                        csclient.StartClient(color, "r");
                        //turn towards middle
                    }
                    else if (distance_to_smallest<distance_to_middle)
                    {
                        Console.WriteLine("turn towards smallest");
                        csclient.StartClient(color, "l");
                        //turn towards smallest
                    }
                }
            }
            reInit(a, b, c);
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
