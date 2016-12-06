/*Laura Berry(10111166)
 * Chris Miller(<ID NUMBER>)
 * 
 * citation:
 *  code taken in part from:
 *  -http://www.aforgenet.com/articles/step_to_stereo_vision/
 *  -http://www.aforgenet.com/
 *  -http://www.aforgenet.com/framework/docs/html/d7196dc6-8176-4344-a505-e7ade35c1741.htm
 *  -http://stackoverflow.com/questions/2006055/implementing-a-webcam-on-a-wpf-app-using-aforge-net
 */

//blue hex value ==1E85B0
// green hex value ==3C9275 
//red hex value == BD413F

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
        int whichFilter;
        bool roomba1_pressed, roomba2_pressed, roomba3_pressed, start_pressed;
        logic aname = new logic();
        AForge.Video.DirectShow.FilterInfoCollection videoDevices;
        AForge.Video.DirectShow.VideoCaptureDevice vidsource1, videosource2;

        public MainWindow()
        {
            start_pressed = roomba1_pressed = roomba2_pressed = roomba3_pressed = false;
            InitializeComponent();
            whichFilter = 1;
            aname.init();
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
            while (counter<2)
            {
                // set NewFrame event handler
                videoSource.NewFrame += new AForge.Video.NewFrameEventHandler(video_NewFrame);
                if (whichFilter==1)
                {
                    whichFilter = 2;
                }
                else if (whichFilter==2)
                {
                    whichFilter = 3;
                }
                else
                {
                    whichFilter = 1;
                }
                // wait until we have two acquired images
                /*camera1Acquired.WaitOne();
                camera2Acquired.WaitOne();*/
                counter++;
            }
            //timer
            //call getBarring

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
                Console.Write("can't get frame");
                Console.Write(ex);
            }
            System.Drawing.Bitmap bitmap = eventArgs.Frame;
            // create filter
            //AForge.Imaging.Filters.ColorFiltering colorFilter = new AForge.Imaging.Filters.ColorFiltering();
            // configure the filter


            /// create blob counter and configure it
            BlobCounter blobCounter = new BlobCounter();
            blobCounter.MinHeight = 50;
            blobCounter.MaxHeight = 93;
            blobCounter.MinWidth = 50;
            blobCounter.FilterBlobs = true;                                         // filter blobs by size
            blobCounter.ObjectsOrder = ObjectsOrder.Size;                           // order found object by size
            // grayscaling
            //AForge.Imaging.Filters.Grayscale grayFilter = new AForge.Imaging.Filters.Grayscale(0.2125, 0.7154, 0.0721);
            AForge.Imaging.Filters.ColorFiltering colorFilter = new AForge.Imaging.Filters.ColorFiltering();
            /*colorFilter.Red = new AForge.IntRange(20, 255);
            colorFilter.Green = new AForge.IntRange(59, 220);
            colorFilter.Blue = new AForge.IntRange(60, 230);*/
            colorFilter.Red = new AForge.IntRange(50, 130);
            colorFilter.Green = new AForge.IntRange(40, 150);
            colorFilter.Blue = new AForge.IntRange(30,170);
            Bitmap colorfiltered = colorFilter.Apply(bitmap);

            // locate blobs 
            blobCounter.ProcessImage(colorfiltered);
            System.Drawing.Rectangle[] rects = blobCounter.GetObjectsRectangles();
            // draw rectangle around all seen objects
            if (rects.Length > 0)
            {
                for (int i = 0; i < rects.Length; i++)
                {
                    System.Drawing.Rectangle objectRect = rects[i];
                    Graphics g = Graphics.FromImage(bitmap);
                    using (System.Drawing.Pen pen = new System.Drawing.Pen(System.Drawing.Color.FromArgb(160, 255, 160), 3))
                    {
                        g.DrawRectangle(pen, objectRect);
                        int x1 = makePositive(objectRect.Right, objectRect.Left);
                        x1 = x1 / 2;
                        x1 = objectRect.Left + x1;
                        int y1 = makePositive(objectRect.Top, objectRect.Bottom);
                        y1 = y1 / 2;
                        y1 = objectRect.Bottom + y1;
                        if (x1 < 0)
                        {
                            x1 = x1 * -1;
                        }
                        if (x1 > 639)
                        {
                            x1 = 639;
                        }
                        if (y1 < 0)
                        {
                            y1 = y1 * -1;
                        }
                        if (y1 > 479)
                        {
                            y1 = 479;
                        }
                        System.Drawing.Color a = bitmap.GetPixel(x1, y1);
                        write_to_struct(a, x1, y1, rects[i]);
                       }
                        g.Dispose();
                    }
                }
            }
        public void write_to_struct(System.Drawing.Color a, int x1, int y1, System.Drawing.Rectangle rec)
        {
            if (aname.helperMethod(aname.Object1, aname.Object2, aname.Object3, x1, y1))
            {
                Console.WriteLine("saw a thing 1");
                Console.Write(a);
                Console.Write(" x-coordinate" + x1);
                Console.WriteLine(" y-coordinate" + y1);
                aname.Object1.xCoord = x1;
                aname.Object1.yCoord = y1;
                aname.Object1.height = rec.Height;
                aname.Object1.width = rec.Width;
                aname.Object1.color = a;
                aname.Object1.taken = true;
            }
            else if (aname.helperMethod(aname.Object2, aname.Object1, aname.Object3, x1, y1))
            {
                Console.WriteLine("saw a thing 2");
                Console.Write(a);
                Console.Write(" x-coordinate: " + x1);
                Console.WriteLine(" y-coordinate: " + y1);
                aname.Object2.xCoord = x1;
                aname.Object2.yCoord = y1;
                aname.Object2.height = rec.Height;
                aname.Object2.width = rec.Width;
                aname.Object2.color = a;
                aname.Object2.taken = true;
            }
            else if (aname.helperMethod(aname.Object3, aname.Object2, aname.Object1, x1, y1))
            {
                Console.WriteLine("saw a thing 3");
                Console.Write(a);
                Console.Write(" x-coordinate" + x1);
                Console.WriteLine(" y-coordinate" + y1);
                aname.Object3.xCoord = x1;
                aname.Object3.yCoord = y1;
                aname.Object3.height = rec.Height;
                aname.Object3.width = rec.Width;
                aname.Object3.color = a;
                aname.Object3.taken = true;
            }
            else
            {
                return;
            }
        }
        public int makePositive(int a, int b)
        {
            int result =a -b;
             if (result <0)
            {
             result = result*-1;
            }
            return result;
        }

        /*
         * listens for which roomba is selected, ensures that only one is selected at a time
         */
        private void Din_clicked(object sender, RoutedEventArgs e)
        {
            if (!start_pressed)
            {
                if (roomba1_pressed == false)
                {
                    roomba1_pressed = true;
                    Roomba1.Width = 106;
                    Roomba1.Margin = new Thickness(401, 44, 0, 0);                 
                }
                else
                {
                    Roomba1.Width = 96;
                    Roomba1.Margin = new Thickness(411, 44, 0, 0);
                    roomba1_pressed = false;                    
                }
            }
        }
        private void Farore_clicked(object sender, RoutedEventArgs e)
        {
            if (!start_pressed)
            {
                if (roomba2_pressed == false)
                {
                    roomba2_pressed = true;
                    Roomba2.Width = 106;
                    Roomba2.Margin = new Thickness(401, 78, 0, 0);
                }
                else
                {
                    Roomba2.Width = 96;
                    Roomba2.Margin = new Thickness(411, 78, 0, 0);
                    roomba2_pressed = false;
                }
            }
        }
        private void Nayru_clicked(object sender, RoutedEventArgs e)
        {
            if (!start_pressed)
            {
                if (roomba3_pressed == false)
                {
                    roomba3_pressed = true;
                    Roomba3.Width = 106;
                    Roomba3.Margin = new Thickness(401, 112, 0, 0);
                }
                else
                {
                    Roomba3.Width = 96;
                    Roomba3.Margin = new Thickness(411, 112, 0, 0);
                    roomba3_pressed = false;
                }
            }
        }
      
        private void move_forward(object sender, RoutedEventArgs e)
        {
            if (roomba1_pressed==true)
            {
                csclient.StartClient("red", "f");
            }
            else if (roomba2_pressed==true)
            {
                csclient.StartClient("blue", "f");
            }
            else if (roomba3_pressed==true)
            {
                csclient.StartClient("green", "f");
            }
            else
            {
                return;
            }
        }

        private void turn_left(object sender, RoutedEventArgs e)
        {
            if (roomba1_pressed == true)
            {
                csclient.StartClient("red", "l");
            }
            else if (roomba2_pressed == true)
            {
                csclient.StartClient("blue", "l");
            }
            else if (roomba3_pressed == true)
            {
                csclient.StartClient("green", "l");
            }
            else
            {
                return;
            }

        }

        private void turn_right(object sender, RoutedEventArgs e)
        {
            if (roomba1_pressed == true)
            {
                csclient.StartClient("red", "r");
            }
            else if (roomba2_pressed == true)
            {
                csclient.StartClient("blue", "r");
            }
            else if (roomba3_pressed == true)
            {
                csclient.StartClient("green", "r");
            }
            else
            {
                return;
            }
        }

        private void turn_around_pressed(object sender, RoutedEventArgs e)
        {
            if (roomba1_pressed == true)
            {
                csclient.StartClient("red", "a");
            }
            else if (roomba2_pressed == true)
            {
                csclient.StartClient("blue", "a");
            }
            else if (roomba3_pressed == true)
            {
                csclient.StartClient("green", "a");
            }
            else
            {
                return;
            }
        }
    }
}