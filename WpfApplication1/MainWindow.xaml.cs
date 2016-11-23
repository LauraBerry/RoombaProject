/*Laura Berry(10111166)
 * Chris Miller(<ID NUMBER>)
 * 
 * citation:
 *  code taken in part from:
 *  -http://www.aforgenet.com/articles/step_to_stereo_vision/
 *  -http://www.aforgenet.com/
 *  -http://www.aforgenet.com/framework/docs/html/d7196dc6-8176-4344-a505-e7ade35c1741.htm
 *  -http://stackoverflow.com/questions/2006055/implementing-a-webcam-on-a-wpf-app-using-aforge-net
 *  change interface so that it only highlights 1 square as it is selected. make it so that this stores the center point of the grid bock 
 *  as a 64/480 coordinate
 *  impliment color tracking colors: green (A=255, R=100, G=155, B=15), Blue (A=255, R=76, G=143, B=204)  and Red (A=255, R=121, G=177, B=255)
 *  start linklist implimentation
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
    public partial class MainWindow
    {
        bool roomba1_pressed, roomba2_pressed, roomba3_pressed;
        int currRed, currBlue, currGreen;
        arrayClass arr = new arrayClass();
        AForge.Video.DirectShow.FilterInfoCollection videoDevices;
        AForge.Video.DirectShow.VideoCaptureDevice vidsource1, videosource2;
        int[] coords, coords2;
        int firstOrSecond = 0;
        bool red_list_started, blue_list_started, green_list_started;
        pathNode redHead, blueHead, greenHead;

        public MainWindow()
        {
            red_list_started = blue_list_started = green_list_started = false;
            coords = new int[2];
            coords[0] = coords[1] = 0;
            coords2 = new int[2];
            coords2[0] = coords2[1] = 0;
            roomba1_pressed = roomba2_pressed = roomba3_pressed = false;
            currBlue = currGreen = currRed = 1;
            arr.init();
            InitializeComponent();

            /*
             * start of video feed code
             */
            //find video device
            videoDevices = new AForge.Video.DirectShow.FilterInfoCollection(AForge.Video.DirectShow.FilterCategory.VideoInputDevice);
            // create video source
            vidsource1 = new AForge.Video.DirectShow.VideoCaptureDevice(videoDevices[0].MonikerString);
            videosource2 = new AForge.Video.DirectShow.VideoCaptureDevice(videoDevices[0].MonikerString);
            // enumerate video devices
            AForge.Video.DirectShow.FilterInfoCollection videoDevices2 = new AForge.Video.DirectShow.FilterInfoCollection(AForge.Video.DirectShow.FilterCategory.VideoInputDevice);
            // create video source
            AForge.Video.DirectShow.VideoCaptureDevice videoSource = new AForge.Video.DirectShow.VideoCaptureDevice(videoDevices2[0].MonikerString);
            // start the video source
            videoSource.Start();
            int counter = 0;
            while (true)
            {
                // set NewFrame event handler
                videoSource.NewFrame += new AForge.Video.NewFrameEventHandler(video_NewFrame);
                if (firstOrSecond == 0)
                {
                    firstOrSecond = 1;
                }
                else
                {
                    firstOrSecond = 0;
                }
                // wait until we have two acquired images
                /*camera1Acquired.WaitOne();
                camera2Acquired.WaitOne();*/
                if (counter >= 2)
                {
                    int[] vector;
                    vector = new int[2];
                    vector[0] = coords2[0] - coords[0];
                    vector[1] = coords2[1] - coords[1];         //Nico: for some reason if this result is negative it shows up in vector as 0
                    break;
                }
                counter++;
            }

            // signal to stop when you no longer need capturing
            //videoSource.SignalToStop( );
        }


        private void video_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            try
            {
                System.Drawing.Image image = (Bitmap)eventArgs.Frame.Clone();                   //Width==640 , Height==480
                System.IO.MemoryStream mssg = new System.IO.MemoryStream();
                image.Save(mssg, System.Drawing.Imaging.ImageFormat.Bmp);
                mssg.Seek(0, System.IO.SeekOrigin.Begin);
                BitmapImage bitmap1 = new BitmapImage();
                bitmap1.BeginInit();
                bitmap1.StreamSource = mssg;                                                    //creates a bitmap of the frame from the camera
                bitmap1.EndInit();

                bitmap1.Freeze();
                Dispatcher.BeginInvoke(new System.Threading.ThreadStart(delegate { camera_input.Source = bitmap1; })); //sets image source to be the bitmap image
            }
            catch (Exception ex)
            {
                Console.Write(ex);
            }
            System.Drawing.Bitmap bitmap = eventArgs.Frame;
            // create filter
            AForge.Imaging.Filters.ColorFiltering colorFilter = new AForge.Imaging.Filters.ColorFiltering();
            // configure the filter
            colorFilter.Red = new AForge.IntRange(0, 100);
            colorFilter.Green = new AForge.IntRange(0, 200);
            colorFilter.Blue = new AForge.IntRange(150, 255);

            System.Drawing.Bitmap filteredImage = colorFilter.Apply(bitmap);

            /// create blob counter and configure it
            BlobCounter blobCounter = new BlobCounter();
            blobCounter.MinHeight = 5;                                              //nico it will see my phone but not the roomba? what is the unit here?
            blobCounter.MinWidth = 5;
            blobCounter.FilterBlobs = true;                                         // filter blobs by size
            blobCounter.ObjectsOrder = ObjectsOrder.Size;                           // order found object by size
            // grayscaling
            AForge.Imaging.Filters.Grayscale grayFilter = new AForge.Imaging.Filters.Grayscale(0.2125, 0.7154, 0.0721); ;

            Bitmap grayImage = grayFilter.Apply(filteredImage);
            // locate blobs 
            blobCounter.ProcessImage(grayImage);
            System.Drawing.Rectangle[] rects = blobCounter.GetObjectsRectangles();
            // draw rectangle around the biggest blob           //todo maybe alter this if it is not seeing the roomba properly

            //Console.WriteLine(rects[0].Size.Height);
            //Console.WriteLine(rects[0].Size.Width);
            if (rects.Length > 0)
            {
                for (int i = 0; i < rects.Length; i++)
                {
                    System.Drawing.Rectangle objectRect = rects[i];
                    Graphics g = Graphics.FromImage(bitmap);

                    using (System.Drawing.Pen pen = new System.Drawing.Pen(System.Drawing.Color.FromArgb(160, 255, 160), 3))
                    {
                        int[] location = new int[2];
                        g.DrawRectangle(pen, objectRect);
                        int x1 = (objectRect.Left + objectRect.Right) / 2;                          //finds the x coordinate of the middle of the rectangle
                        int y1 = (objectRect.Top + objectRect.Bottom) / 2;                          //finds the y coordinate of the middle of the rectangle
                        System.Drawing.Color a = bitmap.GetPixel(x1, y1);
                        System.Drawing.Color green_color = System.Drawing.Color.FromArgb(255, 100, 155, 15);
                        System.Drawing.Color blue_color = System.Drawing.Color.FromArgb(255, 76, 143, 204);
                        System.Drawing.Color red_color = System.Drawing.Color.FromArgb(255, 121, 177, 255);
                        /*
                         * alternates which coordinates the x and y will be stored in to allow for vector calculations
                         */
                        if (firstOrSecond == 0)
                        {
                            coords[0] = x1;
                            coords[1] = y1;
                            location = actual_location(coords);
                        }
                        else
                        {
                            coords2[0] = x1;
                            coords2[1] = y1;
                            location = actual_location(coords2);
                        }
                    }
                    g.Dispose();
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
            if (location_coords[0] > 4)
            {
                location_coords[0] = 4;
            }
            else if (location_coords[0] < 0)
            {
                location_coords[0] = 0;
            }
            if (location_coords[1] > 6)
            {
                location_coords[1] = 6;
            }
            else if (location_coords[1] < 0)
            {
                location_coords[1] = 0;
            }

            return location_coords;
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
        private void selected_11(object sender, MouseButtonEventArgs e)
        {
            change(Rec_11, 0, 0, 48, 45);
        }
        private void selected_12(object sender, MouseButtonEventArgs e)
        {
            change(Rec_12, 0, 1, 48, 137);
        }
        private void selected_13(object sender, MouseButtonEventArgs e)
        {
            change(Rec_13, 0, 2, 48, 228);
        }
        private void selected_14(object sender, MouseButtonEventArgs e)
        {
            change(Rec_14, 0, 3, 48, 320);
        }
        private void selected_15(object sender, MouseButtonEventArgs e)
        {
            change(Rec_15, 0, 4, 48, 411);
        }
        private void selected_16(object sender, MouseButtonEventArgs e)
        {
            change(Rec_16, 0, 5, 48, 502);
        }
        private void selected_17(object sender, MouseButtonEventArgs e)
        {
            change(Rec_17, 0, 6, 48, 594);
        }
        private void selected_21(object sender, MouseButtonEventArgs e)
        {
            change(Rec_21, 1, 0, 144, 45);
        }
        private void selected_22(object sender, MouseButtonEventArgs e)
        {
            change(Rec_22, 1, 1, 144, 137);
        }
        private void selected_23(object sender, MouseButtonEventArgs e)
        {
            change(Rec_23, 1, 2, 144, 228);
        }
        private void selected_24(object sender, MouseButtonEventArgs e)
        {
            change(Rec_24, 1, 3, 144, 320);
        }
        private void selected_25(object sender, MouseButtonEventArgs e)
        {
            change(Rec_25, 1, 4, 144, 411);
        }
        private void selected_26(object sender, MouseButtonEventArgs e)
        {
            change(Rec_26, 1, 5, 144, 502);
        }
        private void selected_27(object sender, MouseButtonEventArgs e)
        {
            change(Rec_27, 1, 6, 144, 594);
        }
        private void selected_31(object sender, MouseButtonEventArgs e)
        {
            change(Rec_31, 2, 0, 240, 45);
        }
        private void selected_32(object sender, MouseButtonEventArgs e)
        {
            change(Rec_32, 2, 1, 240, 144);
        }
        private void selected_33(object sender, MouseButtonEventArgs e)
        {
            change(Rec_33, 2, 2, 240, 228);
        }
        private void selected_34(object sender, MouseButtonEventArgs e)
        {
            change(Rec_34, 2, 3, 240, 320);
        }
        private void selected_35(object sender, MouseButtonEventArgs e)
        {
            change(Rec_35, 2, 4, 240, 411);
        }
        private void selected_36(object sender, MouseButtonEventArgs e)
        {
            change(Rec_36, 2, 5, 240, 502);
        }
        private void selected_37(object sender, MouseButtonEventArgs e)
        {
            change(Rec_37, 2, 6, 240, 594);
        }
        private void selected_41(object sender, MouseButtonEventArgs e)
        {
            change(Rec_41, 3, 0, 336, 45);
        }
        private void selected_42(object sender, MouseButtonEventArgs e)
        {
            change(Rec_42, 3, 1, 336, 137);
        }
        private void selected_43(object sender, MouseButtonEventArgs e)
        {
            change(Rec_43, 3, 2, 336, 228);
        }
        private void selected_44(object sender, MouseButtonEventArgs e)
        {
            change(Rec_44, 3, 3, 336, 320);
        }
        private void selected_45(object sender, MouseButtonEventArgs e)
        {
            change(Rec_45, 3, 4, 336, 411);
        }
        private void selected_46(object sender, MouseButtonEventArgs e)
        {
            change(Rec_46, 3, 5, 336, 502);
        }
        private void selected_47(object sender, MouseButtonEventArgs e)
        {
            change(Rec_47, 3, 6, 336, 594);
        }
        private void selected_51(object sender, MouseButtonEventArgs e)
        {
            change(Rec_51, 4, 0, 432, 45);
        }
        private void selected_52(object sender, MouseButtonEventArgs e)
        {
            change(Rec_52, 4, 1, 432, 137);
        }
        private void selected_53(object sender, MouseButtonEventArgs e)
        {
            change(Rec_53, 4, 2, 432, 228);
        }
        private void selected_54(object sender, MouseButtonEventArgs e)
        {
            change(Rec_54, 4, 3, 432, 320);
        }
        private void selected_55(object sender, MouseButtonEventArgs e)
        {
            change(Rec_55, 4, 4, 432, 411);
        }
        private void selected_57(object sender, MouseButtonEventArgs e)
        {
            change(Rec_57, 4, 6, 432, 502);
        }
        private void selected_56(object sender, MouseButtonEventArgs e)
        {
            change(Rec_56, 4, 5, 432, 137);
        }

        public void add_to_linkedList(int x, int y)
        {

            if (roomba1_pressed == true)
            {
                if (red_list_started == false)
                {
                    redHead = new pathNode();
                    redHead.x_coord=x;
                    redHead.y_coord=y;
                    redHead.sequence=currRed;
                    currRed++;
                    red_list_started = true;
                }
                else
                {
                    pathNode newNode = new pathNode();
                    newNode.x_coord=x; 
                    newNode.y_coord=y;
                    newNode.sequence=currRed;
                    currRed++;
                    pathNode finder = redHead;
                    while(finder.Next!=null)
                    {
                        finder=finder.Next;
                    }
                    finder.Next=newNode;
                }
                currRed++;
                
            }
            else if (roomba2_pressed == true)
            {
                if (blue_list_started == false)
                {
                    blueHead = new pathNode();
                    blueHead.x_coord=x;
                    blueHead.y_coord=y;
                    blueHead.sequence=currBlue;
                    currBlue++;
                    blue_list_started = true;
                }
                else
                {
                    pathNode newNode = new pathNode();
                    newNode.x_coord=x; 
                    newNode.y_coord=y;
                    newNode.sequence=currBlue;
                    currBlue++;
                    pathNode finder = blueHead;
                    while(finder.Next!=null)
                    {
                        finder=finder.Next;
                    }
                    finder.Next=newNode;
                }
                currBlue++;
            }
            else if (roomba3_pressed == true)
            {
                if (green_list_started == false)
                {
                    greenHead = new pathNode();
                    greenHead.x_coord=x;
                    greenHead.y_coord=y;
                    greenHead.sequence=currGreen;
                    currGreen++;
                    green_list_started = true;
                }
                else
                {
                    pathNode newNode = new pathNode();
                    newNode.x_coord=x; 
                    newNode.y_coord=y;
                    newNode.sequence=currGreen;
                    currGreen++;
                    pathNode finder = greenHead;
                    while(finder.Next!=null)
                    {
                        finder=finder.Next;
                    }
                    finder.Next=newNode;
                }
                currGreen++;
            }
        }

        public void remove_from_linkedList(int x, int y)
        {
            pathNode findnode;
            if (roomba1_pressed == true)
            {
                currRed--;
                findnode = redHead;
                if (findnode.x_coord==x&&findnode.y_coord==y)
                {
                    redHead = findnode.Next;
                    findnode.Next = null;
                    return;
                }
            }
            else if (roomba2_pressed == true)
            {
                currBlue--;
                findnode = blueHead;
                if (findnode.x_coord == x && findnode.y_coord == y)
                {
                    blueHead = findnode.Next;
                    findnode.Next = null;
                    return;
                }
            }
            else if (roomba3_pressed==true)
            {
                currGreen--;
                findnode = greenHead;
                if (findnode.x_coord == x && findnode.y_coord == y)
                {
                    greenHead = findnode.Next;
                    findnode.Next = null;
                    return;
                }
            }
            else
            {
                redHead = null;
                red_list_started = false;
                greenHead = null;
                green_list_started = false;
                blueHead = null;
                blue_list_started = false;
                return;
            }
            do
            {
                if (findnode.Next==null)
                {
                    break;
                }
                else if(findnode.Next.x_coord==x && findnode.Next.y_coord==y)
                {
                    findnode.Next = findnode.Next.Next;
                    findnode = findnode.Next;
                    break;
                }
                else
                {
                    findnode = findnode.Next;
                }

            } while (findnode.Next != null);

            do
            {
                findnode.sequence--;
            } while (findnode.Next != null);
        }

        /*
         * changes the color of the System.Windows.Shapes.Rectangle based on which roomba is selected
         */
        public void remove_red(int x, int y, int midX,int midY,System.Windows.Shapes.Rectangle Rec, bool b)
        {
            if (b == true)
            {
                remove_from_linkedList(midX, midY);
            }
            if (arr.board[x, y] == 1)
            {
                Rec.Fill = new SolidColorBrush(System.Windows.Media.Color.FromRgb(244, 244, 245));
                arr.board[x, y] = 0;
                remove_path(arr.red_path, x, y, currRed);  
                
            }
            else if (arr.board[x, y] == 7)
            {
                Rec.Fill = new SolidColorBrush(System.Windows.Media.Color.FromRgb(11, 162, 94));
                arr.board[x, y] = 6;
                remove_path(arr.red_path, x, y, currRed);
            }
            else if (arr.board[x, y] == 4)
            {
                Rec.Fill = new SolidColorBrush(System.Windows.Media.Color.FromRgb(13, 41, 168));
                arr.board[x, y] = 2;
                remove_path(arr.red_path, x, y, currRed);
            }
            else if (arr.board[x, y] == 5)
            {
                Rec.Fill = new SolidColorBrush(System.Windows.Media.Color.FromRgb(7, 147, 0));
                arr.board[x, y] = 3;
                remove_path(arr.red_path, x, y, currRed);
            }
        }

        public void remove_blue(int x, int y, int midX, int midY, System.Windows.Shapes.Rectangle Rec, bool b)
        {
            if (b == true)
            {
                remove_from_linkedList(midX, midY);
            }
            if (arr.board[x, y] == 2)
            {
                Rec.Fill = new SolidColorBrush(System.Windows.Media.Color.FromRgb(244, 244, 245));
                arr.board[x, y] = 0;
                remove_path(arr.blue_path, x, y, currBlue);
            }
            else if (arr.board[x, y] == 7)
            {
                Rec.Fill = new SolidColorBrush(System.Windows.Media.Color.FromRgb(188, 226, 14));
                arr.board[x, y] = 5;
                remove_path(arr.blue_path, x, y, currBlue);
            }
            else if (arr.board[x, y] == 4)
            {
                Rec.Fill = new SolidColorBrush(System.Windows.Media.Color.FromRgb(176, 5, 5));
                arr.board[x, y] = 1;
                remove_path(arr.blue_path, x, y, currBlue);
            }
            else if (arr.board[x, y] == 6)
            {
                Rec.Fill = new SolidColorBrush(System.Windows.Media.Color.FromRgb(7, 147, 0));
                arr.board[x, y] = 3;
                remove_path(arr.blue_path, x, y, currBlue);
            }
        }
        public void remove_green(int x, int y,int midX, int midY, System.Windows.Shapes.Rectangle Rec, Boolean b)
        {
            if (b == true)
            {
                remove_from_linkedList(midX, midY);
            }
            if (arr.board[x, y] == 3)
            {
                Rec.Fill = new SolidColorBrush(System.Windows.Media.Color.FromRgb(244, 244, 245));
                arr.board[x, y] = 0;
                remove_path(arr.green_path, x, y, currGreen);
            }
            else if (arr.board[x, y] == 7)
            {
                Rec.Fill = new SolidColorBrush(System.Windows.Media.Color.FromRgb(124, 21, 226));
                arr.board[x, y] = 4;
                remove_path(arr.green_path, x, y, currGreen);
            }
            else if (arr.board[x, y] == 5)
            {
                Rec.Fill = new SolidColorBrush(System.Windows.Media.Color.FromRgb(176, 5, 5));
                arr.board[x, y] = 1;
                remove_path(arr.green_path, x, y, currGreen);
            }
            else if (arr.board[x, y] == 6)
            {
                Rec.Fill = new SolidColorBrush(System.Windows.Media.Color.FromRgb(13, 41, 168));
                arr.board[x, y] = 2;
                remove_path(arr.green_path, x, y, currGreen);
            }
        }

        public void change(System.Windows.Shapes.Rectangle Rec, int x, int y, int midX, int midY)
        {          
            if (roomba1_pressed == true)
            {
                if (arr.board[x, y] == 2)
                {
                    Rec.Fill = new SolidColorBrush(System.Windows.Media.Color.FromRgb(124, 21, 226));
                    arr.board[x, y] = 4;
                    add_to_linkedList(midX, midY);
                }
                else if (arr.board[x, y] == 3)
                {
                    Rec.Fill = new SolidColorBrush(System.Windows.Media.Color.FromRgb(188, 226, 14));
                    arr.board[x, y] = 5;
                    add_to_linkedList(midX, midY);
                }
                else if (arr.board[x, y] == 6)
                {
                    Rec.Fill = new SolidColorBrush(System.Windows.Media.Color.FromRgb(58, 58, 58));
                    arr.board[x, y] = 7;
                    add_to_linkedList(midX, midY);
                }
                else if (arr.board[x, y] == 0)
                {
                    Rec.Fill = new SolidColorBrush(System.Windows.Media.Color.FromRgb(176, 5, 5));
                    arr.board[x, y] = 1;
                    add_to_linkedList(midX, midY);
                }
                else
                {
                    remove_red(x, y, midX, midY, Rec, true);
                }
            }
            else if (roomba2_pressed == true)
            {
                if (arr.board[x, y] == 1)
                {
                    Rec.Fill = new SolidColorBrush(System.Windows.Media.Color.FromRgb(124, 21, 226));
                    arr.board[x, y] = 4;
                    add_to_linkedList(midX, midY);
                }
                else if (arr.board[x, y] == 3)
                {
                    Rec.Fill = new SolidColorBrush(System.Windows.Media.Color.FromRgb(11, 162, 94));
                    arr.board[x, y] = 6;
                    add_to_linkedList(midX, midY);
                }
                else if (arr.board[x, y] == 5)
                {
                    Rec.Fill = new SolidColorBrush(System.Windows.Media.Color.FromRgb(58, 58, 58));
                    arr.board[x, y] = 7;
                    add_to_linkedList(midX, midY);
                }
                else if (arr.board[x, y] == 0)
                {
                    Rec.Fill = new SolidColorBrush(System.Windows.Media.Color.FromRgb(13, 41, 168));
                    arr.board[x, y] = 2;
                    add_to_linkedList(midX, midY);
                }
                else
                {
                    remove_blue(x, y,midX, midY, Rec, true);
                }
            }
            else if (roomba3_pressed == true)
            {
                if (arr.board[x, y] == 1)
                {
                    Rec.Fill = new SolidColorBrush(System.Windows.Media.Color.FromRgb(188, 226, 14));
                    arr.board[x, y] = 5;
                    add_to_linkedList(midX, midY);
                }
                else if (arr.board[x, y] == 2)
                {
                    Rec.Fill = new SolidColorBrush(System.Windows.Media.Color.FromRgb(11, 162, 94));
                    arr.board[x, y] = 6;
                    add_to_linkedList(midX, midY);
                }
                else if (arr.board[x, y] == 4)
                {
                    Rec.Fill = new SolidColorBrush(System.Windows.Media.Color.FromRgb(58, 58, 58));
                    arr.board[x, y] = 7;
                    add_to_linkedList(midX, midY);
                }
                else if (arr.board[x, y] == 0)
                {
                    Rec.Fill = new SolidColorBrush(System.Windows.Media.Color.FromRgb(7, 147, 0));
                    arr.board[x, y] = 3;
                    add_to_linkedList(midX, midY);
                }
                else
                {
                    remove_green(x, y, midX, midY, Rec, true);
                }
            }
        }
        public void remove_path(int[,] a, int x, int y, int val)
        {
            a[x, y] = 0;
        }


        /*
         * sets a block back to a blank state
         */
        public void clear_block(System.Windows.Shapes.Rectangle rec, int x, int y, int midX, int midY)
        {
            rec.Fill = new SolidColorBrush(System.Windows.Media.Color.FromRgb(244, 244, 244));
            arr.board[x, y] = 0;
            remove_from_linkedList(midX, midY);
        }

        /*
         * clears entier grid
         */
        private void clear_board(object sender, RoutedEventArgs e)
        {
            if (roomba1_pressed == true)
            {
                redHead = null;
                red_list_started = false;
                remove_red( 0, 0, 48, 45,Rec_11, false);     
                remove_red(0, 1, 48, 137,Rec_12, false);
                remove_red(0, 2, 48, 228,Rec_13, false);
                remove_red( 0, 3, 48, 320,Rec_14, false);
                remove_red( 0, 4, 48, 411,Rec_15, false);
                remove_red( 0, 5, 48, 502,Rec_16, false);
                remove_red(0, 6, 48, 594,Rec_17, false);
                remove_red( 1, 0, 144, 45,Rec_21, false);
                remove_red( 1, 1, 144, 137,Rec_22, false);
                remove_red( 1, 2, 144, 228,Rec_23, false);
                remove_red( 1, 3, 144, 320,Rec_24, false);
                remove_red( 1, 4, 144, 411,Rec_25, false);
                remove_red( 1, 5, 144, 502,Rec_26, false);
                remove_red( 1, 6, 144, 594,Rec_27, false);
                remove_red( 2, 0, 240, 45,Rec_31, false);
                remove_red( 2, 1, 240, 144,Rec_32, false);
                remove_red( 2, 2, 240, 228,Rec_33, false);
                remove_red( 2, 3, 240, 320,Rec_34, false);
                remove_red( 2, 4, 240, 411,Rec_35, false);
                remove_red( 2, 5, 240, 502,Rec_36, false);
                remove_red( 2, 6, 240, 594,Rec_37, false);
                remove_red( 3, 0, 336, 45,Rec_41, false);
                remove_red( 3, 1, 336, 137,Rec_42, false);
                remove_red( 3, 2, 336, 228,Rec_43, false);
                remove_red( 3, 3, 336, 320,Rec_44, false);
                remove_red( 3, 4, 336, 411,Rec_45, false);
                remove_red( 3, 5, 336, 502,Rec_46, false);
                remove_red( 3, 6, 336, 594,Rec_47, false);
                remove_red( 4, 0, 432, 45,Rec_51, false);
                remove_red( 4, 1, 432, 137,Rec_52, false);
                remove_red( 4, 2, 432, 228,Rec_53, false);
                remove_red( 4, 3, 432, 320,Rec_54, false);
                remove_red( 4, 4, 432, 411,Rec_55, false);
                remove_red( 4, 6, 432, 502,Rec_57, false);
                remove_red( 4, 5, 432, 137,Rec_56, false);
        }
            else if (roomba2_pressed == true)
            {
                blueHead = null;
                blue_list_started = false;
                remove_blue( 0, 0, 48, 45,Rec_11, false);     
                remove_blue(0, 1, 48, 137,Rec_12, false);
                remove_blue(0, 2, 48, 228,Rec_13, false);
                remove_blue( 0, 3, 48, 320,Rec_14, false);
                remove_blue( 0, 4, 48, 411,Rec_15, false);
                remove_blue( 0, 5, 48, 502,Rec_16, false);
                remove_blue(0, 6, 48, 594,Rec_17, false);
                remove_blue( 1, 0, 144, 45,Rec_21, false);
                remove_blue( 1, 1, 144, 137,Rec_22, false);
                remove_blue( 1, 2, 144, 228,Rec_23, false);
                remove_blue( 1, 3, 144, 320,Rec_24, false);
                remove_blue( 1, 4, 144, 411,Rec_25, false);
                remove_blue( 1, 5, 144, 502,Rec_26, false);
                remove_blue( 1, 6, 144, 594,Rec_27, false);
                remove_blue( 2, 0, 240, 45,Rec_31, false);
                remove_blue( 2, 1, 240, 144,Rec_32, false);
                remove_blue( 2, 2, 240, 228,Rec_33, false);
                remove_blue( 2, 3, 240, 320,Rec_34, false);
                remove_blue( 2, 4, 240, 411,Rec_35, false);
                remove_blue( 2, 5, 240, 502,Rec_36, false);
                remove_blue( 2, 6, 240, 594,Rec_37, false);
                remove_blue( 3, 0, 336, 45,Rec_41, false);
                remove_blue( 3, 1, 336, 137,Rec_42, false);
                remove_blue( 3, 2, 336, 228,Rec_43, false);
                remove_blue( 3, 3, 336, 320,Rec_44, false);
                remove_blue( 3, 4, 336, 411,Rec_45, false);
                remove_blue( 3, 5, 336, 502,Rec_46, false);
                remove_blue( 3, 6, 336, 594,Rec_47, false);
                remove_blue( 4, 0, 432, 45,Rec_51, false);
                remove_blue( 4, 1, 432, 137,Rec_52, false);
                remove_blue( 4, 2, 432, 228,Rec_53, false);
                remove_blue( 4, 3, 432, 320,Rec_54, false);
                remove_blue( 4, 4, 432, 411,Rec_55, false);
                remove_blue( 4, 6, 432, 502,Rec_57, false);
                remove_blue( 4, 5, 432, 137,Rec_56, false);
            }
            else if (roomba3_pressed == true)
            {
                greenHead = null;
                green_list_started = false;
                remove_green( 0, 0, 48, 45,Rec_11, false);     
                remove_green(0, 1, 48, 137,Rec_12, false);
                remove_green(0, 2, 48, 228,Rec_13, false);
                remove_green( 0, 3, 48, 320,Rec_14, false);
                remove_green( 0, 4, 48, 411,Rec_15, false);
                remove_green( 0, 5, 48, 502,Rec_16, false);
                remove_green(0, 6, 48, 594,Rec_17, false);
                remove_green( 1, 0, 144, 45,Rec_21, false);
                remove_green( 1, 1, 144, 137,Rec_22, false);
                remove_green( 1, 2, 144, 228,Rec_23, false);
                remove_green( 1, 3, 144, 320,Rec_24, false);
                remove_green( 1, 4, 144, 411,Rec_25, false);
                remove_green( 1, 5, 144, 502,Rec_26, false);
                remove_green( 1, 6, 144, 594,Rec_27, false);
                remove_green( 2, 0, 240, 45,Rec_31, false);
                remove_green( 2, 1, 240, 144,Rec_32, false);
                remove_green( 2, 2, 240, 228,Rec_33, false);
                remove_green( 2, 3, 240, 320,Rec_34, false);
                remove_green( 2, 4, 240, 411,Rec_35, false);
                remove_green( 2, 5, 240, 502,Rec_36, false);
                remove_green( 2, 6, 240, 594,Rec_37, false);
                remove_green( 3, 0, 336, 45,Rec_41, false);
                remove_green( 3, 1, 336, 137,Rec_42, false);
                remove_green( 3, 2, 336, 228,Rec_43, false);
                remove_green( 3, 3, 336, 320,Rec_44, false);
                remove_green( 3, 4, 336, 411,Rec_45, false);
                remove_green( 3, 5, 336, 502,Rec_46, false);
                remove_green( 3, 6, 336, 594,Rec_47, false);
                remove_green( 4, 0, 432, 45,Rec_51, false);
                remove_green( 4, 1, 432, 137,Rec_52, false);
                remove_green( 4, 2, 432, 228,Rec_53, false);
                remove_green( 4, 3, 432, 320,Rec_54, false);
                remove_green( 4, 4, 432, 411,Rec_55, false);
                remove_green( 4, 6, 432, 502,Rec_57, false);
                remove_green( 4, 5, 432, 137,Rec_56, false);
            }
            else
            {
                clear_block(Rec_11, 0, 0,48, 45);
                clear_block(Rec_12,0,1, 48, 137);
                clear_block(Rec_13,0, 2, 48, 228);
                clear_block(Rec_14, 0, 3, 48, 320);
                clear_block(Rec_15, 0, 4, 48, 411);
                clear_block(Rec_16, 0, 5, 48, 502);
                clear_block(Rec_17, 0, 6, 48, 594);
                clear_block(Rec_21, 1, 0, 144, 45);
                clear_block(Rec_22, 1, 1, 144, 137);
                clear_block(Rec_23, 1, 2, 144, 228);
                clear_block(Rec_24, 1, 3, 144, 320);
                clear_block(Rec_25, 1, 4, 144, 411);
                clear_block(Rec_26, 1, 5, 144, 502);
                clear_block(Rec_27, 1, 6, 144, 594);
                clear_block(Rec_31, 2, 0, 240, 45);
                clear_block(Rec_32, 2, 1, 240, 144);
                clear_block(Rec_33, 2, 2, 240, 228);
                clear_block(Rec_34, 2, 3, 240, 320);
                clear_block(Rec_35, 2, 4, 240, 411);
                clear_block(Rec_36, 2, 5, 240, 502);
                clear_block(Rec_37, 2, 6, 240, 594);
                clear_block(Rec_41, 3, 0, 336, 45);
                clear_block(Rec_42, 3, 1, 336, 137);
                clear_block(Rec_43, 3, 2, 336, 228);
                clear_block(Rec_44, 3, 3, 336, 320);
                clear_block(Rec_46, 3, 5, 336, 502);
                clear_block(Rec_47, 3, 6, 336, 594);
                clear_block(Rec_51, 4, 0, 432, 45);
                clear_block(Rec_52, 4, 1, 432, 137);
                clear_block(Rec_53, 4, 2, 432, 228);
                clear_block(Rec_54, 4, 3, 432, 320);
                clear_block(Rec_55, 4, 4, 432, 411);
                clear_block(Rec_57, 4, 6, 432, 502);
                clear_block(Rec_56, 4, 5, 432, 137);
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
            if (printboards == true)
            {
                try
                {
                    pathNode current = new pathNode();
                    for (int i = 0; i < 3; i++)
                    {
                        if (i == 0)
                        {
                            current = redHead;
                            Console.Write("currRed: ");
                            Console.WriteLine(currRed);
                            Console.WriteLine("Red Roomba: ");
                            Console.Write("( ");
                            while (current != null)
                            {
                                Console.Write("[");
                                Console.Write(current.x_coord);
                                Console.Write(", ");
                                Console.Write(current.y_coord);
                                Console.Write("] ");
                                current = current.Next;
                            }
                            Console.Write(")");
                            Console.WriteLine("");

                        }
                        else if (i == 1)
                        {
                            current = blueHead;
                            Console.Write("currblue: ");
                            Console.WriteLine(currBlue);
                            Console.WriteLine("blue Roomba: ");
                            Console.Write("( ");
                            while (current != null)
                            {
                                Console.Write("[");
                                Console.Write(current.x_coord);
                                Console.Write(", ");
                                Console.Write(current.y_coord);
                                Console.Write("], ");
                                current = current.Next;
                            }
                            Console.WriteLine(")");
                        }
                        else if (i == 2)
                        {
                            current = greenHead;
                            Console.Write("currgreen: ");
                            Console.WriteLine(currGreen);
                            Console.WriteLine("green Roomba: ");
                            Console.Write("( ");
                            while (current != null)
                            {
                                Console.Write("[");
                                Console.Write(current.x_coord);
                                Console.Write(", ");
                                Console.Write(current.y_coord);
                                Console.Write("], ");
                                current = current.Next;
                            }
                            Console.WriteLine(")");
                        }
                    }
                }
                catch (Exception exc)
                {
                    Console.WriteLine(exc);
                }
            }


        }
    }
}