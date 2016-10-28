using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApplication1
{
    public class arrayClass
    {
       public  int[,] board = new int[4, 4];
       public void init()
       {
           int i=0;
           int j=0;
           while (i < 4)
           {
               while (j < 4)
               {
                   board[i,j] = 0;
                   j++;
               }
               j = 0;
               i++;
           }
       }
    }
}
