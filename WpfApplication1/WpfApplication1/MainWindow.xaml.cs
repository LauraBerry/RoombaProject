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
        bool started;
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

        public void change( Rectangle Rec1, Rectangle Rec2, Rectangle Rec3, int x, int y)
        {
            if (roomba1_pressed == true)
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
            else if (roomba2_pressed == true)
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
            else if (roomba3_pressed == true)
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
        }
        private void clear_block(Rectangle Rec1, Rectangle Rec2, Rectangle Rec3, int x, int y)
        {
            Rec1.Fill = new SolidColorBrush(Color.FromRgb(7, 147, 0));
            Rec2.Fill = new SolidColorBrush(Color.FromRgb(7, 147, 0));
            Rec3.Fill = new SolidColorBrush(Color.FromRgb(7, 147, 0));
            arr.board[x, y] = 3;
        }
        private void start_clicke(object sender, RoutedEventArgs e)
        {
            started = true;
        }

        private void clear_grid(object sender, RoutedEventArgs e)
        {

        }
    }
}