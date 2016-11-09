﻿using System;
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
        int currRed;
        int currBlue;
        int currGreen;
        arrayClass arr = new arrayClass();
        public MainWindow()
        {
            roomba1_pressed = false;
            roomba2_pressed = false;
            roomba3_pressed = false;
            mouseClicked = false;
            currBlue = 1;
            currGreen = 1;
            currRed = 1;
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
                clear_button.Background = new SolidColorBrush(Color.FromRgb(176, 5, 5));
            }
            else
            {
                
                Roomba1.Width = 96;
                Roomba1.Margin = new Thickness(411, 44, 0, 0);
                roomba1_pressed = false;
                roomba2_pressed = false;
                roomba3_pressed = false;
                clear_button.Background = new SolidColorBrush(Color.FromRgb(221,221,221));
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
                clear_button.Background = new SolidColorBrush(Color.FromRgb(13, 41, 168));
            }
            else
            {
                Roomba2.Width = 96;
                Roomba2.Margin = new Thickness(411, 78, 0, 0);
                roomba1_pressed = false;
                roomba2_pressed = false;
                roomba3_pressed = false;
                clear_button.Background = new SolidColorBrush(Color.FromRgb(221, 221, 221));
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
                clear_button.Background = new SolidColorBrush(Color.FromRgb(7, 147, 0));
            }
            else
            {
                Roomba3.Width = 96;
                Roomba3.Margin = new Thickness(411, 112, 0, 0);
                roomba1_pressed = false;
                roomba2_pressed = false;
                roomba3_pressed = false;
                clear_button.Background = new SolidColorBrush(Color.FromRgb(221, 221, 221));
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
        public void remove_red(int x, int y, Rectangle Rec)
        {
            if (arr.board[x, y] == 1)
            {
                Rec.Fill = new SolidColorBrush(Color.FromRgb(244, 244, 245));
                arr.board[x, y] = 0;
                currRed = remove_path(arr.red_path, x, y, currRed);
            }
            else if (arr.board[x,y] == 7)
            {
                Rec.Fill = new SolidColorBrush(Color.FromRgb(11, 162, 94));
                arr.board[x, y] = 6;
                currRed = remove_path(arr.red_path, x, y, currRed);
            }
            else if (arr.board[x, y] == 4)
            {
                Rec.Fill = new SolidColorBrush(Color.FromRgb(13, 41, 168));
                arr.board[x, y] = 2;
                currRed = remove_path(arr.red_path, x, y, currRed);
            }
            else if (arr.board[x, y] == 5)
            {
                Rec.Fill = new SolidColorBrush(Color.FromRgb(7, 147, 0));
                arr.board[x, y] = 3;
                currRed = remove_path(arr.red_path, x, y, currRed);
            }
        }

        public void remove_blue(int x, int y, Rectangle Rec)
        {
            if (arr.board[x, y] == 2)
            {
                Rec.Fill = new SolidColorBrush(Color.FromRgb(244, 244, 245));
                arr.board[x, y] = 0;
                currBlue = remove_path(arr.blue_path, x, y, currBlue);
            }
            else if (arr.board[x, y] == 7)
            {
                Rec.Fill = new SolidColorBrush(Color.FromRgb(188, 226, 14));
                arr.board[x, y] = 5;
                currBlue = remove_path(arr.blue_path, x, y, currBlue);
            }
            else if (arr.board[x, y] == 4)
            {
                Rec.Fill = new SolidColorBrush(Color.FromRgb(176, 5, 5));
                arr.board[x, y] = 1;
                currBlue = remove_path(arr.blue_path, x, y, currBlue);
            }
            else if (arr.board[x, y] == 6)
            {
                Rec.Fill = new SolidColorBrush(Color.FromRgb(7, 147, 0));
                arr.board[x, y] = 3;
                currBlue = remove_path(arr.blue_path, x, y, currBlue);
            }
        }
        public void remove_green(int x, int y, Rectangle Rec)
        {
            if (arr.board[x, y] == 3)
            {
                Rec.Fill = new SolidColorBrush(Color.FromRgb(244, 244, 245));
                arr.board[x, y] = 0;
                currGreen = remove_path(arr.green_path, x, y, currGreen);
            }
            else if (arr.board[x, y] == 7)
            {
                Rec.Fill = new SolidColorBrush(Color.FromRgb(124, 21, 226));
                arr.board[x, y] = 4;
                currGreen = remove_path(arr.green_path, x, y, currGreen);
            }
            else if (arr.board[x, y] == 5)
            {
                Rec.Fill = new SolidColorBrush(Color.FromRgb(176, 5, 5));
                arr.board[x, y] = 1;
                currGreen = remove_path(arr.green_path, x, y, currGreen);
            }
            else if (arr.board[x, y] == 6)
            {
                Rec.Fill = new SolidColorBrush(Color.FromRgb(13, 41, 168));
                arr.board[x, y] = 2;
                currGreen = remove_path(arr.green_path, x, y, currGreen);
            }
        }

        public void change(Rectangle Rec, int x, int y)
        {
            if (roomba1_pressed == true)
            {
                if (arr.board[x, y] == 2)
                {
                    Rec.Fill = new SolidColorBrush(Color.FromRgb(124,21,226));
                    arr.board[x, y] = 4;
                    currRed = add_path(arr.red_path, x, y, currRed);
                }
                else if (arr.board[x, y] == 3)
                {
                    Rec.Fill = new SolidColorBrush(Color.FromRgb(188, 226, 14));
                    arr.board[x, y] = 5;
                    currRed = add_path(arr.red_path, x, y, currRed);
                }
                else if (arr.board[x, y] == 6)
                {
                    Rec.Fill = new SolidColorBrush(Color.FromRgb(58,58,58));
                    arr.board[x, y] = 7;
                    currRed = add_path(arr.red_path, x, y, currRed);
                }
                else if (arr.board[x,y]==0)
                {
                    Rec.Fill = new SolidColorBrush(Color.FromRgb(176, 5, 5));
                    arr.board[x, y] = 1;
                    currRed = add_path(arr.red_path, x, y, currRed);
                }
                else
                {
                    remove_red(x, y, Rec);
                }
            }

            else if (roomba2_pressed == true)
            {
                if (arr.board[x, y] == 1)
                {
                    Rec.Fill = new SolidColorBrush(Color.FromRgb(124, 21, 226));
                    arr.board[x, y] = 4;
                    currBlue = add_path(arr.blue_path, x, y, currBlue);
                }
                else if (arr.board[x, y] == 3)
                {
                    Rec.Fill = new SolidColorBrush(Color.FromRgb(11, 162, 94));
                    arr.board[x, y] = 6;
                    currBlue = add_path(arr.blue_path, x, y, currBlue);
                }
                else if (arr.board[x, y] == 5)
                {
                    Rec.Fill = new SolidColorBrush(Color.FromRgb(58, 58, 58));
                    arr.board[x, y] = 7;
                    currBlue = add_path(arr.blue_path, x, y, currBlue);
                }
                else if (arr.board[x,y]==0)
                {
                    Rec.Fill = new SolidColorBrush(Color.FromRgb(13, 41, 168));
                    arr.board[x, y] = 2;
                    currBlue = add_path(arr.blue_path, x, y, currBlue);
                }
                else
                {
                    remove_blue(x, y, Rec);
                }
            }
            else if (roomba3_pressed == true)
            {
                if (arr.board[x, y] == 1)
                {
                    Rec.Fill = new SolidColorBrush(Color.FromRgb(188, 226, 14));
                    arr.board[x, y] = 5;
                    currGreen = add_path(arr.green_path, x, y, currGreen);
                }
                else if (arr.board[x, y] == 2)
                {
                    Rec.Fill = new SolidColorBrush(Color.FromRgb(11, 162, 94));
                    arr.board[x, y] = 6;
                    currGreen = add_path(arr.green_path, x, y, currGreen);
                }
                else if (arr.board[x, y] == 4)
                {
                    Rec.Fill = new SolidColorBrush(Color.FromRgb(58, 58, 58));
                    arr.board[x, y] = 7;
                    currGreen = add_path(arr.green_path, x, y, currGreen);
                }
                else if (arr.board[x,y]==0)
                {
                    Rec.Fill = new SolidColorBrush(Color.FromRgb(7, 147, 0));
                    arr.board[x, y] = 3;
                    currGreen = add_path(arr.green_path, x, y, currGreen);
                }
                else
                {
                    remove_green(x, y, Rec);
                }
            }
        }
        public int remove_path(int[,] a,int x, int y, int val)
        {
            a[x, y] = 0;
            val--;
            return val;
        }
        public int add_path(int[,] a, int x, int y, int val)
        {
            a[x, y] = val;
            val++;
            return val;
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
            if(roomba1_pressed==true)
            {
                remove_red(0, 0, Rec_11);
                remove_red(0, 1, Rec_12);
                remove_red(0, 2,Rec_13);
                remove_red(0, 3, Rec_14);
                remove_red(0, 4, Rec_15);
                remove_red(0, 5, Rec_16);
                remove_red(0, 6, Rec_17);
                remove_red(1, 0, Rec_21);
                remove_red(1, 1, Rec_22);
                remove_red(1, 2, Rec_23);
                remove_red(1, 3, Rec_24);
                remove_red(1, 4, Rec_25);
                remove_red(1, 5, Rec_26);
                remove_red(1, 6, Rec_27);
                remove_red(2, 0, Rec_31);
                remove_red(2, 1, Rec_32);
                remove_red(2, 2, Rec_33);
                remove_red(2, 3, Rec_34);
                remove_red(2, 4, Rec_35);
                remove_red(2, 5,Rec_36);
                remove_red(2, 6, Rec_37);
                remove_red(3, 0, Rec_41);
                remove_red(3, 1,Rec_42);
                remove_red(3, 2, Rec_43);
                remove_red(3, 3, Rec_44);
                remove_red(3, 4, Rec_45);
                remove_red(3, 5,Rec_46);
                remove_red(3, 6, Rec_47);
                remove_red(4, 0, Rec_51);
                remove_red(4, 1, Rec_52);
                remove_red(4, 2, Rec_53);
                remove_red(4, 3, Rec_54);
                remove_red(4, 4, Rec_55);
                remove_red(4, 5, Rec_56);
                remove_red(4, 6, Rec_57);
            }
            else if (roomba2_pressed == true)
            {
                remove_blue(0, 0, Rec_11);
                remove_blue(0, 1, Rec_12);
                remove_blue(0, 2, Rec_13);
                remove_blue(0, 3, Rec_14);
                remove_blue(0, 4, Rec_15);
                remove_blue(0, 5, Rec_16);
                remove_blue(0, 6, Rec_17);
                remove_blue(1, 0, Rec_21);
                remove_blue(1, 1, Rec_22);
                remove_blue(1, 2, Rec_23);
                remove_blue(1, 3, Rec_24);
                remove_blue(1, 4, Rec_25);
                remove_blue(1, 5, Rec_26);
                remove_blue(1, 6, Rec_27);
                remove_blue(2, 0, Rec_31);
                remove_blue(2, 1, Rec_32);
                remove_blue(2, 2, Rec_33);
                remove_blue(2, 3, Rec_34);
                remove_blue(2, 4, Rec_35);
                remove_blue(2, 5, Rec_36);
                remove_blue(2, 6, Rec_37);
                remove_blue(3, 0, Rec_41);
                remove_blue(3, 1, Rec_42);
                remove_blue(3, 2, Rec_43);
                remove_blue(3, 3, Rec_44);
                remove_blue(3, 4, Rec_45);
                remove_blue(3, 5, Rec_46);
                remove_blue(3, 6, Rec_47);
                remove_blue(4, 0, Rec_51);
                remove_blue(4, 1, Rec_52);
                remove_blue(4, 2, Rec_53);
                remove_blue(4, 3, Rec_54);
                remove_blue(4, 4, Rec_55);
                remove_blue(4, 5, Rec_56);
                remove_blue(4, 6, Rec_57);
            }
            else if (roomba3_pressed == true)
            {
                remove_green(0, 0, Rec_11);
                remove_green(0, 1, Rec_12);
                remove_green(0, 2, Rec_13);
                remove_green(0, 3, Rec_14);
                remove_green(0, 4, Rec_15);
                remove_green(0, 5, Rec_16);
                remove_green(0, 6, Rec_17);
                remove_green(1, 0, Rec_21);
                remove_green(1, 1, Rec_22);
                remove_green(1, 2, Rec_23);
                remove_green(1, 3, Rec_24);
                remove_green(1, 4, Rec_25);
                remove_green(1, 5, Rec_26);
                remove_green(1, 6, Rec_27);
                remove_green(2, 0, Rec_31);
                remove_green(2, 1, Rec_32);
                remove_green(2, 2, Rec_33);
                remove_green(2, 3, Rec_34);
                remove_green(2, 4, Rec_35);
                remove_green(2, 5, Rec_36);
                remove_green(2, 6, Rec_37);
                remove_green(3, 0, Rec_41);
                remove_green(3, 1, Rec_42);
                remove_green(3, 2, Rec_43);
                remove_green(3, 3, Rec_44);
                remove_green(3, 4, Rec_45);
                remove_green(3, 5, Rec_46);
                remove_green(3, 6, Rec_47);
                remove_green(4, 0, Rec_51);
                remove_green(4, 1, Rec_52);
                remove_green(4, 2, Rec_53);
                remove_green(4, 3, Rec_54);
                remove_green(4, 4, Rec_55);
                remove_green(4, 5, Rec_56);
                remove_green(4, 6, Rec_57);
            }
            else
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
        
        /*
         * this well eventuall call the logic class and run the program but for not it prints out the path arrays to the console
         */
        private void start_clicked(object sender, RoutedEventArgs e)
        {
            roomba1_pressed = false;
            roomba2_pressed = false;
            roomba3_pressed = false;
            Roomba1.Margin = new Thickness(411, 44, 0, 0);
            Roomba2.Margin = new Thickness(411, 78, 0, 0);
            Roomba3.Margin = new Thickness(411, 112, 0, 0);
            clear_button.Background = new SolidColorBrush(Color.FromRgb(221, 221, 221));

            for (int i=0;i<3;i++)
            {
                if(i==0)
                {
                    Console.Write("currRed: ");
                    Console.WriteLine(currRed);
                    Console.WriteLine("Red Roomba: ");
                    for (int j=0; j<5;j++)
                    {
                        for (int k=0;k<7;k++)
                        {
                            Console.Write(arr.red_path[j, k]);
                        }
                        Console.WriteLine("");
                    }
                }
                else if (i == 1)
                {
                    Console.Write("currblue: ");
                    Console.WriteLine(currBlue);
                    Console.WriteLine("Blue Roomba: ");
                    for (int j = 0; j < 5; j++)
                    {
                        for (int k = 0; k < 7; k++)
                        {
                            Console.Write(arr.blue_path[j, k]);
                        }
                        Console.WriteLine("");
                    }
                }
                else if (i == 2)
                {
                    Console.Write("currGreen: ");
                    Console.WriteLine(currGreen);
                    Console.WriteLine("Green Roomba: ");
                    for (int j = 0; j < 5; j++)
                    {
                        for (int k = 0; k < 7; k++)
                        {
                            Console.Write(arr.green_path[j, k]);
                        }
                        Console.WriteLine("");
                    }
                }
            }
        }


    }
}