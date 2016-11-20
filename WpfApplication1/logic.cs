using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApplication1
{
    class logic
    {
        public bool done=false;
        public int redLocation = 1;
        public int blueLocation = 1;
        public int greenLocation = 1;
        arrayClass arr = new arrayClass();

        /*
         * checks roomba location on array (so where it should be according to what the user drew in)
         * and compairs it to where the kinect camera sees it to be and adjusts roomba path to match
         * if it cannot find a next block that the roomba wants to go to it returns false otherwise 
         * it returns true.
         */
        public bool checkpath(String color)
        {
            int should_be_currX = 0;
            int should_be_currY = 0;
            int currX = 0;
            int CurrY = 0;
            //find current location
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (color.Equals("red"))
                    {
                        if (arr.red_path[i, j] == redLocation)
                        {
                            should_be_currX = i;
                            should_be_currY = j;
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
            }
            //find actual location (code for kinect should go here-ish)
            int a = should_be_currX;
            bool on_path = false;
            while (!on_path)
            {
                if (currX != a || CurrY != should_be_currY)
                {
                    //something to correct the roomba's path to fix it

                }
                else
                {
                    on_path = true;
                }
            }
            return true;

        }

        /*
         * will eventually loop infinitly until all 3 roombas are at their destinations as plotted out by the user
         * this may need to be re-worked to ensure that all 3 roombas run at the same time (for now we will focus on
         * getting one roomba running and tracking)
         */
        public void running(String color)
        {
            bool on_right_path = false;
            while (done == false)
            {
                on_right_path = checkpath(color);
                if (on_right_path==false)
                {
                    done = true;
                }
                redLocation++;
            }
        }
    }
}
