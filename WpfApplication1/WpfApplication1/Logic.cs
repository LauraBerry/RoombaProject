using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApplication1
{
    class Logic
    {
        bool turn_over=false;                            //is the turn over
        bool done = false;                              //are the roomba done moving (at their destination)
        MainWindow win= new MainWindow();
        /*
         * theoretically this is the logic that calls the roomba controls and does the collision logic
         */
        public void running()
        {
            while (done == false)
            {
                //look at current location
                //look at next location(if there is one)
                    //if no next location this roomba is done
                // identify how/if the roomba needs to turn
                //call code to turn the roomba apropriately 
                // call code to have roomba move
                //wait to see if there is a collision
                if (turn_over == true)
                {
                    //call code to remove color from block
                    //up date current location
                    //check if all 3 roombas are done
                }
            }
        }
    }
}
