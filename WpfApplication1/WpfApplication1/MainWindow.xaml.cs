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
        bool roomba1_pressed = false;
        bool roomba2_pressed = false;
        bool roomba3_pressed = false;
        arrayClass arr = new arrayClass();
        public MainWindow()
        {
            InitializeComponent();
        }
        private void button_click(object sender, RoutedEventArgs e)
        {
            if(roomba1_pressed==false)
            {
                Roomba1.Margin = new Thickness(425, 49, 0, 0);
                Roomba1.Width = 95;
                roomba1_pressed=true;

                Roomba2.Margin = new Thickness(435, 86, 0, 0);
                Roomba2.Width = 82;
                roomba2_pressed = false;

                Roomba3.Margin = new Thickness(435, 124, 0, 0);
                Roomba3.Width = 82;
                roomba3_pressed = false;
            }
            else
            {
                Roomba1.Margin = new Thickness(435,49,0,0);
                Roomba1.Width = 82;
                roomba1_pressed = false;
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
                roomba2_pressed = false;
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
                roomba3_pressed = false;
            }
        }

        private void grid_11_click(object sender, MouseButtonEventArgs e)
        {
            if (roomba1_pressed==true)
            {
                Rec_111.Fill = new SolidColorBrush(Color.FromRgb(176, 5,5));
                Rec_112.Fill = new SolidColorBrush(Color.FromRgb(176, 5, 5));
                Rec_113.Fill = new SolidColorBrush(Color.FromRgb(176, 5, 5));
                arr.board[0, 0] = 1;
            }
            else if (roomba2_pressed == true)
            {
                Rec_111.Fill = new SolidColorBrush(Color.FromRgb(13, 41, 168));
                Rec_112.Fill = new SolidColorBrush(Color.FromRgb(13, 41, 168));
                Rec_113.Fill = new SolidColorBrush(Color.FromRgb(13, 41, 168));
                arr.board[0, 0] = 2;
            }
            else if (roomba3_pressed == true)
            {
                Rec_111.Fill = new SolidColorBrush(Color.FromRgb(7, 147, 0));
                Rec_112.Fill = new SolidColorBrush(Color.FromRgb(7, 147, 0));
                Rec_113.Fill = new SolidColorBrush(Color.FromRgb(7, 147, 0));
                arr.board[0, 0] = 3;
            }
        }

    }
}
