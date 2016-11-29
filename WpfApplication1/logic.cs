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
        public string FaroreID = "FireFly-9E83";
        public string DinID = "FireFly-E5E8";
        public string NayruID = "FireFly-E5AE";
        public string password = "1234";
        arrayClass arr = new arrayClass();
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
                        go_forward(FaroreID);
                        //go forward
                    }
                    else
                    {
                        long_turn(FaroreID);
                        //long turn
                    }
                }
                else
                {
                    if (distance_to_middle < distance_to_smallest)
                    {
                        turn_towards_middle(FaroreID);
                        //turn towards middle
                    }
                    else if (distance_to_smallest<distance_to_middle)
                    {
                        turn_toward_small(FaroreID);
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

        public void init_contorls()
        {
            /*
             * how do we start? this my guess is.
             * _init_(self, FaroreID, startingMode=SAFE_MODE, sim_mode = False);
             * _init_(self, DinID, startingMode=SAFE_MODE, sim_mode = False);
             * _init_(self, NayruID, startingMode=SAFE_MODE, sim_mode = False);
             */
        }
        public void stop(string ID)
        {
                //stop(FaroreID);
                //stop roomba
                blue1.taken = false;                //set taken to false so that on next frame the x and y coordinates of the object will be updated
                blue2.taken = false;
                blue3.taken = false;
                return;
        }
        public void turn_toward_small(string ID)
        {
  //driveDirect(FaroreID, 0, 10);
                // or
                //driveDirect(FaroreID, 10, 0);
                //turn towards the smallest shape (left or right)
                blue1.taken = false;                //set taken to false so that on next frame the x and y coordinates of the object will be updated
                blue2.taken = false;
                blue3.taken = false;
                return;

        }
        public void turn_towards_middle(string ID)
        {

                //driveDirect(FaroreID, 0, 10);
                // or
                //driveDirect(FaroreID, 10, 0);
                //turn towards the middle shape (left or right, opposit of small)
                blue1.taken = false;                //set taken to false so that on next frame the x and y coordinates of the object will be updated
                blue2.taken = false;
                blue3.taken = false;
                return;
        }
        public void go_forward(string ID)
        {

                //go(FaroreID, 10, 0);
                //move forward
                blue1.taken = false;                //set taken to false so that on next frame the x and y coordinates of the object will be updated
                blue2.taken = false;
                blue3.taken = false;
                return;
        }
        public void long_turn(string ID)
        {

                //driveDirect(FaroreID, 0, 30);
                //turn for a long time
                blue1.taken = false;                //set taken to false so that on next frame the x and y coordinates of the object will be updated
                blue2.taken = false;
                blue3.taken = false;
                return;
        }
    }
}
