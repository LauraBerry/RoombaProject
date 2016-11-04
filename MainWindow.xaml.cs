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
        Rectangle R1;
        Rectangle R2;
        Rectangle R3;
        int currRed;
        int currBlue;
        int currGreen;
        bool roomba1_pressed;
        bool roomba2_pressed;
        bool roomba3_pressed;
        bool started;
        arrayClass arr = new arrayClass();

        public void init()
        {
            bool roomba1_pressed = false;
            bool roomba2_pressed = false;
            bool roomba3_pressed = false;
            bool started = false;
            currRed=1;
            currBlue=1;
            currGreen=1;
            arr.init();
        }
        public MainWindow()
        {
            InitializeComponent();
            init();
        }

        /*
         * button listeners for each of the roomba tabs, updates the margins so the selected roomba
         * pops out of alignment and the unselected roombas pop back into alignment and updates the
         * selected roomba's boolean variable
         */
        private void button_click(object sender, RoutedEventArgs e)
        {
            if (roomba1_pressed == false)
            {
                Roomba1.Margin = new Thickness(425, 43, 0, 0);
                Roomba1.Width = 95;
                roomba1_pressed = true;

                Roomba2.Margin = new Thickness(435, 80, 0, 0);
                Roomba2.Width = 82;
                roomba2_pressed = false;

                Roomba3.Margin = new Thickness(435, 118, 0, 0);
                Roomba3.Width = 82;
                roomba3_pressed = false;
            }
            else
            {
                Roomba1.Margin = new Thickness(435, 43, 0, 0);
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
                Roomba2.Margin = new Thickness(425, 80, 0, 0);
                Roomba2.Width = 95;
                roomba2_pressed = true;

                Roomba1.Margin = new Thickness(435, 43, 0, 0);
                Roomba1.Width = 82;
                roomba1_pressed = false;

                Roomba3.Margin = new Thickness(435, 118, 0, 0);
                Roomba3.Width = 82;
                roomba3_pressed = false;
            }
            else
            {
                Roomba2.Margin = new Thickness(435, 80, 0, 0);
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
                Roomba3.Margin = new Thickness(425, 118, 0, 0);
                Roomba3.Width = 95;
                roomba3_pressed = true;

                Roomba2.Margin = new Thickness(435, 80, 0, 0);
                Roomba2.Width = 82;
                roomba2_pressed = false;

                Roomba1.Margin = new Thickness(435, 43, 0, 0);
                Roomba1.Width = 82;
                roomba1_pressed = false;
            }
            else
            {
                Roomba3.Margin = new Thickness(435, 118, 0, 0);
                Roomba3.Width = 82;
                roomba1_pressed = false;
                roomba2_pressed = false;
                roomba3_pressed = false;

            }
        }

        /*
         * listeners for each grid block, sends rectangle names and array co-ordinates to change method
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
         * checks which roomba is selected, what color the block selected is and if the selected block is adjacent to one of the selected 
         * roomba's color and updates the block color accordingly
         */
        public void change( Rectangle Rec1, Rectangle Rec2, Rectangle Rec3, int x, int y)
        {
            bool adjacent = is_adjacent(x, y, 1);
            if (roomba1_pressed == true)
            {
                if (adjacent == true)
                {
                    if (arr.board[x, y] == 2)
                    {
                        Rec1.Fill = new SolidColorBrush(Color.FromRgb(13, 41, 168));
                        Rec2.Fill = new SolidColorBrush(Color.FromRgb(13, 41, 168));
                        Rec3.Fill = new SolidColorBrush(Color.FromRgb(176, 5, 5));
                        arr.board[x, y] = 4;
                    }
                    else if (arr.board[x, y] == 3)
                    {
                        Rec1.Fill = new SolidColorBrush(Color.FromRgb(7, 147, 0));
                        Rec2.Fill = new SolidColorBrush(Color.FromRgb(7, 147, 0));
                        Rec3.Fill = new SolidColorBrush(Color.FromRgb(176, 5, 5));
                        arr.board[x, y] = 5;
                    }
                    else if (arr.board[x, y] == 6)
                    {
                        Rec1.Fill = new SolidColorBrush(Color.FromRgb(7, 147, 0));
                        Rec2.Fill = new SolidColorBrush(Color.FromRgb(13, 41, 168));
                        Rec3.Fill = new SolidColorBrush(Color.FromRgb(176, 5, 5));
                        arr.board[x, y] = 7;
                    }
                    else if (arr.board[x, y] == 7)
                    {
                        Rec1.Fill = new SolidColorBrush(Color.FromRgb(7, 147, 0));
                        Rec2.Fill = new SolidColorBrush(Color.FromRgb(7, 147, 0));
                        Rec3.Fill = new SolidColorBrush(Color.FromRgb(13, 41, 168));
                        arr.board[x, y] = 6;
                    }
                    else if (arr.board[x, y] == 4)
                    {
                        Rec1.Fill = new SolidColorBrush(Color.FromRgb(13, 41, 168));
                        Rec2.Fill = new SolidColorBrush(Color.FromRgb(13, 41, 168));
                        Rec3.Fill = new SolidColorBrush(Color.FromRgb(13, 41, 168));
                        arr.board[x, y] = 2;
                    }
                    else if (arr.board[x, y] == 5)
                    {
                        Rec1.Fill = new SolidColorBrush(Color.FromRgb(7, 147, 0));
                        Rec2.Fill = new SolidColorBrush(Color.FromRgb(7, 147, 0));
                        Rec3.Fill = new SolidColorBrush(Color.FromRgb(7, 147, 0));
                        arr.board[x, y] = 3;
                    }
                    else if (arr.board[x, y] == 1)
                    {
                        Rec1.Fill = new SolidColorBrush(Color.FromRgb(244, 244, 245));
                        Rec2.Fill = new SolidColorBrush(Color.FromRgb(244, 244, 245));
                        Rec3.Fill = new SolidColorBrush(Color.FromRgb(244, 244, 245));
                        arr.board[x, y] = 0;
                    }
                    else
                    {
                        Rec1.Fill = new SolidColorBrush(Color.FromRgb(176, 5, 5));
                        Rec2.Fill = new SolidColorBrush(Color.FromRgb(176, 5, 5));
                        Rec3.Fill = new SolidColorBrush(Color.FromRgb(176, 5, 5));
                        arr.board[x, y] = 1;
                    }
                }
                else
                {
                    error_msg_adjacent.Visibility = Visibility.Visible;
                    close_error_msg_adjacent.Visibility = Visibility.Visible;
                }
            }
            else if (roomba2_pressed == true)
            {
                adjacent = is_adjacent(x, y, 2);
                if (adjacent == true)
                {
                    if (arr.board[x, y] == 1)
                    {
                        Rec1.Fill = new SolidColorBrush(Color.FromRgb(13, 41, 168));
                        Rec2.Fill = new SolidColorBrush(Color.FromRgb(176, 5, 5));
                        Rec3.Fill = new SolidColorBrush(Color.FromRgb(176, 5, 5));
                        arr.board[x, y] = 4;
                    }
                    else if (arr.board[x, y] == 3)
                    {
                        Rec1.Fill = new SolidColorBrush(Color.FromRgb(7, 147, 0));
                        Rec2.Fill = new SolidColorBrush(Color.FromRgb(7, 147, 0));
                        Rec3.Fill = new SolidColorBrush(Color.FromRgb(13, 41, 168));
                        arr.board[x, y] = 6;
                    }
                    else if (arr.board[x, y] == 5)
                    {
                        Rec1.Fill = new SolidColorBrush(Color.FromRgb(7, 147, 0));
                        Rec2.Fill = new SolidColorBrush(Color.FromRgb(13, 41, 168));
                        Rec3.Fill = new SolidColorBrush(Color.FromRgb(176, 5, 5));
                        arr.board[x, y] = 7;
                    }
                    else if (arr.board[x, y] == 7)
                    {
                        Rec1.Fill = new SolidColorBrush(Color.FromRgb(7, 147, 0));
                        Rec2.Fill = new SolidColorBrush(Color.FromRgb(7, 147, 0));
                        Rec3.Fill = new SolidColorBrush(Color.FromRgb(176, 5, 5));
                        arr.board[x, y] = 5;
                    }
                    else if (arr.board[x, y] == 4)
                    {
                        Rec1.Fill = new SolidColorBrush(Color.FromRgb(176, 5, 5));
                        Rec2.Fill = new SolidColorBrush(Color.FromRgb(176, 5, 5));
                        Rec3.Fill = new SolidColorBrush(Color.FromRgb(176, 5, 5));
                        arr.board[x, y] = 1;
                    }
                    else if (arr.board[x, y] == 6)
                    {
                        Rec1.Fill = new SolidColorBrush(Color.FromRgb(7, 147, 0));
                        Rec2.Fill = new SolidColorBrush(Color.FromRgb(7, 147, 0));
                        Rec3.Fill = new SolidColorBrush(Color.FromRgb(7, 147, 0));
                        arr.board[x, y] = 3;
                    }
                    else if (arr.board[x, y] == 2)
                    {
                        Rec1.Fill = new SolidColorBrush(Color.FromRgb(244, 244, 245));
                        Rec2.Fill = new SolidColorBrush(Color.FromRgb(244, 244, 245));
                        Rec3.Fill = new SolidColorBrush(Color.FromRgb(244, 244, 245));
                        arr.board[x, y] = 0;
                    }
                    else
                    {
                        Rec1.Fill = new SolidColorBrush(Color.FromRgb(13, 41, 168));
                        Rec2.Fill = new SolidColorBrush(Color.FromRgb(13, 41, 168));
                        Rec3.Fill = new SolidColorBrush(Color.FromRgb(13, 41, 168));
                        arr.board[x, y] = 2;
                    }
                }
                else
                {
                    error_msg_adjacent.Visibility = Visibility.Visible;
                    close_error_msg_adjacent.Visibility = Visibility.Visible;
                }
            }
            else if (roomba3_pressed == true)
            {
                adjacent = is_adjacent(x, y, 3);
                if (adjacent == true)
                {
                    if (arr.board[x, y] == 1)
                    {
                        Rec1.Fill = new SolidColorBrush(Color.FromRgb(7, 147, 0));
                        Rec2.Fill = new SolidColorBrush(Color.FromRgb(7, 147, 0));
                        Rec3.Fill = new SolidColorBrush(Color.FromRgb(176, 5, 5));
                        arr.board[x, y] = 5;
                    }
                    else if (arr.board[x, y] == 2)
                    {
                        Rec1.Fill = new SolidColorBrush(Color.FromRgb(7, 147, 0));
                        Rec2.Fill = new SolidColorBrush(Color.FromRgb(7, 147, 0));
                        Rec3.Fill = new SolidColorBrush(Color.FromRgb(13, 41, 168));
                        arr.board[x, y] = 6;
                    }
                    else if (arr.board[x, y] == 4)
                    {
                        Rec1.Fill = new SolidColorBrush(Color.FromRgb(7, 147, 0));
                        Rec2.Fill = new SolidColorBrush(Color.FromRgb(13, 41, 168));
                        Rec3.Fill = new SolidColorBrush(Color.FromRgb(176, 5, 5));
                        arr.board[x, y] = 7;
                    }
                    else if (arr.board[x, y] == 7)
                    {
                        Rec1.Fill = new SolidColorBrush(Color.FromRgb(13, 41, 168));
                        Rec2.Fill = new SolidColorBrush(Color.FromRgb(13, 41, 168));
                        Rec3.Fill = new SolidColorBrush(Color.FromRgb(176, 5, 5));
                        arr.board[x, y] = 4;
                    }
                    else if (arr.board[x, y] == 5)
                    {
                        Rec1.Fill = new SolidColorBrush(Color.FromRgb(176, 5, 5));
                        Rec2.Fill = new SolidColorBrush(Color.FromRgb(176, 5, 5));
                        Rec3.Fill = new SolidColorBrush(Color.FromRgb(176, 5, 5));
                        arr.board[x, y] = 1;
                    }
                    else if (arr.board[x, y] == 6)
                    {
                        Rec1.Fill = new SolidColorBrush(Color.FromRgb(13, 41, 168));
                        Rec2.Fill = new SolidColorBrush(Color.FromRgb(13, 41, 168));
                        Rec3.Fill = new SolidColorBrush(Color.FromRgb(13, 41, 168));
                        arr.board[x, y] = 2;
                    }
                    else if (arr.board[x, y] == 3)
                    {
                        Rec1.Fill = new SolidColorBrush(Color.FromRgb(244, 244, 245));
                        Rec2.Fill = new SolidColorBrush(Color.FromRgb(244, 244, 245));
                        Rec3.Fill = new SolidColorBrush(Color.FromRgb(244, 244, 245));
                        arr.board[x, y] = 0;
                    }
                    else
                    {
                        Rec1.Fill = new SolidColorBrush(Color.FromRgb(7, 147, 0));
                        Rec2.Fill = new SolidColorBrush(Color.FromRgb(7, 147, 0));
                        Rec3.Fill = new SolidColorBrush(Color.FromRgb(7, 147, 0));
                        arr.board[x, y] = 3;
                    }
                }
                else
                {
                    error_msg_adjacent.Visibility = Visibility.Visible;
                    close_error_msg_adjacent.Visibility = Visibility.Visible;
                }
            }
        }

        /*
         * check the color of the selected block relative to that in variable "val"
         */
        private bool check_color_match(int x, int y, int val)
        {
            //is the variable within the range of the array?
            if(x<0||x>3)
            {
                return false;
            }
            if (y<0||y>3)
            {
                return false;
            }
            //is the selected block the same color as val?
            if (arr.board[x,y]==val)
            {
                return true;
            }
            //is the selected block a color that includes the one in val
              if (val==1)                                                    //red
              {
                if (arr.board[x,y]==4)                                      //red and blue
                {
                    return true;
                }
                else if (arr.board[x,y]==5)                                 //red and green
                {
                    return true;
                }
                else if (arr.board[x,y]==7)                                 //red, blue and green
                {
                    return true;
                }
            }
            else if (val==2)                                                //blue
            {
                if (arr.board[x,y]==4)                                      //red and blue
                {
                    return true;
                }
                else if (arr.board[x,y]==6)                                 //blue and green
                {
                    return true;
                }
                else if (arr.board[x,y]==7)                                 //red, blue and green
                {
                    return true;
                }
            }
            else if (val==3)                                                //green
            {
                if (arr.board[x,y]==5)                                      //red and green
                {
                    return true;
                }
                else if (arr.board[x,y]==6)                                 //blue and green
                {
                    return true;
                }
                else if (arr.board[x,y]==7)                                 //red, blue and green
                {
                    return true;
                }
            }
              return false;
        }
        /*
         * checks if selected block is adjacent to another block of the same color
         */
        private bool is_adjacent(int x, int y, int val)
        {
            bool above =check_color_match((x-1),y,val);
            bool below=check_color_match((x+1),y,val);
            bool left = check_color_match(x,(y-1),val);
            bool right = check_color_match (x,y+1,val);
            if (x==3 && y==0)                                                 //check upper right hand corner
            {  
                if (above||right)
                {
                    return true;
                }
            }
            else if (x==3 && y==3)                                               //check lower right hand corner
            {
                if (below||left)
                {
                    return true;
                }
            }
            else if (x==0 &&y==0)                                                //check lower left hand corner
            {
                if (roomba1_pressed==true)
                {
                    return true;
                }
            }
            else if (x==0 && y==3)                                              //check the lower right hand corner
            {
                if (below||left)
                {
                    return true;
                }
            }
            else if (x == 0)                                                         //check top row
            {
                if (y == 0 && roomba1_pressed==true)
                {
                    return true;
                }
                else if (y == 1 && roomba2_pressed==true)
                {
                    return true;
                }
                else if (y == 2 && roomba3_pressed==true)
                {
                    return true;
                }
                else if (below||left||right)
                {
                    return true;
                }
            }
            else if (x==3)                                                                         //check bottom row
            {
                if (above|| left||right)
                {
                    return true;
                }
            }
            else  if (y==0)                                                                       //check left row
            {
                if (below|| above||right)
                {
                    return true;
                }
            }
            else if (y==3)
            {
                if (below || above||left)                                                         //check right hand row
                {
                    return true;
                }
            }
            if (above || below || left || right)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /*
         * clears the block so it is empty and updates the array value
         */
        private void clear_block(Rectangle Rec1, Rectangle Rec2, Rectangle Rec3, int x, int y)
        {
            Rec1.Fill = new SolidColorBrush(Color.FromRgb(244, 244, 245));
            Rec2.Fill = new SolidColorBrush(Color.FromRgb(244, 244, 245));
            Rec3.Fill = new SolidColorBrush(Color.FromRgb(244, 244, 245));
            arr.board[x, y] = 0;
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

        /*
         * 
         */StartupEventArgs button has been clicked
        private void start_clicked(object sender, RoutedEventArgs e)
        {
            started = true;
            //unselects all roombas
            Roomba2.Margin = new Thickness(435, 80, 0, 0);
            Roomba2.Width = 82;
            roomba2_pressed = false;

            Roomba3.Margin = new Thickness(435, 118, 0, 0);
            Roomba3.Width = 82;
            roomba3_pressed = false;

            Roomba1.Margin = new Thickness(435, 43, 0, 0);
            Roomba1.Width = 82;
            roomba1_pressed = false;
           

        }

        /*
         * colapses the error message for the adjacency when the x button is clicked
         */
        private void close_error_msg_adjacent_clicked(object sender, RoutedEventArgs e)
        {
            error_msg_adjacent.Visibility = Visibility.Collapsed;
            close_error_msg_adjacent.Visibility = Visibility.Collapsed;
        }
       /* public void get_rectangles(int x, int y)
        {
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
                else
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
                else if(y==1)
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
                else
                {
                    R1 = Rec_241;
                    R2 = Rec_242;
                    R3 = Rec_243;
                }
            }
            else if (x==2)
            {
                if(y==0)
                {
                    R1 = Rec_311;
                    R2 = Rec_312;
                    R3 = Rec_313;
                }
                else if(y==1)
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
                else
                {
                    R1 = Rec_341;
                    R2 = Rec_342;
                    R3 = Rec_343;
                }
            }
            else if (x==3)
            {
                if(y==0)
                {
                    R1 = Rec_411;
                    R2 = Rec_412;
                    R3 = Rec_413;
                }
                else if(y==1)
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
                else
                {
                    R1 = Rec_441;
                    R2 = Rec_442;
                    R3 = Rec_443;
                }
            }
        }
        /*
         * this is the code to clear the color from the board visually as the roomba moves
         */
       /* public void clear_behind_roomba(string color, int currX, int currY, int[,] board)
        {
            get_rectangles(currX, currY);
            if(String.ReferenceEquals(color, "red"))                            //we are dealing with the red roomba
            {
                if(board[currX,currY]==1)                                   //if the block is red
                {
                    clear_block(R1, R2, R3, currX, currY);
                }
                else if (board[currX,currY]==4)                             //if the block is red and blue
                {
                    R1.Fill = new SolidColorBrush(Color.FromRgb(13, 41, 168));
                    R2.Fill = new SolidColorBrush(Color.FromRgb(13, 41, 168));
                    R3.Fill = new SolidColorBrush(Color.FromRgb(13, 41, 168));
                    arr.board[currX, currY] = 2;
                }
                else if (arr.board[currX,currY]==5)                          // if the block is red and green
                {
                    R1.Fill = new SolidColorBrush(Color.FromRgb(7, 147, 0));
                    R2.Fill = new SolidColorBrush(Color.FromRgb(7, 147, 0));
                    R3.Fill = new SolidColorBrush(Color.FromRgb(7, 147, 0));
                    arr.board[currX, currY] = 3;
                }
                else if (board[currX,currY]==7)                         //if the block is red, green and blue
                {
                    R1.Fill = new SolidColorBrush(Color.FromRgb(7, 147, 0));
                    R2.Fill = new SolidColorBrush(Color.FromRgb(7, 147, 0));
                    R3.Fill = new SolidColorBrush(Color.FromRgb(13, 41, 168));
                    arr.board[currX, currY] = 6;
                }
            }
            else if (String.ReferenceEquals(color, "blue"))             //we are dealing with the blue roomba
            {
                if (board[currX, currY] == 2)                                   //if the block is blue
                {
                    clear_block(R1, R2, R3, currX, currY);
                }
                else if (board[currX, currY] == 4)                             //if the block is red and blue
                {
                    R1.Fill = new SolidColorBrush(Color.FromRgb(176, 5, 5));
                    R2.Fill = new SolidColorBrush(Color.FromRgb(176, 5, 5));
                    R3.Fill = new SolidColorBrush(Color.FromRgb(176, 5, 5));
                    arr.board[currX, currY] = 1;
                }
                else if (board[currX, currY] == 6)                          // if the block is blue and green
                {
                    R1.Fill = new SolidColorBrush(Color.FromRgb(7, 147, 0));
                    R2.Fill = new SolidColorBrush(Color.FromRgb(7, 147, 0));
                    R3.Fill = new SolidColorBrush(Color.FromRgb(7, 147, 0));
                    arr.board[currX, currY] = 3;
                }
                else if (board[currX, currY] == 7)                         //if the block is red, green and blue
                {
                    R1.Fill = new SolidColorBrush(Color.FromRgb(7, 147, 0));
                    R2.Fill = new SolidColorBrush(Color.FromRgb(7, 147, 0));
                    R3.Fill = new SolidColorBrush(Color.FromRgb(176, 5, 5));
                    arr.board[currX, currY] = 6;
                }
            }
            else if (String.ReferenceEquals(color, "green"))                    //we are dealing with the green roomba
            {
                if (board[currX, currY] == 3)                                   //if the block is green
                {
                    clear_block(R1, R2, R3, currX, currY);
                }
                else if (arr.board[currX, currY] == 6)                             //if the block is red and green
                {
                    R1.Fill = new SolidColorBrush(Color.FromRgb(176, 5, 5));
                    R2.Fill = new SolidColorBrush(Color.FromRgb(176, 5, 5));
                    R3.Fill = new SolidColorBrush(Color.FromRgb(176, 5, 5));
                    arr.board[currX, currY] = 1;
                }
                else if (board[currX, currY] == 6)                          // if the block is blue and green
                {
                    R1.Fill = new SolidColorBrush(Color.FromRgb(13, 41, 168));
                    R2.Fill = new SolidColorBrush(Color.FromRgb(13, 41, 168));
                    R3.Fill = new SolidColorBrush(Color.FromRgb(13, 41, 168));
                    arr.board[currX, currY] = 2;
                }
                else if (board[currX, currY] == 7)                         //if the block is red, green and blue
                {
                    R1.Fill = new SolidColorBrush(Color.FromRgb(13, 41, 168));
                    R2.Fill = new SolidColorBrush(Color.FromRgb(13, 41, 168));
                    R3.Fill = new SolidColorBrush(Color.FromRgb(176, 5, 5));
                    arr.board[currX, currY] = 4;
                }
            }
        }*/
       
    }
}