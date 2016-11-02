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
                //turn logic goes here
                if (turn_over == true)
                {
                    
                }
            }
        }
        /*
         * this is the code to clear the color from the board visually as the roomba moves
         */
        public void clear_behind_roomba(string color, int currX, int currY, int[,] board)
        {
            
        }
    }
}
