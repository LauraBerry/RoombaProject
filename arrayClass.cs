﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApplication1
{
    public class arrayClass
    {
        //0== empty
        //1 == red, 2== blue, 3== green
        // 4== red and blue, 5== red and green 6== green and blue
        // 7== all three
       public  int[,] board = new int[4, 4];
       public int[,] red_path = new int[4, 4];
       public int[,] blue_path = new int[4, 4];
       public int[,] green_path = new int[4, 4];
       public void init()
       {
           int i=0;
           int j=0;
           while (i < 4)
           {
               while (j < 4)
               {
                   board[i,j] = 0;
                   red_path[i, j] = 0;
                   blue_path[i, j] = 0;
                   green_path[i, j] = 0;
                   j++;
               }
               j = 0;
               i++;
           }
       }
    }
}