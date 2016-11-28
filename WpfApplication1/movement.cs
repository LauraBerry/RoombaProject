using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApplication1
{
    class movement
    {
        public logic logic = new logic();
        public MainWindow main = new MainWindow();
        public pathNode node = new pathNode();
       // public create roombaControls = new create();
        public string FaroreID = "FireFly-9E83";
        public string DinID = "FireFly-E5E8";
        public string NayruID = "FireFly-E5AE";
        public string password = "1234";
        public void init()
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
        }
        public void turn_toward_small(string ID)
        {
            //driveDirect(FaroreID, 0, 10);
            // or
            //driveDirect(FaroreID, 10, 0);
            //turn towards the smallest shape (left or right)
        }
        public void turn_towards_middle(string ID)
        {
            //driveDirect(FaroreID, 0, 10);
            // or
            //driveDirect(FaroreID, 10, 0);
            //turn towards the middle shape (left or right, opposit of small)
        }
        public void go_forward(string ID)
        {
            //go(FaroreID, 10, 0);
            //move forward
        }
        public void long_turn(string ID)
        {
            //driveDirect(FaroreID, 0, 30);
            //turn for a long time
        }
        //beep method maybe (like "i have arrived")
        
    }
}
