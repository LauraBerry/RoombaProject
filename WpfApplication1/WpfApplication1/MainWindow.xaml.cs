﻿/*Laura Berry(10111166)
 * Chris Miller(<ID NUMBER>)
 * 
 * citation:
 *  code taken in part from:
 *  -http://www.aforgenet.com/articles/step_to_stereo_vision/
 *  -http://www.aforgenet.com/
 *  -http://www.aforgenet.com/framework/docs/html/d7196dc6-8176-4344-a505-e7ade35c1741.htm
 *  -http://stackoverflow.com/questions/2006055/implementing-a-webcam-on-a-wpf-app-using-aforge-net
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using AForge.Controls;
using AForge.Imaging;
using AForge.Math;
using AForge.Video;

namespace WpfApplication1
{
    public partial class MainWindow : Window
    {
        bool roomba1_pressed,  roomba2_pressed, roomba3_pressed, mouseClicked;
        int currRed, currBlue,currGreen;
        arrayClass arr = new arrayClass();
        AForge.Video.DirectShow.FilterInfoCollection videoDevices;
        AForge.Video.DirectShow.VideoCaptureDevice vidsource1, videosource2;
        int[] coords, coords2;
        int firstOrSecond = 0;
        public MainWindow()
        {
            coords = new int[2];
            coords[0] = coords[1] = 0;
            coords2 = new int[2];
            coords2[0] = coords2[1] = 0;
            roomba1_pressed = roomba2_pressed = roomba3_pressed= mouseClicked = false;
            currBlue = currGreen = currRed = 1;
            arr.init();
            InitializeComponent();

            /*
             * start of video feed code
             */
            //find video device
            videoDevices= new AForge.Video.DirectShow.FilterInfoCollection(AForge.Video.DirectShow.FilterCategory.VideoInputDevice);
            // create video source
             vidsource1= new AForge.Video.DirectShow.VideoCaptureDevice(videoDevices[0].MonikerString);
             videosource2 = new AForge.Video.DirectShow.VideoCaptureDevice(videoDevices[0].MonikerString );
            // enumerate video devices
            AForge.Video.DirectShow.FilterInfoCollection videoDevices2= new AForge.Video.DirectShow.FilterInfoCollection(AForge.Video.DirectShow.FilterCategory.VideoInputDevice);
            // create video source
            AForge.Video.DirectShow.VideoCaptureDevice videoSource = new AForge.Video.DirectShow.VideoCaptureDevice(videoDevices2[0].MonikerString);
            // start the video source
            videoSource.Start();
            int counter=0;
            while (true)
            {
                // set NewFrame event handler
                videoSource.NewFrame += new AForge.Video.NewFrameEventHandler(video_NewFrame);
                // wait until we have two acquired images
                /*camera1Acquired.WaitOne();
                camera2Acquired.WaitOne();*/
                if(counter>=2)
                {
                    int[] vector;
                    vector = new int[2];
                    vector[0] = coords2[0] - coords[0];
                    vector[1] = coords2[1] - coords[1];
                    break;
                }
                counter++;
            }

            // signal to stop when you no longer need capturing
            //videoSource.SignalToStop( );
        }
        

        private void video_NewFrame( object sender, NewFrameEventArgs eventArgs )
        {
            try
            {
                System.Drawing.Image image = (Bitmap)eventArgs.Frame.Clone();                   //Width==640 , Height==480
                System.IO.MemoryStream mssg = new System.IO.MemoryStream();
                image.Save(mssg, System.Drawing.Imaging.ImageFormat.Bmp);
                mssg.Seek(0, System.IO.SeekOrigin.Begin);
                BitmapImage bitmap1 = new BitmapImage();
                bitmap1.BeginInit();
                bitmap1.StreamSource = mssg;
                bitmap1.EndInit();

                bitmap1.Freeze();
                Dispatcher.BeginInvoke(new System.Threading.ThreadStart(delegate{camera_input.Source = bitmap1;}));
            }
            catch (Exception ex)
            {
                Console.Write(ex);
            }
            System.Drawing.Bitmap bitmap = eventArgs.Frame;
            // create filter
            AForge.Imaging.Filters.ColorFiltering colorFilter = new AForge.Imaging.Filters.ColorFiltering();
            // configure the filter
            colorFilter.Red   = new AForge.IntRange( 0, 100 );
            colorFilter.Green = new AForge.IntRange( 0, 200 );
            colorFilter.Blue = new AForge.IntRange(150, 255);

            System.Drawing.Bitmap filteredImage = colorFilter.Apply(bitmap);

            /// create blob counter and configure it
            BlobCounter blobCounter = new BlobCounter();
            blobCounter.MinWidth = 5;                    // set minimum size of
            blobCounter.MinHeight = 5;                   // objects we look for
            blobCounter.FilterBlobs = true;               // filter blobs by size
            blobCounter.ObjectsOrder = ObjectsOrder.Size; // order found object by size
            // grayscaling
            AForge.Imaging.Filters.Grayscale grayFilter = new AForge.Imaging.Filters.Grayscale(0.2125, 0.7154, 0.0721); ;

            Bitmap grayImage = grayFilter.Apply(filteredImage);
            // locate blobs 
            blobCounter.ProcessImage(grayImage);
            System.Drawing.Rectangle[] rects = blobCounter.GetObjectsRectangles();
            // draw rectangle around the biggest blob
            if (rects.Length > 0)
            {
                System.Drawing.Rectangle objectRect = rects[0];
                Graphics g = Graphics.FromImage(bitmap);

                using (System.Drawing.Pen pen = new System.Drawing.Pen(System.Drawing.Color.FromArgb(160, 255, 160), 3))
                {
                    int[] location = new int[2];
                    g.DrawRectangle(pen, objectRect);
                    int x1 = (objectRect.Left + objectRect.Right) / 2;
                    int  y1 = (objectRect.Top + objectRect.Bottom) / 2;
                    clear_location_board();
                    if (firstOrSecond == 0)
                    {
                        coords[0] = x1;
                        coords[1] = y1;
                        location = actual_location(coords);
                        arr.location_on_board[location[0], location[1]]=1;
                        firstOrSecond = 1;
                    }
                    else
                    {
                        coords2[0] = x1;
                        coords2[1] = y1;
                        location = actual_location(coords2);
                        arr.location_on_board[location[0], location[1]] = 1;
                        firstOrSecond = 0;
                    }
                    Console.Write("Location: ");
                    Console.Write(location[0]);
                    Console.Write(", ");
                    Console.WriteLine(location[1]);
                }

                g.Dispose();
            }
        }

        public void clear_location_board()
        {
            for (int i=0; i<5;i++)
            {
                for (int j=0; j<7; j++)
                {
                    arr.location_on_board[i, j] = 0;
                }
            }
        }

        /*
         * caluclates the coordinates of the roomba on a 5X7 grid 
         * from the 640X480 grid on the image and returns it as a tuple.
         */
        public int[] actual_location(int[] a)
        {
            int[] location_coords = new int[2];
            if (a[0] <= 91)
            {
                location_coords[0] = 0;
            }
            else if (a[0] <= 182)
            {
                location_coords[0] = 1;
            }
            else if (a[0] <= 273)
            {
                location_coords[0] = 2;
            }
            else if (a[0] <= 364)
            {
                location_coords[0] = 3;
            }
            else if (a[0] <= 455)
            {
                location_coords[0] = 4;
            }
            else if (a[0] <= 546)
            {
                location_coords[0] = 5;
            }
            else if (a[0] <= 640)
            {
                location_coords[0] = 6;
            }
            if (a[1] <= 96)
            {
                location_coords[1] = 0;
            }
            else if (a[1] <= 192)
            {
                location_coords[1] = 1;
            }
            else if (a[1] <= 288)
            {
                location_coords[1] = 2;
            }
            else if (a[1] <= 385)
            {
                location_coords[1] = 3;
            }
            else if (a[1] <= 480)
            {
                location_coords[1] = 4;
            }
            if (location_coords[0]>6)
            {
                location_coords[0] = 6;
            }
            else if (location_coords[0] < 0)
            {
                location_coords[0] = 0;
            }
            if (location_coords[1] > 4)
            {
                location_coords[1] = 4;
            }
            else if (location_coords[1] < 0)
            {
                location_coords[1] = 0;
            }

            return location_coords;
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
         * listens for which roomba is selected, ensures that only one is selected at a time
         */
        private void Din_clicked(object sender, RoutedEventArgs e)
        {
            if (roomba1_pressed == false)
            {
                roomba1_pressed = true;
                Roomba1.Width = 106;
                Roomba1.Margin = new Thickness(401, 44, 0, 0);
                Roomba2.Margin = new Thickness(411, 78, 0, 0);
                roomba2_pressed = false;
                Roomba3.Margin = new Thickness(411, 112, 0, 0);
                roomba3_pressed = false;
                clear_button.Background = new SolidColorBrush(System.Windows.Media.Color.FromRgb(176, 5, 5));
            }
            else
            {

                Roomba1.Width = 96;
                Roomba1.Margin = new Thickness(411, 44, 0, 0);
                roomba1_pressed = false;
                roomba2_pressed = false;
                roomba3_pressed = false;
                clear_button.Background = new SolidColorBrush(System.Windows.Media.Color.FromRgb(221, 221, 221));
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
                clear_button.Background = new SolidColorBrush(System.Windows.Media.Color.FromRgb(13, 41, 168));
            }
            else
            {
                Roomba2.Width = 96;
                Roomba2.Margin = new Thickness(411, 78, 0, 0);
                roomba1_pressed = false;
                roomba2_pressed = false;
                roomba3_pressed = false;
                clear_button.Background = new SolidColorBrush(System.Windows.Media.Color.FromRgb(221, 221, 221));
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
                clear_button.Background = new SolidColorBrush(System.Windows.Media.Color.FromRgb(7, 147, 0));
            }
            else
            {
                Roomba3.Width = 96;
                Roomba3.Margin = new Thickness(411, 112, 0, 0);
                roomba1_pressed = false;
                roomba2_pressed = false;
                roomba3_pressed = false;
                clear_button.Background = new SolidColorBrush(System.Windows.Media.Color.FromRgb(221, 221, 221));
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
            if (mouseClicked == true)
            {
                change(Rec_12, 0, 1);
            }
        }
        private void selected_13(object sender, MouseEventArgs e)
        {
            if (mouseClicked == true)
            {
                change(Rec_13, 0, 2);
            }
        }
        private void selected_14(object sender, MouseEventArgs e)
        {
            if (mouseClicked == true)
            {
                change(Rec_14, 0, 3);
            }
        }
        private void selected_15(object sender, MouseEventArgs e)
        {
            if (mouseClicked == true)
            {
                change(Rec_15, 0, 4);
            }
        }
        private void selected_16(object sender, MouseEventArgs e)
        {
            if (mouseClicked == true)
            {
                change(Rec_16, 0, 5);
            }
        }
        private void selected_17(object sender, MouseEventArgs e)
        {
            if (mouseClicked == true)
            {
                change(Rec_17, 0, 6);
            }
        }
        private void selected_21(object sender, MouseEventArgs e)
        {
            if (mouseClicked == true)
            {
                change(Rec_21, 1, 0);
            }
        }
        private void selected_22(object sender, MouseEventArgs e)
        {
            if (mouseClicked == true)
            {
                change(Rec_22, 1, 1);
            }
        }
        private void selected_23(object sender, MouseEventArgs e)
        {
            if (mouseClicked == true)
            {
                change(Rec_23, 1, 2);
            }
        }
        private void selected_24(object sender, MouseEventArgs e)
        {
            if (mouseClicked == true)
            {
                change(Rec_24, 1, 3);
            }
        }
        private void selected_25(object sender, MouseEventArgs e)
        {
            if (mouseClicked == true)
            {
                change(Rec_25, 1, 4);
            }
        }
        private void selected_26(object sender, MouseEventArgs e)
        {
            if (mouseClicked == true)
            {
                change(Rec_26, 1, 5);
            }
        }
        private void selected_27(object sender, MouseEventArgs e)
        {
            if (mouseClicked == true)
            {
                change(Rec_27, 1, 6);
            }
        }
        private void selected_31(object sender, MouseEventArgs e)
        {
            if (mouseClicked == true)
            {
                change(Rec_31, 2, 0);
            }
        }
        private void selected_32(object sender, MouseEventArgs e)
        {
            if (mouseClicked == true)
            {
                change(Rec_32, 2, 1);
            }
        }
        private void selected_33(object sender, MouseEventArgs e)
        {
            if (mouseClicked == true)
            {
                change(Rec_33, 2, 2);
            }
        }
        private void selected_34(object sender, MouseEventArgs e)
        {
            if (mouseClicked == true)
            {
                change(Rec_34, 2, 3);
            }
        }
        private void selected_35(object sender, MouseEventArgs e)
        {
            if (mouseClicked == true)
            {
                change(Rec_35, 2, 4);
            }
        }
        private void selected_36(object sender, MouseEventArgs e)
        {
            if (mouseClicked == true)
            {
                change(Rec_36, 2, 5);
            }
        }
        private void selected_37(object sender, MouseEventArgs e)
        {
            if (mouseClicked == true)
            {
                change(Rec_37, 2, 6);
            }
        }
        private void selected_41(object sender, MouseEventArgs e)
        {
            if (mouseClicked == true)
            {
                change(Rec_41, 3, 0);
            }
        }
        private void selected_42(object sender, MouseEventArgs e)
        {
            if (mouseClicked == true)
            {
                change(Rec_42, 3, 1);
            }
        }
        private void selected_43(object sender, MouseEventArgs e)
        {
            if (mouseClicked == true)
            {
                change(Rec_43, 3, 2);
            }
        }
        private void selected_44(object sender, MouseEventArgs e)
        {
            if (mouseClicked == true)
            {
                change(Rec_44, 3, 3);
            }
        }
        private void selected_45(object sender, MouseEventArgs e)
        {
            if (mouseClicked == true)
            {
                change(Rec_45, 3, 4);
            }
        }
        private void selected_46(object sender, MouseEventArgs e)
        {
            if (mouseClicked == true)
            {
                change(Rec_46, 3, 5);
            }
        }
        private void selected_47(object sender, MouseEventArgs e)
        {
            if (mouseClicked == true)
            {
                change(Rec_47, 3, 6);
            }
        }
        private void selected_51(object sender, MouseEventArgs e)
        {
            if (mouseClicked == true)
            {
                change(Rec_51, 4, 0);
            }
        }
        private void selected_52(object sender, MouseEventArgs e)
        {
            if (mouseClicked == true)
            {
                change(Rec_52, 4, 1);
            }
        }
        private void selected_53(object sender, MouseEventArgs e)
        {
            if (mouseClicked == true)
            {
                change(Rec_53, 4, 2);
            }
        }
        private void selected_54(object sender, MouseEventArgs e)
        {
            if (mouseClicked == true)
            {
                change(Rec_54, 4, 3);
            }
        }
        private void selected_55(object sender, MouseEventArgs e)
        {
            if (mouseClicked == true)
            {
                change(Rec_55, 4, 4);
            }
        }
        private void selected_57(object sender, MouseEventArgs e)
        {
            if (mouseClicked == true)
            {
                change(Rec_57, 4, 6);
            }
        }
        private void selected_56(object sender, MouseEventArgs e)
        {
            if (mouseClicked == true)
            {
                change(Rec_56, 4, 5);
            }
        }

        /*
         * changes the color of the System.Windows.Shapes.Rectangle based on which roomba is selected
         */
        public void remove_red(int x, int y, System.Windows.Shapes.Rectangle Rec)
        {
            if (arr.board[x, y] == 1)
            {
                Rec.Fill = new SolidColorBrush(System.Windows.Media.Color.FromRgb(244, 244, 245));
                arr.board[x, y] = 0;
                currRed = remove_path(arr.red_path, x, y, currRed);
            }
            else if (arr.board[x, y] == 7)
            {
                Rec.Fill = new SolidColorBrush(System.Windows.Media.Color.FromRgb(11, 162, 94));
                arr.board[x, y] = 6;
                currRed = remove_path(arr.red_path, x, y, currRed);
            }
            else if (arr.board[x, y] == 4)
            {
                Rec.Fill = new SolidColorBrush(System.Windows.Media.Color.FromRgb(13, 41, 168));
                arr.board[x, y] = 2;
                currRed = remove_path(arr.red_path, x, y, currRed);
            }
            else if (arr.board[x, y] == 5)
            {
                Rec.Fill = new SolidColorBrush(System.Windows.Media.Color.FromRgb(7, 147, 0));
                arr.board[x, y] = 3;
                currRed = remove_path(arr.red_path, x, y, currRed);
            }
        }

        public void remove_blue(int x, int y, System.Windows.Shapes.Rectangle Rec)
        {
            if (arr.board[x, y] == 2)
            {
                Rec.Fill = new SolidColorBrush(System.Windows.Media.Color.FromRgb(244, 244, 245));
                arr.board[x, y] = 0;
                currBlue = remove_path(arr.blue_path, x, y, currBlue);
            }
            else if (arr.board[x, y] == 7)
            {
                Rec.Fill = new SolidColorBrush(System.Windows.Media.Color.FromRgb(188, 226, 14));
                arr.board[x, y] = 5;
                currBlue = remove_path(arr.blue_path, x, y, currBlue);
            }
            else if (arr.board[x, y] == 4)
            {
                Rec.Fill = new SolidColorBrush(System.Windows.Media.Color.FromRgb(176, 5, 5));
                arr.board[x, y] = 1;
                currBlue = remove_path(arr.blue_path, x, y, currBlue);
            }
            else if (arr.board[x, y] == 6)
            {
                Rec.Fill = new SolidColorBrush(System.Windows.Media.Color.FromRgb(7, 147, 0));
                arr.board[x, y] = 3;
                currBlue = remove_path(arr.blue_path, x, y, currBlue);
            }
        }
        public void remove_green(int x, int y, System.Windows.Shapes.Rectangle Rec)
        {
            if (arr.board[x, y] == 3)
            {
                Rec.Fill = new SolidColorBrush(System.Windows.Media.Color.FromRgb(244, 244, 245));
                arr.board[x, y] = 0;
                currGreen = remove_path(arr.green_path, x, y, currGreen);
            }
            else if (arr.board[x, y] == 7)
            {
                Rec.Fill = new SolidColorBrush(System.Windows.Media.Color.FromRgb(124, 21, 226));
                arr.board[x, y] = 4;
                currGreen = remove_path(arr.green_path, x, y, currGreen);
            }
            else if (arr.board[x, y] == 5)
            {
                Rec.Fill = new SolidColorBrush(System.Windows.Media.Color.FromRgb(176, 5, 5));
                arr.board[x, y] = 1;
                currGreen = remove_path(arr.green_path, x, y, currGreen);
            }
            else if (arr.board[x, y] == 6)
            {
                Rec.Fill = new SolidColorBrush(System.Windows.Media.Color.FromRgb(13, 41, 168));
                arr.board[x, y] = 2;
                currGreen = remove_path(arr.green_path, x, y, currGreen);
            }
        }

        public void change(System.Windows.Shapes.Rectangle Rec, int x, int y)
        {
            if (roomba1_pressed == true)
            {
                if (arr.board[x, y] == 2)
                {
                    Rec.Fill = new SolidColorBrush(System.Windows.Media.Color.FromRgb(124, 21, 226));
                    arr.board[x, y] = 4;
                    currRed = add_path(arr.red_path, x, y, currRed);
                }
                else if (arr.board[x, y] == 3)
                {
                    Rec.Fill = new SolidColorBrush(System.Windows.Media.Color.FromRgb(188, 226, 14));
                    arr.board[x, y] = 5;
                    currRed = add_path(arr.red_path, x, y, currRed);
                }
                else if (arr.board[x, y] == 6)
                {
                    Rec.Fill = new SolidColorBrush(System.Windows.Media.Color.FromRgb(58, 58, 58));
                    arr.board[x, y] = 7;
                    currRed = add_path(arr.red_path, x, y, currRed);
                }
                else if (arr.board[x, y] == 0)
                {
                    Rec.Fill = new SolidColorBrush(System.Windows.Media.Color.FromRgb(176, 5, 5));
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
                    Rec.Fill = new SolidColorBrush(System.Windows.Media.Color.FromRgb(124, 21, 226));
                    arr.board[x, y] = 4;
                    currBlue = add_path(arr.blue_path, x, y, currBlue);
                }
                else if (arr.board[x, y] == 3)
                {
                    Rec.Fill = new SolidColorBrush(System.Windows.Media.Color.FromRgb(11, 162, 94));
                    arr.board[x, y] = 6;
                    currBlue = add_path(arr.blue_path, x, y, currBlue);
                }
                else if (arr.board[x, y] == 5)
                {
                    Rec.Fill = new SolidColorBrush(System.Windows.Media.Color.FromRgb(58, 58, 58));
                    arr.board[x, y] = 7;
                    currBlue = add_path(arr.blue_path, x, y, currBlue);
                }
                else if (arr.board[x, y] == 0)
                {
                    Rec.Fill = new SolidColorBrush(System.Windows.Media.Color.FromRgb(13, 41, 168));
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
                    Rec.Fill = new SolidColorBrush(System.Windows.Media.Color.FromRgb(188, 226, 14));
                    arr.board[x, y] = 5;
                    currGreen = add_path(arr.green_path, x, y, currGreen);
                }
                else if (arr.board[x, y] == 2)
                {
                    Rec.Fill = new SolidColorBrush(System.Windows.Media.Color.FromRgb(11, 162, 94));
                    arr.board[x, y] = 6;
                    currGreen = add_path(arr.green_path, x, y, currGreen);
                }
                else if (arr.board[x, y] == 4)
                {
                    Rec.Fill = new SolidColorBrush(System.Windows.Media.Color.FromRgb(58, 58, 58));
                    arr.board[x, y] = 7;
                    currGreen = add_path(arr.green_path, x, y, currGreen);
                }
                else if (arr.board[x, y] == 0)
                {
                    Rec.Fill = new SolidColorBrush(System.Windows.Media.Color.FromRgb(7, 147, 0));
                    arr.board[x, y] = 3;
                    currGreen = add_path(arr.green_path, x, y, currGreen);
                }
                else
                {
                    remove_green(x, y, Rec);
                }
            }
        }
        public int remove_path(int[,] a, int x, int y, int val)
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
        public void clear_block(System.Windows.Shapes.Rectangle rec, int x, int y)
        {
            rec.Fill = new SolidColorBrush(System.Windows.Media.Color.FromRgb(244, 244, 244));
            arr.board[x, y] = 0;
        }

        /*
         * clears entier grid
         */
        private void clear_board(object sender, RoutedEventArgs e)
        {
            if (roomba1_pressed == true)
            {
                remove_red(0, 0, Rec_11);
                remove_red(0, 1, Rec_12);
                remove_red(0, 2, Rec_13);
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
                remove_red(2, 5, Rec_36);
                remove_red(2, 6, Rec_37);
                remove_red(3, 0, Rec_41);
                remove_red(3, 1, Rec_42);
                remove_red(3, 2, Rec_43);
                remove_red(3, 3, Rec_44);
                remove_red(3, 4, Rec_45);
                remove_red(3, 5, Rec_46);
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
            bool printboards = false;
            roomba1_pressed = false;
            roomba2_pressed = false;
            roomba3_pressed = false;
            Roomba1.Margin = new Thickness(411, 44, 0, 0);
            Roomba2.Margin = new Thickness(411, 78, 0, 0);
            Roomba3.Margin = new Thickness(411, 112, 0, 0);
            clear_button.Background = new SolidColorBrush(System.Windows.Media.Color.FromRgb(221, 221, 221));
            Console.Write("first coordinates: ");
            Console.Write(coords[0]);
            Console.Write(", ");
            Console.WriteLine(coords[1]);
            Console.Write("second coordinates: ");
            Console.Write(coords2[0]);
            Console.Write(", ");
            Console.WriteLine(coords2[1]);
            int[] vector;
            vector = new int[2];
            vector[0] = coords2[0] - coords[0];
            vector[1] = coords2[1] - coords[1];
            Console.Write("vector: ");
            Console.Write(vector[0]);
            Console.Write(", ");
            Console.WriteLine(vector[0]);
            if(printboards==true)
            {
                for (int i = 0; i < 3; i++)
                {
                    if (i == 0)
                    {
                        Console.Write("currRed: ");
                        Console.WriteLine(currRed);
                        Console.WriteLine("Red Roomba: ");
                        for (int j = 0; j < 5; j++)
                        {
                            for (int k = 0; k < 7; k++)
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
}