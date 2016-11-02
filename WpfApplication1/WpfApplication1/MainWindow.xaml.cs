using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApplication1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>

    public partial class MainWindow : Window
    {
       public  bool roomba1_pressed;
       public bool roomba2_pressed;
       public bool roomba3_pressed;
        bool started;
        int val;
        byte c1;
        byte c2;
        byte c3;
        arrayClass arr = new arrayClass();
        public void init()
        {
            bool roomba1_pressed = false;
            bool roomba2_pressed = false;
            bool roomba3_pressed = false;
            bool started = false;
            arr.init();
        }
        public MainWindow()
        {
            InitializeComponent();
            init();
        }
        /*
         *roomba button listener changes location of roomba button selected and updates booleans roomba1_pressed,
         *roomba2_pressed and roomba3_pressed ensures that only 1 roomba is selected at a time.
         */
        private void button_click(object sender, RoutedEventArgs e)
        {
            if (roomba1_pressed == false)
            {
                Roomba1.Margin = new Thickness(425, 49, 0, 0);
                Roomba1.Width = 95;
                roomba1_pressed = true;

                Roomba2.Margin = new Thickness(435, 86, 0, 0);
                Roomba2.Width = 82;
                roomba2_pressed = false;

                Roomba3.Margin = new Thickness(435, 124, 0, 0);
                Roomba3.Width = 82;
                roomba3_pressed = false;
            }
            else
            {
                Roomba1.Margin = new Thickness(435, 49, 0, 0);
                Roomba1.Width = 82;
                roomba1_pressed = false;
                roomba2_pressed = false;
                roomba3_pressed = false;
            }

        }

        private void button_click1(object sender, RoutedEventArgs e)
        {
            if (roomba2_pressed == false)
            {
                Roomba2.Margin = new Thickness(425, 86, 0, 0);
                Roomba2.Width = 95;
                roomba2_pressed = true;

                Roomba1.Margin = new Thickness(435, 49, 0, 0);
                Roomba1.Width = 82;
                roomba1_pressed = false;

                Roomba3.Margin = new Thickness(435, 124, 0, 0);
                Roomba3.Width = 82;
                roomba3_pressed = false;
            }
            else
            {
                Roomba2.Margin = new Thickness(435, 86, 0, 0);
                Roomba2.Width = 82;
                roomba1_pressed = false;
                roomba2_pressed = false;
                roomba3_pressed = false;
            }

        }

        private void button_click2(object sender, RoutedEventArgs e)
        {
            if (roomba3_pressed == false)
            {
                Roomba3.Margin = new Thickness(425, 124, 0, 0);
                Roomba3.Width = 95;
                roomba3_pressed = true;

                Roomba2.Margin = new Thickness(435, 86, 0, 0);
                Roomba2.Width = 82;
                roomba2_pressed = false;

                Roomba1.Margin = new Thickness(435, 49, 0, 0);
                Roomba1.Width = 82;
                roomba1_pressed = false;
            }
            else
            {
                Roomba3.Margin = new Thickness(435, 124, 0, 0);
                Roomba3.Width = 82;
                roomba1_pressed = false;
                roomba2_pressed = false;
                roomba3_pressed = false;

            }
        }
        /*
         * Button click listeners for the grid, hands the rectangles and offset within the array to the color checker
         */
        private void grid_11_click(object sender, MouseButtonEventArgs e)
        {
            change(Rec_111, Rec_112, Rec_113, 0, 0);
        }
        private void click_12(object sender, MouseButtonEventArgs e)
        {
            change(Rec_121, Rec_122, Rec_123, 0, 1);
        }
        private void click_13(object sender, MouseButtonEventArgs e)
        {
            change(Rec_131, Rec_132, Rec_133, 0, 2);
        }
        private void click_14(object sender, MouseButtonEventArgs e)
        {
            change(Rec_141, Rec_142, Rec_143,0, 3);
        }
        private void click_21(object sender, MouseButtonEventArgs e)
        {
            change(Rec_211, Rec_212, Rec_213, 1, 0);
        }
        private void click_22(object sender, MouseButtonEventArgs e)
        {
            change(Rec_221, Rec_222, Rec_223, 1, 1);
        }
        private void click_23(object sender, MouseButtonEventArgs e)
        {
            change(Rec_231, Rec_232, Rec_233,1 ,2);
        }
        private void click_24(object sender, MouseButtonEventArgs e)
        {
            change(Rec_241, Rec_242, Rec_243, 1, 3);
        }

        private void click_31(object sender, MouseButtonEventArgs e)
        {
            change(Rec_311, Rec_312, Rec_313,2, 0);
        }

        private void click_32(object sender, MouseButtonEventArgs e)
        {
            change(Rec_321, Rec_322, Rec_323, 2, 1);
        }

        private void click_33(object sender, MouseButtonEventArgs e)
        {
            change(Rec_331, Rec_332, Rec_333, 2, 2);
        }

        private void click_34(object sender, MouseButtonEventArgs e)
        {
            change(Rec_341, Rec_342, Rec_343, 2, 3);
        }

        private void click_41(object sender, MouseButtonEventArgs e)
        {
            change(Rec_411, Rec_412, Rec_413, 3, 0);
        }

        private void click_42(object sender, MouseButtonEventArgs e)
        {
            change(Rec_421, Rec_422, Rec_423, 3, 1);
        }

        private void click_43(object sender, MouseButtonEventArgs e)
        {
            change(Rec_431, Rec_432, Rec_433, 3, 2);
        }

        private void click_44(object sender, MouseButtonEventArgs e)
        {
            change(Rec_441, Rec_442, Rec_443, 3, 3);
        }
        /*
         * color change checker takes in the rectangles and the offset of the board index and updates the color accordingly
         */
        public void change( Rectangle Rec1, Rectangle Rec2, Rectangle Rec3, int x, int y)
        {
            if (roomba1_pressed == true)                            //is the red roomba selected
            {
                if (arr.board[x, y] == 2)                           //block is already red
                {
                    val=4;
                    c1=13;
                    c2=41;
                    c3=168;
                    color_block(Rec1,  Rec2,  Rec3,  x,  y, val, c1, c2, c3);
                    c1 = 176;
                    c2 = 5;
                    c3 = 5;
                    color_1Rec(Rec3, x, y, c1, c2, c3);
                }
                else if (arr.board[x, y] == 3)                              //board is already green
                {
                    val = 5;
                    c1 = 7;
                    c2 = 147;
                    c3 = 0;
                    color_block(Rec1, Rec2, Rec3, x, y, val, c1, c2, c3);
                    c1 = 176;
                    c2 = 5;
                    c3 = 5;
                    color_1Rec(Rec3, x, y, c1, c2, c3);
                    
                }
                else if (arr.board[x, y] == 6)                              //block is alread red and green
                {
                    val = 7;
                    c1 = 7;
                    c2 = 147;
                    c3 = 0;
                    color_block(Rec1, Rec2, Rec3, x, y, val, c1, c2, c3);
                    c1 = 13;
                    c2 = 41;
                    c3 = 168;
                    color_1Rec(Rec3, x, y, c1, c2, c3);
                    c1 = 176;
                    c2 = 5;
                    c3 = 5;
                    color_1Rec(Rec2, x, y, c1, c2, c3);
                }
                else if (arr.board[x, y] == 7)                             //block is already red, green and blue
                {
                    val = 6;
                    c1 = 7;
                    c2 = 147;
                    c3 = 0;
                    color_block(Rec1, Rec2, Rec3, x, y, val, c1, c2, c3);
                    c1 = 13;
                    c2 = 41;
                    c3 = 168;
                    color_1Rec(Rec3, x, y, c1, c2, c3);
                }
                else if (arr.board[x, y] == 4)                                  //block is already red and blue
                  {
                      val = 2;
                      c1 = 13;
                      c2 = 41;
                      c3 = 168;
                      color_block(Rec1, Rec2, Rec3, x, y, val, c1, c2, c3);
                }  
                else if (arr.board[x, y] == 5)                                  //block is already red and green
                {
                    val = 3;
                    c1 = 7;
                    c2 = 147;
                    c3 = 0;
                    color_block(Rec1, Rec2, Rec3, x, y, val, c1, c2, c3);
                    
                }
                else if (arr.board[x, y] == 1)                                  //block is already red
                {
                    clear_block(Rec1, Rec2, Rec3, x, y);
                }
                else
                {
                    val = 1;
                    c1 = 176;
                    c2 = 5;
                    c3 = 5;
                    color_block(Rec1, Rec2, Rec3, x, y, val, c1, c2, c3);
                }
            }
            else if (roomba2_pressed == true)                               //is the blue roomba selected
            {
                if (arr.board[x, y] == 1)                                   //block is already red
                {
                    val = 4;
                    c1 = 176;
                    c2 = 5;
                    c3 = 5;
                    color_block(Rec1, Rec2, Rec3, x, y, val, c1, c2, c3);
                    c1 = 13;
                    c2 = 41;
                    c3 = 168;
                    color_1Rec(Rec1, x, y, c1, c2, c3);
                }
                else if (arr.board[x, y] == 3)                              //block is already green
                {
                    val = 6;
                    c1 = 7;
                    c2 = 147;
                    c3 = 0;
                    color_block(Rec1, Rec2, Rec3, x, y, val, c1, c2, c3);
                    c1 = 13;
                    c2 = 41;
                    c3 = 168;
                    color_1Rec(Rec3, x, y, c1, c2, c3);
                }
                else if (arr.board[x, y] == 5)                          //block is alread red and green
                {
                    val = 7;
                    c1 = 7;
                    c2 = 147;
                    c3 = 0;
                    color_block(Rec1, Rec2, Rec3, x, y, val, c1, c2, c3);
                    c1 = 13;
                    c2 = 41;
                    c3 = 168;
                    color_1Rec(Rec3, x, y, c1, c2, c3);
                    c1 = 176;
                    c2 = 5;
                    c3 = 5;
                    color_1Rec(Rec2, x, y, c1, c2, c3);
                }
                else if (arr.board[x, y] == 7)                          //block is already red, green and blue
                {
                    val = 5;
                    c1 = 7;
                    c2 = 147;
                    c3 = 0;
                    color_block(Rec1, Rec2, Rec3, x, y, val, c1, c2, c3);
                    c1 = 176;
                    c2 = 5;
                    c3 = 5;
                    color_1Rec(Rec3, x, y, c1, c2, c3);
                }
                else if (arr.board[x, y] == 4)                              //block is already red and blue
                {
                    val = 1;
                    c1 = 176;
                    c2 = 5;
                    c3 = 5;
                    color_block(Rec1, Rec2, Rec3, x, y, val, c1, c2, c3);
                }
                else if (arr.board[x, y] == 6)                             //block is already green and blue
                {
                    val = 3;
                    c1 = 7;
                    c2 = 147;
                    c3 = 0;
                    color_block(Rec1, Rec2, Rec3, x, y, val, c1, c2, c3);
                }
                else if (arr.board[x, y] == 2)                          //block is already blue
                {
                    clear_block(Rec1, Rec2, Rec3, x, y);
                }
                else
                {
                    val = 2;
                    c1 = 13;
                    c2 = 41;
                    c3 = 168;
                    color_block(Rec1, Rec2, Rec3, x, y, val, c1, c2, c3);
                }
            }
            else if (roomba3_pressed == true)                               //is the green roomba selected
            {
                if (arr.board[x, y] == 1)                                   //block is already red
                {
                    val = 5;
                    c1 = 7;
                    c2 = 147;
                    c3 = 0;
                    color_block(Rec1, Rec2, Rec3, x, y, val, c1, c2, c3);
                    c1 = 176;
                    c2 = 5;
                    c3 = 5;
                    color_1Rec(Rec3, x, y, c1, c2, c3);
                }
                else if (arr.board[x, y] == 2)                              //block is already blue
                {
                    val = 6;
                    c1 = 7;
                    c2 = 147;
                    c3 = 0;
                    color_block(Rec1, Rec2, Rec3, x, y, val, c1, c2, c3);
                    c1 = 13;
                    c2 = 41;
                    c3 = 168;
                    color_1Rec(Rec3, x, y, c1, c2, c3);
                }
                else if (arr.board[x, y] == 4)                      //block is already red and blue
                {
                    val = 7;
                    c1 = 7;
                    c2 = 147;
                    c3 = 0;
                    color_block(Rec1, Rec2, Rec3, x, y, val, c1, c2, c3);
                    c1 = 13;
                    c2 = 41;
                    c3 = 168;
                    color_1Rec(Rec3, x, y, c1, c2, c3);
                    c1 = 176;
                    c2 = 5;
                    c3 = 5;
                    color_1Rec(Rec2, x, y, c1, c2, c3);
                }
                else if (arr.board[x, y] == 7)                          //block is already red, green and blue
                {
                    val = 4;
                    c1 = 176;
                    c2 = 5;
                    c3 = 5;
                    color_block(Rec1, Rec2, Rec3, x, y, val, c1, c2, c3);
                    c1 = 13;
                    c2 = 41;
                    c3 = 168;
                    color_1Rec(Rec1, x, y, c1, c2, c3);
                }
                else if (arr.board[x, y] == 5)                            //block is already red and green
                {
                    val = 1;
                    c1 = 176;
                    c2 = 5;
                    c3 = 5;
                    color_block(Rec1, Rec2, Rec3, x, y, val, c1, c2, c3);
                }
                else if (arr.board[x, y] == 6)                          //block is already green and blue
                {
                    val = 2;
                    c1 = 13;
                    c2 = 41;
                    c3 = 168;
                    color_block(Rec1, Rec2, Rec3, x, y, val, c1, c2, c3);
                }
                else if (arr.board[x, y] == 3)                      //block is already green
                {
                    clear_block(Rec1, Rec2, Rec3, x, y);
                }
                else
                {
                    val = 3;
                    c1 = 7;
                    c2 = 147;
                    c3 = 0;
                    color_block(Rec1, Rec2, Rec3, x, y, val, c1, c2, c3);
                }
            }
        }
        /*
         * takes in rectangles and offset into board array and clears block so that it goes back to grey
         */
        public void clear_block(Rectangle Rec1, Rectangle Rec2, Rectangle Rec3, int x, int y)
        {
            Rec1.Fill = new SolidColorBrush(Color.FromRgb(244, 244, 245));
            Rec2.Fill = new SolidColorBrush(Color.FromRgb(244, 244, 245));
            Rec3.Fill = new SolidColorBrush(Color.FromRgb(244, 244, 245));
            arr.board[x, y] = 0;
        }
        /*
         * takes in rectangles, offset to board, value for board array and color values and fills all 3 rectangles with that color
         */
        public void color_block(Rectangle Rec1, Rectangle Rec2, Rectangle Rec3, int x, int y, int val, byte c1, byte c2, byte c3)
        {
            Rec1.Fill = new SolidColorBrush(Color.FromRgb(c1, c2, c3));
            Rec2.Fill = new SolidColorBrush(Color.FromRgb(c1, c2, c3));
            Rec3.Fill = new SolidColorBrush(Color.FromRgb(c1, c2, c3));
            arr.board[x, y] = val;
        }
        /*
         * takes 1 rectangle and fills it with the color given
         */
        public void color_1Rec(Rectangle Rec, int x, int y, byte c1, byte c2, byte c3)
        {
            Rec.Fill = new SolidColorBrush(Color.FromRgb(c1, c2, c3));  
        }
        /*
         * clears the entier grid
         */
        private void clear_grid(object sender, RoutedEventArgs e)
        {

            clear_block(Rec_111, Rec_112, Rec_113, 0, 0);
            clear_block(Rec_121, Rec_122, Rec_123, 0, 1);
            clear_block(Rec_131, Rec_132, Rec_133, 0, 2);
            clear_block(Rec_141, Rec_142, Rec_143, 0, 3);
            clear_block(Rec_211, Rec_212, Rec_213, 1, 0);
            clear_block(Rec_221, Rec_222, Rec_223, 1, 1);
            clear_block(Rec_231, Rec_232, Rec_233, 1, 2);
            clear_block(Rec_241, Rec_242, Rec_243, 1, 3);
            clear_block(Rec_311, Rec_312, Rec_313, 2, 0);
            clear_block(Rec_321, Rec_322, Rec_323, 2, 1);
            clear_block(Rec_331, Rec_332, Rec_333, 2, 2);
            clear_block(Rec_341, Rec_342, Rec_343, 2, 3);
            clear_block(Rec_411, Rec_412, Rec_413, 3, 0);
            clear_block(Rec_421, Rec_422, Rec_423, 3, 1);
            clear_block(Rec_431, Rec_432, Rec_433, 3, 2);
            clear_block(Rec_441, Rec_442, Rec_443, 3, 3);
        }

        private void start_clicke(object sender, RoutedEventArgs e)
        {
            started = true;
        }

       public void findRecs(int x, int y, string color)
        {
            Rectangle R1;
            Rectangle R2;
            Rectangle R3;
           if(x==0)
           {
                if(y==0)
                {
                    R1 = Rec_111;
                    R2 = Rec_112;
                    R3 = Rec_113;
                }
                else if(y==1)
                {
                    R1 = Rec_121;
                    R2 = Rec_122;
                    R3 = Rec_123;
                }
                else if (y==2)
                {
                    R1 = Rec_131;
                    R2 = Rec_132;
                    R3 = Rec_133;
                }
                else if (y==3)
                {
                    R1 = Rec_141;
                    R2 = Rec_142;
                    R3 = Rec_143;
                }
           }
           else if (x==1)
           {
               if(y==0)
               {
                   R1 = Rec_211;
                   R2 = Rec_212;
                   R3 = Rec_213;
               }
               else if (y==1)
               {
                   R1 = Rec_221;
                   R2 = Rec_222;
                   R3 = Rec_223;
               }
               else if (y==2)
               {
                   R1 = Rec_231;
                   R2 = Rec_232;
                   R3 = Rec_233;
               }
               else if (y==3)
               {
                   R1 = Rec_241;
                   R2 = Rec_242;
                   R3 = Rec_243;
               }
           }
           else if (x==2)
           {
               if (y == 0)
               {
                   R1 = Rec_311;
                   R2 = Rec_312;
                   R3 = Rec_313;
               }
               else if (y==1)
               {
                   R1 = Rec_321;
                   R2 = Rec_322;
                   R3 = Rec_323;
               }
               else if (y==2)
               {
                   R1 = Rec_331;
                   R2 = Rec_332;
                   R3 = Rec_333;
               }
               else if (y==3)
               {
                   R1 = Rec_341;
                   R2 = Rec_342;
                   R3 = Rec_343;
               }
           }
           else if(x==3)
           {
               if(y==0)
               {
                   R1 = Rec_411;
                   R2 = Rec_412;
                   R3 = Rec_413;
               }
               else if (y==1)
               {
                   R1 = Rec_421;
                   R2 = Rec_422;
                   R3 = Rec_423;
               }
               else if (y==2)
               {
                   R1 = Rec_431;
                   R2 = Rec_432;
                   R3 = Rec_433;
               }
               else if (y==3)
               {
                   R1 = Rec_441;
                   R2 = Rec_442;
                   R3 = Rec_443;
               }
           }
        }

        public void change_postRoomba(Rectangle Rec1, Rectangle Rec2, Rectangle Rec3,  string color, int x, int y)
        {
            if (String.ReferenceEquals(color, "red"))                   //roomba that moved was the red roomba
            {
                if(arr.board[x,y]==1)                                   //block was red
                {
                    clear_block(Rec1, Rec2, Rec3, x, y);
                }
                else if (arr.board[x,y]==4)                             //block was red and blue
                {
                    val = 2;
                    c1 = 13;
                    c2 = 41;
                    c3 = 168;
                    color_block(Rec1, Rec2, Rec3, x, y, val, c1, c2, c3);
                }
                else if (arr.board[x,y]==5)                         //block was red and green
                {
                    val = 3;
                    c1 = 7;
                    c2 = 147;
                    c3 = 0;
                    color_block(Rec1, Rec2, Rec3, x, y, val, c1, c2, c3);
                }
                else if (arr.board[x,y]==7)                     //block was red,green and blue
                {
                    val = 6;
                    c1 = 7;
                    c2 = 147;
                    c3 = 0;
                    color_block(Rec1, Rec2, Rec3, x, y, val, c1, c2, c3);
                    c1 = 13;
                    c2 = 41;
                    c3 = 168;
                    color_1Rec(Rec3, x, y, c1, c2, c3);
                }
            }
            else if (String.ReferenceEquals(color, "blue"))
            {
                if (arr.board[x, y] == 2)                                   //block was blue
                {
                    clear_block(Rec1, Rec2, Rec3, x, y);
                }
                else if (arr.board[x, y] == 4)                              //block was red and blue
                {
                    val = 1;
                    c1 = 176;
                    c2 = 5;
                    c3 = 5;
                    color_block(Rec1, Rec2, Rec3, x, y, val, c1, c2, c3);
                }
                else if (arr.board[x, y] == 6)                              //block was blue and green
                {
                    val = 3;
                    c1 = 7;
                    c2 = 147;
                    c3 = 0;
                    color_block(Rec1, Rec2, Rec3, x, y, val, c1, c2, c3);
                }
                else if (arr.board[x, y] == 7)                            //block was red, blue and green
                {
                    val = 5;
                    c1 = 7;
                    c2 = 147;
                    c3 = 0;
                    color_block(Rec1, Rec2, Rec3, x, y, val, c1, c2, c3);
                    c1 = 176;
                    c2 = 5;
                    c3 = 5;
                    color_1Rec(Rec3, x, y, c1, c2, c3);
                }
            }
            else if (String.ReferenceEquals(color, "green"))
            {

                if(arr.board[x, y] == 3)                                    //block was green
                {
                    clear_block(Rec1, Rec2, Rec3, x, y);
                }
                else if (arr.board[x, y] == 5)                              //block was red and green
                {
                    val = 1;
                    c1 = 176;
                    c2 = 5;
                    c3 = 5;
                    color_block(Rec1, Rec2, Rec3, x, y, val, c1, c2, c3);
                }
                else if (arr.board[x,y]==6)                                 //block was green and blue
                {
                    val = 2;
                    c1 = 13;
                    c2 = 41;
                    c3 = 168;
                    color_block(Rec1, Rec2, Rec3, x, y, val, c1, c2, c3);
                }
                else if (arr.board[x,y]==7)                                 //block was red, blue and green
                {
                    val = 4;
                    c1 = 13;
                    c2 = 41;
                    c3 = 168;
                    color_block(Rec1, Rec2, Rec3, x, y, val, c1, c2, c3);
                    c1 = 176;
                    c2 = 5;
                    c3 = 5;
                    color_1Rec(Rec3, x, y, c1, c2, c3);
                }
            }
        }
    }
}