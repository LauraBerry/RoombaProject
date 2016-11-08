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
        bool roomba1_pressed;
        bool roomba2_pressed;
        bool roomba3_pressed;
        bool mouseClicked;
        arrayClass arr = new arrayClass();
        public MainWindow()
        {
            roomba1_pressed = false;
            roomba2_pressed = false;
            roomba3_pressed = false;
            mouseClicked = false;
            InitializeComponent();
            arr.init();
            
        }
        /*
         * checks if the mouse left button is clicked (used to allow for drag listeners)
         */
        private void mouse_clicked(object sender, MouseButtonEventArgs e)
        {
            mouseClicked = true;
        }
        private void mouseUnclicked(object sender, MouseButtonEventArgs e)
        {
            mouseClicked = false;
        }
        /*
         * listens for whish roomba is selected, ensures that only one is selected at a time
         */
        private void Din_clicked(object sender, RoutedEventArgs e)
        {
            if(roomba1_pressed==false)
            {
                roomba1_pressed = true;
                Roomba1.Width = 106;
                Roomba1.Margin = new Thickness(401,44,0,0);
                Roomba2.Margin = new Thickness(411, 78, 0, 0);
                roomba2_pressed = false;
                Roomba3.Margin = new Thickness(411, 112, 0, 0);
                roomba3_pressed = false;
            }
            else
            {
                
                Roomba1.Width = 96;
                Roomba1.Margin = new Thickness(411, 44, 0, 0);
                roomba1_pressed = false;
                roomba2_pressed = false;
                roomba3_pressed = false;
            }
        }

        private void Farore_clicked(object sender, RoutedEventArgs e)
        {
            if (roomba2_pressed == false)
            {
                roomba2_pressed = true;
                Roomba2.Width = 106;
                Roomba2.Margin = new Thickness(401, 78, 0, 0);
                Roomba1.Margin = new Thickness(411, 44, 0, 0);
                roomba1_pressed = false;
                Roomba3.Margin = new Thickness(411, 112, 0, 0);
                roomba3_pressed = false;
            }
            else
            {
                Roomba2.Width = 96;
                Roomba2.Margin = new Thickness(411, 78, 0, 0);
                roomba1_pressed = false;
                roomba2_pressed = false;
                roomba3_pressed = false;
            }
        }

        private void Nayru_clicked(object sender, RoutedEventArgs e)
        {
            if (roomba3_pressed == false)
            {
                roomba3_pressed = true;
                Roomba3.Width = 106;
                Roomba3.Margin = new Thickness(401, 112, 0, 0);
                Roomba1.Margin = new Thickness(411, 44, 0, 0);
                roomba1_pressed = false;
                Roomba2.Margin = new Thickness(411, 78, 0, 0);
                roomba2_pressed = false;
            }
            else
            {
                Roomba3.Width = 96;
                Roomba3.Margin = new Thickness(411, 112, 0, 0);
                roomba1_pressed = false;
                roomba2_pressed = false;
                roomba3_pressed = false;
            }
        }

       /*
        * listens for drag over grid blocks, sends grid block and array index to change method
        */
        private void selected_11(object sender, MouseEventArgs e)
        {
            if (mouseClicked == true)
            {
                change(Rec_11, 0, 0);
            }
        }
        private void selected_12(object sender, MouseEventArgs e)
        {
            if(mouseClicked==true)
            {
                change(Rec_12, 0, 1);
            }
        }
        private void selected_13(object sender, MouseEventArgs e)
        {
            if(mouseClicked==true)
            {
                change(Rec_13, 0, 2);
            }
        }
        private void selected_14(object sender, MouseEventArgs e)
        {
            if(mouseClicked==true)
            {
                change(Rec_14, 0, 3);
            }
        }
        private void selected_15(object sender, MouseEventArgs e)
        {
            if(mouseClicked==true)
            {
                change(Rec_15, 0, 4);
            }
        }
        private void selected_16(object sender, MouseEventArgs e)
        {
            if(mouseClicked==true)
            {
                change(Rec_16, 0, 5);
            }
        }
        private void selected_17(object sender, MouseEventArgs e)
        {
            if(mouseClicked==true)
            {
                change(Rec_17, 0, 6);
            }
        }
        private void selected_21(object sender, MouseEventArgs e)
        {
            if(mouseClicked==true)
            {
                change(Rec_21, 1, 0);
            }
        }
        private void selected_22(object sender, MouseEventArgs e)
        {
            if(mouseClicked==true)
            {
                change(Rec_22, 1, 1);
            }
        }
        private void selected_23(object sender, MouseEventArgs e)
        {
            if(mouseClicked==true)
            {
                change(Rec_23, 1, 2);
            }
        }
        private void selected_24(object sender, MouseEventArgs e)
        {
            if(mouseClicked==true)
            {
                change(Rec_24, 1, 3);
            }
        }
        private void selected_25(object sender, MouseEventArgs e)
        {
            if(mouseClicked==true)
            {
                change(Rec_25, 1, 4);
            }
        }
        private void selected_26(object sender, MouseEventArgs e)
        {
            if(mouseClicked==true)
            {
                change(Rec_26, 1, 5);
            }
        }
        private void selected_27(object sender, MouseEventArgs e)
        {
            if(mouseClicked==true)
            {
                change(Rec_27, 1, 6);
            }
        }
        private void selected_31(object sender, MouseEventArgs e)
        {
            if(mouseClicked==true)
            {
                change(Rec_31, 2, 0);
            }
        }
        private void selected_32(object sender, MouseEventArgs e)
        {
            if(mouseClicked==true)
            {
                change(Rec_32, 2, 1);
            }
        }
        private void selected_33(object sender, MouseEventArgs e)
        {
            if(mouseClicked==true)
            {
                change(Rec_33, 2, 2);
            }
        }
        private void selected_34(object sender, MouseEventArgs e)
        {
            if(mouseClicked==true)
            {
                change(Rec_34, 2, 3);
            }
        }
        private void selected_35(object sender, MouseEventArgs e)
        {
            if(mouseClicked==true)
            {
                change(Rec_35, 2, 4);
            }
        }
        private void selected_36(object sender, MouseEventArgs e)
        {
            if(mouseClicked==true)
            {
                change(Rec_36, 2, 5);
            }
        }
        private void selected_37(object sender, MouseEventArgs e)
        {
            if(mouseClicked==true)
            {
                change(Rec_37, 2, 6);
            }
        }
        private void selected_41(object sender, MouseEventArgs e)
        {
            if(mouseClicked==true)
            {
                change(Rec_41, 3, 0);
            }
        }
        private void selected_42(object sender, MouseEventArgs e)
        {
            if(mouseClicked==true)
            {
                change(Rec_42, 3, 1);
            }
        }
        private void selected_43(object sender, MouseEventArgs e)
        {
            if(mouseClicked==true)
            {
                change(Rec_43, 3, 2);
            }
        }
        private void selected_44(object sender, MouseEventArgs e)
        {
            if(mouseClicked==true)
            {
                change(Rec_44, 3, 3);
            }
        }
        private void selected_45(object sender, MouseEventArgs e)
        {
            if(mouseClicked==true)
            {
                change(Rec_45, 3, 4);
            }
        }
        private void selected_46(object sender, MouseEventArgs e)
        {
            if(mouseClicked==true)
            {
                change(Rec_46, 3, 5);
            }
        }
        private void selected_47(object sender, MouseEventArgs e)
        {
            if(mouseClicked==true)
            {
                change(Rec_47, 3, 6);
            }
        }
        private void selected_51(object sender, MouseEventArgs e)
        {
            if(mouseClicked==true)
            {
                change(Rec_51, 4, 0);
            }
        }
        private void selected_52(object sender, MouseEventArgs e)
        {
            if(mouseClicked==true)
            {
                change(Rec_52, 4, 1);
            }
        }
        private void selected_53(object sender, MouseEventArgs e)
        {
            if(mouseClicked==true)
            {
                change(Rec_53, 4, 2);
            }
        }
        private void selected_54(object sender, MouseEventArgs e)
        {
            if(mouseClicked==true)
            {
                change(Rec_54, 4, 3);
            }
        }
        private void selected_55(object sender, MouseEventArgs e)
        {
            if(mouseClicked==true)
            {
                change(Rec_55, 4, 4);
            }
        }
        private void selected_57(object sender, MouseEventArgs e)
        {
            if(mouseClicked==true)
            {
                change(Rec_57, 4, 6);
            }
        }

        private void selected_56(object sender, MouseEventArgs e)
        {
            if(mouseClicked==true)
            {
                change(Rec_56, 4, 5);
            }
        }
        //188,226,14 == yello
        //124,21,226==purple
        //11,162,94==aqua
        //58,58,58==black
        /*
         * changes the color of the rectangle based on which roomba is selected
         */
        public void change(Rectangle Rec, int x, int y)
        {
            if (roomba1_pressed == true)
            {
                if (arr.board[x, y] == 1)
                {
                    Rec.Fill = new SolidColorBrush(Color.FromRgb(244, 244, 245));
                    arr.board[x, y] = 0;
                }
                
                else if (arr.board[x, y] == 2)
                {
                    Rec.Fill = new SolidColorBrush(Color.FromRgb(124,21,226));
                    arr.board[x, y] = 4;
                }
                else if (arr.board[x, y] == 3)
                {
                    Rec.Fill = new SolidColorBrush(Color.FromRgb(188, 226, 14));
                    arr.board[x, y] = 5;
                }
                else if (arr.board[x, y] == 6)
                {
                    Rec.Fill = new SolidColorBrush(Color.FromRgb(58,58,58));
                    arr.board[x, y] = 7;
                }
                else if (arr.board[x, y] == 7)
                {
                    Rec.Fill = new SolidColorBrush(Color.FromRgb(11, 162, 94));
                    arr.board[x, y] = 6;
                }
                else if (arr.board[x, y] == 4)
                {
                    Rec.Fill = new SolidColorBrush(Color.FromRgb(13, 41, 168));
                    arr.board[x, y] = 2;
                }
                else if (arr.board[x, y] == 5)
                {
                    Rec.Fill = new SolidColorBrush(Color.FromRgb(7, 147, 0));
                    arr.board[x, y] = 3;
                }
                else
                {
                    Rec.Fill = new SolidColorBrush(Color.FromRgb(176, 5, 5));
                    arr.board[x, y] = 1;
                }
            }

            else if (roomba2_pressed == true)
            {
                if (arr.board[x, y] == 2)
                {
                    Rec.Fill = new SolidColorBrush(Color.FromRgb(244, 244, 245));
                    arr.board[x, y] = 0;
                }
                else if (arr.board[x, y] == 1)
                {
                    Rec.Fill = new SolidColorBrush(Color.FromRgb(124, 21, 226));
                    arr.board[x, y] = 4;
                }
                else if (arr.board[x, y] == 3)
                {
                    Rec.Fill = new SolidColorBrush(Color.FromRgb(11, 162, 94));
                    arr.board[x, y] = 6;
                }
                else if (arr.board[x, y] == 5)
                {
                    Rec.Fill = new SolidColorBrush(Color.FromRgb(58, 58, 58));
                    arr.board[x, y] = 7;
                }
                else if (arr.board[x, y] == 7)
                {
                    Rec.Fill = new SolidColorBrush(Color.FromRgb(188, 226, 14));
                    arr.board[x, y] = 5;
                }
                else if (arr.board[x, y] == 4)
                {
                    Rec.Fill = new SolidColorBrush(Color.FromRgb(176, 5, 5));
                    arr.board[x, y] = 1;
                }
                else if (arr.board[x, y] == 6)
                {
                    Rec.Fill = new SolidColorBrush(Color.FromRgb(7, 147, 0));
                    arr.board[x, y] = 3;
                }
                else
                {
                    Rec.Fill = new SolidColorBrush(Color.FromRgb(13, 41, 168));
                    arr.board[x, y] = 2;
                }
            }
            else if (roomba3_pressed == true)
            {
                if (arr.board[x, y] == 3)
                {
                    Rec.Fill = new SolidColorBrush(Color.FromRgb(244, 244, 245));
                    arr.board[x, y] = 0;
                }
                else if (arr.board[x, y] == 1)
                {
                    Rec.Fill = new SolidColorBrush(Color.FromRgb(188, 226, 14));
                    arr.board[x, y] = 5;
                }
                else if (arr.board[x, y] == 2)
                {
                    Rec.Fill = new SolidColorBrush(Color.FromRgb(11, 162, 94));
                    arr.board[x, y] = 6;
                }
                else if (arr.board[x, y] == 4)
                {
                    Rec.Fill = new SolidColorBrush(Color.FromRgb(58, 58, 58));
                    arr.board[x, y] = 7;
                }
                else if (arr.board[x, y] == 7)
                {
                    Rec.Fill = new SolidColorBrush(Color.FromRgb(124, 21, 226));
                    arr.board[x, y] = 4;
                }
                else if (arr.board[x, y] == 5)
                {
                    Rec.Fill = new SolidColorBrush(Color.FromRgb(176, 5, 5));
                    arr.board[x, y] = 1;
                }
                else if (arr.board[x, y] == 6)
                {
                    Rec.Fill = new SolidColorBrush(Color.FromRgb(13, 41, 168));
                    arr.board[x, y] = 2;
                }
                else
                {
                    Rec.Fill = new SolidColorBrush(Color.FromRgb(7, 147, 0));
                    arr.board[x, y] = 3;
                }
            }
        }
        /*
         * sets a block back to a blank state
         */
        public void clear_block(Rectangle rec, int x, int y)
        {
            rec.Fill = new SolidColorBrush(Color.FromRgb(244, 244, 244));
            arr.board[x, y] = 0;
        }

        /*
         * clears entier grid
         */
        private void clear_board(object sender, RoutedEventArgs e)
        {
            clear_block(Rec_11, 0, 0);
            clear_block(Rec_12, 0, 1);
            clear_block(Rec_13, 0, 2);
            clear_block(Rec_14, 0, 3);
            clear_block(Rec_15, 0, 4);
            clear_block(Rec_16, 0, 5);
            clear_block(Rec_17, 0, 6);
            clear_block(Rec_21, 1, 0);
            clear_block(Rec_22, 1, 1);
            clear_block(Rec_23, 1, 2);
            clear_block(Rec_24, 1, 3);
            clear_block(Rec_25, 1, 4);
            clear_block(Rec_26, 1, 5);
            clear_block(Rec_27, 1, 6);
            clear_block(Rec_31, 2, 0);
            clear_block(Rec_32, 2, 1);
            clear_block(Rec_33, 2, 2);
            clear_block(Rec_34, 2, 3);
            clear_block(Rec_35, 2, 4);
            clear_block(Rec_36, 2, 5);
            clear_block(Rec_37, 2, 6);
            clear_block(Rec_41, 3, 0);
            clear_block(Rec_42, 3, 1);
            clear_block(Rec_43, 3, 2);
            clear_block(Rec_44, 3, 3);
            clear_block(Rec_45, 3, 4);
            clear_block(Rec_46, 3, 5);
            clear_block(Rec_47, 3, 6);
            clear_block(Rec_51, 4, 0);
            clear_block(Rec_52, 4, 1);
            clear_block(Rec_53, 4, 2);
            clear_block(Rec_54, 4, 3);
            clear_block(Rec_55, 4, 4);
            clear_block(Rec_56, 4, 5);
            clear_block(Rec_57, 4, 6);
        }


    }
}
