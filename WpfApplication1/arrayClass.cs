﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApplication1
{
    class arrayClass
    {
        //0== empty
        //1 == red, 2== blue, 3== green
        // 4== red and blue, 5== red and green 6== green and blue
        // 7== all three
        public int[,] board = new int[5, 7];
        public void init()
        {
            for (int i = 0; i < 5; i++)
            { 
                for (int j=0; j<7; j++)
                {
                    board[i, j] = 0;
                }
            }
            return;
        }
    }
}
