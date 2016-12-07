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
        bool roomba1_pressed, roomba2_pressed, roomba3_pressed, red_list_started, blue_list_started, green_list_started, start_pressed, manual_control;
        int currRed, currBlue, currGreen, biggest;
        arrayClass arr = new arrayClass();
        AForge.Video.DirectShow.FilterInfoCollection videoDevices;
        AForge.Video.DirectShow.VideoCaptureDevice vidsource1, videosource2;
        pathNode redHead, blueHead, greenHead;
        logic aname= new logic();

        public MainWindow()
        {
            biggest = 0;
            manual_control=red_list_started = blue_list_started = green_list_started=start_pressed = roomba1_pressed = roomba2_pressed = roomba3_pressed = false;
            currBlue = currGreen = currRed = 1;
            arr.init();
            aname.init();
            InitializeComponent();

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

                // set NewFrame event handler
                videoSource.NewFrame += new AForge.Video.NewFrameEventHandler(video_NewFrame);
                // wait until we have two acquired images
                /*camera1Acquired.WaitOne();
                camera2Acquired.WaitOne();*/

            
            //timer
            //call getBarring

            // signal to stop when you no longer need capturing
            //videoSource.SignalToStop( );
        }


        private void video_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            System.Drawing.Image image = (Bitmap)eventArgs.Frame.Clone();
            try
            {
                //Width==640 , Height==480
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
            System.Drawing.Bitmap bitmapRed = (Bitmap)eventArgs.Frame.Clone();
            System.Drawing.Bitmap bitmapBlue = (Bitmap)eventArgs.Frame.Clone();
            System.Drawing.Bitmap bitmapGreen = (Bitmap)eventArgs.Frame.Clone();
            // create filter
            //AForge.Imaging.Filters.ColorFiltering colorFilter = new AForge.Imaging.Filters.ColorFiltering();
            // configure the filter
            AForge.Imaging.Filters.ColorFiltering colorFilter = new AForge.Imaging.Filters.ColorFiltering();
            /*colorFilter.Red = new AForge.IntRange(60, 130);
            colorFilter.Green = new AForge.IntRange(40, 150);
            colorFilter.Blue = new AForge.IntRange(30, 170);*/
            colorFilter.Red = new AForge.IntRange(100, 200);
            colorFilter.Green = new AForge.IntRange(40, 150);
            colorFilter.Blue = new AForge.IntRange(30, 170);
            colorFilter.ApplyInPlace(bitmapRed);

            /// create blob counter and configure it
            BlobCounter blobCounter = new BlobCounter();
            blobCounter.MinHeight = 4;
            blobCounter.MaxHeight = 100;
            blobCounter.MaxWidth = 100;
            blobCounter.MinWidth = 4;
            blobCounter.FilterBlobs = true;                                         // filter blobs by size
            blobCounter.ObjectsOrder = ObjectsOrder.Size;                           // order found object by size
            // locate blobs 
            blobCounter.ProcessImage(bitmapRed);
            System.Drawing.Rectangle[] rects = blobCounter.GetObjectsRectangles();
            //Console.WriteLine(rects.Count());
            // draw rectangle around all seen objects
            if (rects.Length > 0)
            {
                for (int i = 0; i < rects.Length; i++)
                {
                    System.Drawing.Rectangle objectRect = rects[i];
                    Graphics g = Graphics.FromImage(image);
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
                        System.Drawing.Color a = bitmapRed.GetPixel(x1, y1);
                        write_to_struct(a, x1, y1, rects[i]);                                       //assigns rectangles to struct based on color)
                        g.Dispose();
                    }
                }
                colorFilter.Red = new AForge.IntRange(0,20);
                colorFilter.Green = new AForge.IntRange(0,20);
                colorFilter.Blue = new AForge.IntRange(0,20);
                colorFilter.ApplyInPlace(bitmapBlue);

                /// create blob counter and configure it
                BlobCounter blobCounterBlue = new BlobCounter();
                blobCounterBlue.MinHeight = 10;
                blobCounterBlue.MaxHeight = 100;
                blobCounterBlue.MaxWidth = 100;
                blobCounterBlue.MinWidth = 10;
                blobCounterBlue.FilterBlobs = true;                                         // filter blobs by size
                blobCounterBlue.ObjectsOrder = ObjectsOrder.Size;                           // order found object by size
                // locate blobs 
                blobCounterBlue.ProcessImage(bitmapBlue);
                System.Drawing.Rectangle[] rectsBlue = blobCounterBlue.GetObjectsRectangles();
                //Console.WriteLine(rects.Count());
                // draw rectangle around all seen objects
                if (rectsBlue.Length > 0)
                {
                    for (int i = 0; i < rectsBlue.Length; i++)
                    {
                        System.Drawing.Rectangle objectRectBlue = rectsBlue[i];
                        Graphics g = Graphics.FromImage(image);
                        using (System.Drawing.Pen pen = new System.Drawing.Pen(System.Drawing.Color.FromArgb(160, 255, 160), 3))
                        {
                            g.DrawRectangle(pen, objectRectBlue);
                            int x1 = makePositive(objectRectBlue.Right, objectRectBlue.Left);
                            x1 = x1 / 2;
                            x1 = objectRectBlue.Left + x1;
                            int y1 = makePositive(objectRectBlue.Top, objectRectBlue.Bottom);
                            y1 = y1 / 2;
                            y1 = objectRectBlue.Bottom + y1;
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
                            System.Drawing.Color a = bitmapRed.GetPixel(x1, y1);
                            write_to_struct(a, x1, y1, rectsBlue[i]);                                       //assigns rectangles to struct based on color)
                            g.Dispose();
                        }
                    }
                }

                colorFilter.Red = new AForge.IntRange(10, 90);
                colorFilter.Green = new AForge.IntRange(100, 255);
                colorFilter.Blue = new AForge.IntRange(10, 110);
                colorFilter.ApplyInPlace(bitmapBlue);

                /// create blob counter and configure it
                BlobCounter blobCounterGreen = new BlobCounter();
                blobCounterGreen.MinHeight = 10;
                blobCounterGreen.MaxHeight = 100;
                blobCounterGreen.MaxWidth = 100;
                blobCounterGreen.MinWidth = 10;
                blobCounterGreen.FilterBlobs = true;                                         // filter blobs by size
                blobCounterGreen.ObjectsOrder = ObjectsOrder.Size;                           // order found object by size
                // locate blobs 
                blobCounterGreen.ProcessImage(bitmapBlue);
                System.Drawing.Rectangle[] rectsGreen = blobCounterGreen.GetObjectsRectangles();
                //Console.WriteLine(rects.Count());
                // draw rectangle around all seen objects
                if (rectsGreen.Length > 0)
                {
                    for (int i = 0; i < rectsGreen.Length; i++)
                    {
                        System.Drawing.Rectangle objectRectGreen = rectsGreen[i];
                        Graphics g = Graphics.FromImage(image);
                        using (System.Drawing.Pen pen = new System.Drawing.Pen(System.Drawing.Color.FromArgb(160, 255, 160), 3))
                        {
                            g.DrawRectangle(pen, objectRectGreen);
                            int x1 = makePositive(objectRectGreen.Right, objectRectGreen.Left);
                            x1 = x1 / 2;
                            x1 = objectRectGreen.Left + x1;
                            int y1 = makePositive(objectRectGreen.Top, objectRectGreen.Bottom);
                            y1 = y1 / 2;
                            y1 = objectRectGreen.Bottom + y1;
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
                            System.Drawing.Color a = bitmapRed.GetPixel(x1, y1);
                            write_to_struct(a, x1, y1, rectsGreen[i]);                                       //assigns rectangles to struct based on color)
                            g.Dispose();
                        }
                    }
                }
            }
        }

      public int makePositive (int a, int b)
      {
          int c = a - b;
          if (c<0)
          {
              c = c * -1;
          }
          return c;
      }
        
/*
 * takes in a rectangle around a seen object. it checks if the seen object is one of the 3 colors and if so assigns it to 
 * a open struct. it stores the center point as well as the height and width of each object within the struct 
 */
      public void write_to_struct(System.Drawing.Color a, int x1, int y1, System.Drawing.Rectangle rec)
      {
          float color = a.GetHue();
          if (color < 5)//black
          {
              /*if (biggest < rec.Width)
              {
                  biggest = rec.Width;
              }
              Console.WriteLine(biggest);
              Console.WriteLine(rec.Size);*/
              if ((rec.Width < 15 || rec.Height < 15) && aname.blue1.taken == false)
              {
                  Console.WriteLine("saw a black thing 1");
                  aname.blue1.xCoord = x1;
                  aname.blue1.yCoord = y1;
                  aname.blue1.height = makePositive(rec.Right, rec.Left);
                  aname.blue1.width = makePositive(rec.Top, rec.Bottom);
                  aname.blue1.color = a;
                  aname.blue1.taken = true;
              }
              if ((16 <= rec.Width || rec.Height >= 16) && (rec.Height < 50 || rec.Width < 50) && aname.blue2.taken == false)
              {
                  Console.WriteLine("saw a black thing 2");
                  aname.blue2.xCoord = x1;
                  aname.blue2.yCoord = y1;
                  aname.blue2.height = makePositive(rec.Right, rec.Left);
                  aname.blue2.width = makePositive(rec.Top, rec.Bottom);
                  aname.blue2.color = a;
                  aname.blue2.taken = true;
              }
              if ((rec.Width >= 51 || rec.Height >= 51) && (rec.Height < 100 | rec.Width < 100) && aname.blue3.taken == false)
              {
                  Console.WriteLine("saw a black thing 3");
                  aname.blue3.xCoord = x1;
                  aname.blue3.yCoord = y1;
                  aname.blue3.height = makePositive(rec.Right, rec.Left);
                  aname.blue3.width = makePositive(rec.Top, rec.Bottom);
                  aname.blue3.color = a;
                  aname.blue3.taken = true;
              }
          }
          else if (color < 100)//red
          {
             // Console.WriteLine(rec.Size);
              if (rec.Width<15  && aname.red1.taken==false)
              {
                  Console.WriteLine("saw a red thing 1");
                  Console.WriteLine(rec.Size);
                  aname.red1.xCoord = x1;
                  aname.red1.yCoord = y1;
                  aname.red1.height = rec.Height;
                  aname.red1.width = rec.Width;
                  aname.red1.color = a;
                  aname.red1.taken = true;
              }
              else if (16 <= rec.Width && rec.Width< 50 && aname.red2.taken == false)
              {
                  Console.WriteLine("saw a red thing 2");
                  Console.WriteLine(rec.Size);
                  aname.red2.xCoord = x1;
                  aname.red2.yCoord = y1;
                  aname.red2.height = rec.Height;
                  aname.red2.width = rec.Width;
                  aname.red2.color = a;
                  aname.red2.taken = true;
              }
              else if (rec.Width>=51 && rec.Width < 100 && aname.red3.taken == false)
              {
                  Console.WriteLine("saw a red thing 3");
                  Console.WriteLine(rec.Size);
                  aname.red3.xCoord = x1;
                  aname.red3.yCoord = y1;
                  aname.red3.height = rec.Height;
                  aname.red3.width = rec.Width;
                  aname.red3.color = a;
                  aname.red3.taken = true;
              }
          }
          else if (color <250)
          {
              //Console.WriteLine("green: " + rec.Size);
              if ((rec.Width < 15 || rec.Height < 15) && aname.green1.taken == false)
              {
                  Console.WriteLine("saw a blue thing 1");
                  aname.green1.xCoord = x1;
                  aname.green1.yCoord = y1;
                  aname.green1.height = rec.Size.Height;
                  aname.green1.width = rec.Size.Width;
                  aname.green1.color = a;
                  aname.green1.taken = true;
              }
              if ((16 <= rec.Width || rec.Height >= 16) && (rec.Height < 50 || rec.Width < 50) && aname.green2.taken == false)
              {
                  Console.WriteLine("saw a blue thing 2");
                  aname.green2.xCoord = x1;
                  aname.green2.yCoord = y1;
                  aname.green2.height = rec.Size.Height;
                  aname.green2.width = rec.Size.Width;
                  aname.green2.color = a;
                  aname.green2.taken = true;
              }
              if ((rec.Width >= 51 || rec.Height >= 51) && (rec.Height < 100 | rec.Width < 100) && aname.green3.taken == false)
              {
                  Console.WriteLine("saw a blue thing 3");
                  aname.green3.xCoord = x1;
                  aname.green3.yCoord = y1;
                  aname.green3.height = rec.Size.Height;
                  aname.green3.width = rec.Size.Width;
                  aname.green3.color = a;
                  aname.green3.taken = true;
              }
          }
          
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
          test_printer();
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
          test_printer();
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
          test_printer();
      }

        /*
         * listens for drag over grid blocks, sends grid block and array index to change method
         */
        private void selected_11(object sender, MouseButtonEventArgs e)
        {
            if(!start_pressed){change(Rec_11, 0, 0, 45,48);}
        }
        private void selected_12(object sender, MouseButtonEventArgs e)
        {
            if (!start_pressed) { change(Rec_12, 0, 1, 137, 48); }
        }
        private void selected_13(object sender, MouseButtonEventArgs e)
        {
            if(!start_pressed){change(Rec_13, 0, 2, 228, 48);}
        }
        private void selected_14(object sender, MouseButtonEventArgs e)
        {
            if (!start_pressed) { change(Rec_14, 0, 3, 320, 48); }
        }
        private void selected_15(object sender, MouseButtonEventArgs e)
        {
            if(!start_pressed){change(Rec_15, 0, 4, 411, 48);}
        }
        private void selected_16(object sender, MouseButtonEventArgs e)
        {
            if (!start_pressed) { change(Rec_16, 0, 5, 502, 48); }
        }
        private void selected_17(object sender, MouseButtonEventArgs e)
        {
            if(!start_pressed){change(Rec_17, 0, 6, 594, 48);}
        }
        private void selected_21(object sender, MouseButtonEventArgs e)
        {
            if (!start_pressed) { change(Rec_21, 1, 0, 45, 48); }
        }
        private void selected_22(object sender, MouseButtonEventArgs e)
        {
            if(!start_pressed){change(Rec_22, 1, 1, 137,144);}
        }
        private void selected_23(object sender, MouseButtonEventArgs e)
        {
            if (!start_pressed) { change(Rec_23, 1, 2, 228, 144); }
        }
        private void selected_24(object sender, MouseButtonEventArgs e)
        {
            if (!start_pressed) { change(Rec_24, 1, 3, 320, 144); }
        }
        private void selected_25(object sender, MouseButtonEventArgs e)
        {
            if (!start_pressed) { change(Rec_25, 1, 4, 411, 144); }
        }
        private void selected_26(object sender, MouseButtonEventArgs e)
        {
            if (!start_pressed) { change(Rec_26, 1, 5, 502, 144); }
        }
        private void selected_27(object sender, MouseButtonEventArgs e)
        {
            if (!start_pressed) { change(Rec_27, 1, 6, 594, 144); }
        }
        private void selected_31(object sender, MouseButtonEventArgs e)
        {
            if (!start_pressed) { change(Rec_31, 2, 0, 45, 240); }
        }
        private void selected_32(object sender, MouseButtonEventArgs e)
        {
            if (!start_pressed) { change(Rec_32, 2, 1, 137, 240); }
        }
        private void selected_33(object sender, MouseButtonEventArgs e)
        {
            if (!start_pressed) { change(Rec_33, 2, 2, 228, 240); }
        }
        private void selected_34(object sender, MouseButtonEventArgs e)
        {
            if (!start_pressed) { change(Rec_34, 2, 3, 320, 240); }
        }
        private void selected_35(object sender, MouseButtonEventArgs e)
        {
            if (!start_pressed) { change(Rec_35, 2, 4, 411, 240); }
        }
        private void selected_36(object sender, MouseButtonEventArgs e)
        {
            if (!start_pressed) { change(Rec_36, 2, 5, 502, 240); }
        }
        private void selected_37(object sender, MouseButtonEventArgs e)
        {
            if (!start_pressed) { change(Rec_37, 2, 6, 594, 240); }
        }
        private void selected_41(object sender, MouseButtonEventArgs e)
        {
            if(!start_pressed){change(Rec_41, 3, 0, 45, 336);}
        }
        private void selected_42(object sender, MouseButtonEventArgs e)
        {
            if(!start_pressed){change(Rec_42, 3, 1, 137,336);}
        }
        private void selected_43(object sender, MouseButtonEventArgs e)
        {
            if (!start_pressed) { change(Rec_43, 3, 2, 228, 336); }
        }
        private void selected_44(object sender, MouseButtonEventArgs e)
        {
            if (!start_pressed) { change(Rec_44, 3, 3, 320, 336); }
        }
        private void selected_45(object sender, MouseButtonEventArgs e)
        {
            if (!start_pressed) { change(Rec_45, 3, 4, 411, 336); }
        }
        private void selected_46(object sender, MouseButtonEventArgs e)
        {
            if (!start_pressed) { change(Rec_46, 3, 5, 502, 336); }
        }
        private void selected_47(object sender, MouseButtonEventArgs e)
        {
            if (!start_pressed) { change(Rec_47, 3, 6, 594, 336); }
        }
        private void selected_51(object sender, MouseButtonEventArgs e)
        {
            if(!start_pressed){change(Rec_51, 4, 0, 45, 432);}
        }
        private void selected_52(object sender, MouseButtonEventArgs e)
        {
            if (!start_pressed) { change(Rec_52, 4, 1, 137, 432); }
        }
        private void selected_53(object sender, MouseButtonEventArgs e)
        {
            if (!start_pressed) { change(Rec_53, 4, 2, 228, 432); }
        }
        private void selected_54(object sender, MouseButtonEventArgs e)
        {
            if (!start_pressed) { change(Rec_54, 4, 3, 320, 432); }
        }
        private void selected_55(object sender, MouseButtonEventArgs e)
        {
            if (!start_pressed) { change(Rec_55, 4, 4, 411, 432); }
        }
        private void selected_56(object sender, MouseButtonEventArgs e)
        {
            if (!start_pressed) { change(Rec_56, 4, 5, 500, 432); }
        }
        private void selected_57(object sender, MouseButtonEventArgs e)
        {
            if (!start_pressed) { change(Rec_57, 4, 6, 594, 432); }
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
                    currRed++;
                    red_list_started = true;
                }
                else
                {
                    pathNode newNode = new pathNode();
                    newNode.x_coord=x; 
                    newNode.y_coord=y;
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
                    currBlue++;
                    blue_list_started = true;
                }
                else
                {
                    pathNode newNode = new pathNode();
                    newNode.x_coord=x; 
                    newNode.y_coord=y;
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
                    currGreen++;
                    green_list_started = true;
                }
                else
                {
                    pathNode newNode = new pathNode();
                    newNode.x_coord=x; 
                    newNode.y_coord=y;
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
            }
            else if (arr.board[x, y] == 7)
            {
                Rec.Fill = new SolidColorBrush(System.Windows.Media.Color.FromRgb(11, 162, 94));
                arr.board[x, y] = 6;
            }
            else if (arr.board[x, y] == 4)
            {
                Rec.Fill = new SolidColorBrush(System.Windows.Media.Color.FromRgb(13, 41, 168));
                arr.board[x, y] = 2;
            }
            else if (arr.board[x, y] == 5)
            {
                Rec.Fill = new SolidColorBrush(System.Windows.Media.Color.FromRgb(7, 147, 0));
                arr.board[x, y] = 3;
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
            }
            else if (arr.board[x, y] == 7)
            {
                Rec.Fill = new SolidColorBrush(System.Windows.Media.Color.FromRgb(188, 226, 14));
                arr.board[x, y] = 5;
            }
            else if (arr.board[x, y] == 4)
            {
                Rec.Fill = new SolidColorBrush(System.Windows.Media.Color.FromRgb(176, 5, 5));
                arr.board[x, y] = 1;
            }
            else if (arr.board[x, y] == 6)
            {
                Rec.Fill = new SolidColorBrush(System.Windows.Media.Color.FromRgb(7, 147, 0));
                arr.board[x, y] = 3;
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
            }
            else if (arr.board[x, y] == 7)
            {
                Rec.Fill = new SolidColorBrush(System.Windows.Media.Color.FromRgb(124, 21, 226));
                arr.board[x, y] = 4;
            }
            else if (arr.board[x, y] == 5)
            {
                Rec.Fill = new SolidColorBrush(System.Windows.Media.Color.FromRgb(176, 5, 5));
                arr.board[x, y] = 1;
            }
            else if (arr.board[x, y] == 6)
            {
                Rec.Fill = new SolidColorBrush(System.Windows.Media.Color.FromRgb(13, 41, 168));
                arr.board[x, y] = 2;
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
            else if (roomba3_pressed == true)
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
            else if (roomba2_pressed == true)
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
                    Rec.Fill = new SolidColorBrush(System.Windows.Media.Color.FromRgb(9,9,10));
                    arr.board[x, y] = 3;
                    add_to_linkedList(midX, midY);
                }
                else
                {
                    remove_green(x, y, midX, midY, Rec, true);
                }
            }
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
            clear_board_2();
        }

        public void clear_board_2()
        {
            if (roomba1_pressed == true)
            {
                redHead = null;
                red_list_started = false;
                remove_red(0, 0, 48, 45, Rec_11, false);
                remove_red(0, 1, 48, 137, Rec_12, false);
                remove_red(0, 2, 48, 228, Rec_13, false);
                remove_red(0, 3, 48, 320, Rec_14, false);
                remove_red(0, 4, 48, 411, Rec_15, false);
                remove_red(0, 5, 48, 502, Rec_16, false);
                remove_red(0, 6, 48, 594, Rec_17, false);
                remove_red(1, 0, 144, 45, Rec_21, false);
                remove_red(1, 1, 144, 137, Rec_22, false);
                remove_red(1, 2, 144, 228, Rec_23, false);
                remove_red(1, 3, 144, 320, Rec_24, false);
                remove_red(1, 4, 144, 411, Rec_25, false);
                remove_red(1, 5, 144, 502, Rec_26, false);
                remove_red(1, 6, 144, 594, Rec_27, false);
                remove_red(2, 0, 240, 45, Rec_31, false);
                remove_red(2, 1, 240, 144, Rec_32, false);
                remove_red(2, 2, 240, 228, Rec_33, false);
                remove_red(2, 3, 240, 320, Rec_34, false);
                remove_red(2, 4, 240, 411, Rec_35, false);
                remove_red(2, 5, 240, 502, Rec_36, false);
                remove_red(2, 6, 240, 594, Rec_37, false);
                remove_red(3, 0, 336, 45, Rec_41, false);
                remove_red(3, 1, 336, 137, Rec_42, false);
                remove_red(3, 2, 336, 228, Rec_43, false);
                remove_red(3, 3, 336, 320, Rec_44, false);
                remove_red(3, 4, 336, 411, Rec_45, false);
                remove_red(3, 5, 336, 502, Rec_46, false);
                remove_red(3, 6, 336, 594, Rec_47, false);
                remove_red(4, 0, 432, 45, Rec_51, false);
                remove_red(4, 1, 432, 137, Rec_52, false);
                remove_red(4, 2, 432, 228, Rec_53, false);
                remove_red(4, 3, 432, 320, Rec_54, false);
                remove_red(4, 4, 432, 411, Rec_55, false);
                remove_red(4, 6, 432, 502, Rec_57, false);
                remove_red(4, 5, 432, 137, Rec_56, false);
            }
            else if (roomba2_pressed == true)
            {
                blueHead = null;
                blue_list_started = false;
                remove_blue(0, 0, 48, 45, Rec_11, false);
                remove_blue(0, 1, 48, 137, Rec_12, false);
                remove_blue(0, 2, 48, 228, Rec_13, false);
                remove_blue(0, 3, 48, 320, Rec_14, false);
                remove_blue(0, 4, 48, 411, Rec_15, false);
                remove_blue(0, 5, 48, 502, Rec_16, false);
                remove_blue(0, 6, 48, 594, Rec_17, false);
                remove_blue(1, 0, 144, 45, Rec_21, false);
                remove_blue(1, 1, 144, 137, Rec_22, false);
                remove_blue(1, 2, 144, 228, Rec_23, false);
                remove_blue(1, 3, 144, 320, Rec_24, false);
                remove_blue(1, 4, 144, 411, Rec_25, false);
                remove_blue(1, 5, 144, 502, Rec_26, false);
                remove_blue(1, 6, 144, 594, Rec_27, false);
                remove_blue(2, 0, 240, 45, Rec_31, false);
                remove_blue(2, 1, 240, 144, Rec_32, false);
                remove_blue(2, 2, 240, 228, Rec_33, false);
                remove_blue(2, 3, 240, 320, Rec_34, false);
                remove_blue(2, 4, 240, 411, Rec_35, false);
                remove_blue(2, 5, 240, 502, Rec_36, false);
                remove_blue(2, 6, 240, 594, Rec_37, false);
                remove_blue(3, 0, 336, 45, Rec_41, false);
                remove_blue(3, 1, 336, 137, Rec_42, false);
                remove_blue(3, 2, 336, 228, Rec_43, false);
                remove_blue(3, 3, 336, 320, Rec_44, false);
                remove_blue(3, 4, 336, 411, Rec_45, false);
                remove_blue(3, 5, 336, 502, Rec_46, false);
                remove_blue(3, 6, 336, 594, Rec_47, false);
                remove_blue(4, 0, 432, 45, Rec_51, false);
                remove_blue(4, 1, 432, 137, Rec_52, false);
                remove_blue(4, 2, 432, 228, Rec_53, false);
                remove_blue(4, 3, 432, 320, Rec_54, false);
                remove_blue(4, 4, 432, 411, Rec_55, false);
                remove_blue(4, 6, 432, 502, Rec_57, false);
                remove_blue(4, 5, 432, 137, Rec_56, false);
            }
            else if (roomba3_pressed == true)
            {
                greenHead = null;
                green_list_started = false;
                remove_green(0, 0, 48, 45, Rec_11, false);
                remove_green(0, 1, 48, 137, Rec_12, false);
                remove_green(0, 2, 48, 228, Rec_13, false);
                remove_green(0, 3, 48, 320, Rec_14, false);
                remove_green(0, 4, 48, 411, Rec_15, false);
                remove_green(0, 5, 48, 502, Rec_16, false);
                remove_green(0, 6, 48, 594, Rec_17, false);
                remove_green(1, 0, 144, 45, Rec_21, false);
                remove_green(1, 1, 144, 137, Rec_22, false);
                remove_green(1, 2, 144, 228, Rec_23, false);
                remove_green(1, 3, 144, 320, Rec_24, false);
                remove_green(1, 4, 144, 411, Rec_25, false);
                remove_green(1, 5, 144, 502, Rec_26, false);
                remove_green(1, 6, 144, 594, Rec_27, false);
                remove_green(2, 0, 240, 45, Rec_31, false);
                remove_green(2, 1, 240, 144, Rec_32, false);
                remove_green(2, 2, 240, 228, Rec_33, false);
                remove_green(2, 3, 240, 320, Rec_34, false);
                remove_green(2, 4, 240, 411, Rec_35, false);
                remove_green(2, 5, 240, 502, Rec_36, false);
                remove_green(2, 6, 240, 594, Rec_37, false);
                remove_green(3, 0, 336, 45, Rec_41, false);
                remove_green(3, 1, 336, 137, Rec_42, false);
                remove_green(3, 2, 336, 228, Rec_43, false);
                remove_green(3, 3, 336, 320, Rec_44, false);
                remove_green(3, 4, 336, 411, Rec_45, false);
                remove_green(3, 5, 336, 502, Rec_46, false);
                remove_green(3, 6, 336, 594, Rec_47, false);
                remove_green(4, 0, 432, 45, Rec_51, false);
                remove_green(4, 1, 432, 137, Rec_52, false);
                remove_green(4, 2, 432, 228, Rec_53, false);
                remove_green(4, 3, 432, 320, Rec_54, false);
                remove_green(4, 4, 432, 411, Rec_55, false);
                remove_green(4, 6, 432, 502, Rec_57, false);
                remove_green(4, 5, 432, 137, Rec_56, false);
            }
            else
            {
                clear_block(Rec_11, 0, 0, 48, 45);
                clear_block(Rec_12, 0, 1, 48, 137);
                clear_block(Rec_13, 0, 2, 48, 228);
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
                clear_block(Rec_45, 3, 4, 336, 411);
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
            if (start_pressed == false)
            {
                start_pressed = true;
                roomba1_pressed = roomba2_pressed = roomba3_pressed = false;
                Roomba1.Margin = new Thickness(411, 44, 0, 0);
                Roomba2.Margin = new Thickness(411, 78, 0, 0);
                Roomba3.Margin = new Thickness(411, 112, 0, 0);
                clear_button.Background = new SolidColorBrush(System.Windows.Media.Color.FromRgb(221, 221, 221));
                bool filled = aname.three_filled(aname.blue1, aname.blue2, aname.blue3);
                if (filled == true)
                {
                    try
                    {
                        aname.get_barring(aname.blue1, aname.blue2, aname.blue3, blueHead.x_coord, blueHead.y_coord, blue_list_started);   //get the barring and decide if and where to turn
                    }
                    catch (Exception except)
                    {
                        Console.Write("objects are null");
                        Console.WriteLine(except);
                    }
                }
            }
        }

        private void pause_clicked(object sender, RoutedEventArgs e)
        {
            if (start_pressed)
            {
                start_pressed = false;
                clear_board_2();
                try
                {
                    //aname.stop(aname.FaroreID);
                }
                catch (Exception exc)
                {
                    Console.Write("problem with pause button");
                    Console.WriteLine(exc);
                }
            }
        }

        private void switch_to_maual(object sender, RoutedEventArgs e)
        {
            manual_control = true;
            control_switcher.Visibility = System.Windows.Visibility.Collapsed;
            Start_button.Visibility= System.Windows.Visibility.Collapsed;
            clear_button.Visibility = System.Windows.Visibility.Collapsed;
            Pause_button.Visibility = System.Windows.Visibility.Collapsed;
            Automatic_button.Visibility = System.Windows.Visibility.Visible;
            Roomba1.Margin = new Thickness(411, 44, 0, 0);
            roomba1_pressed = false;
            Roomba2.Margin = new Thickness(411, 78, 0, 0);
            roomba2_pressed = false;
            Roomba3.Margin = new Thickness(411, 112, 0, 0);
            roomba3_pressed = false;
            clear_board_2();
            forward_button.Visibility = System.Windows.Visibility.Visible;
            left_button.Visibility = System.Windows.Visibility.Visible;
            right_button.Visibility = System.Windows.Visibility.Visible;
            turn_around.Visibility = System.Windows.Visibility.Visible;
        }
        public void test_printer()
        {
            if (roomba1_pressed==true)
            {
                Console.WriteLine("red selected");
            }
            if (roomba2_pressed==true)
            {
                Console.WriteLine("blue selected");
            }
            if (roomba3_pressed==true)
            {
                Console.WriteLine("green selected");
            }
        }

        private void move_forward(object sender, RoutedEventArgs e)
        {
            test_printer();
            if (roomba1_pressed == true)
            {
                csclient.StartClient("red", "f");
            }
            if (roomba2_pressed == true)
            {
                csclient.StartClient("blue", "f");
            }
            if (roomba3_pressed == true)
            {
                csclient.StartClient("green", "f");
            }
        }

        private void turn_left(object sender, RoutedEventArgs e)
        {
            test_printer();
            if (roomba1_pressed == true)
            {
                csclient.StartClient("red", "l");
            }
            if (roomba2_pressed == true)
            {
                csclient.StartClient("blue", "l");
            }
            if (roomba3_pressed == true)
            {
                csclient.StartClient("green", "l");
            }
        }

        private void turn_right(object sender, RoutedEventArgs e)
        {
            test_printer();
            if (roomba1_pressed == true)
            {
                csclient.StartClient("red", "r");
            }
            if (roomba2_pressed == true)
            {
                csclient.StartClient("blue", "r");
            }
            if (roomba3_pressed == true)
            {
                Console.WriteLine("in green");
                csclient.StartClient("green", "r");
            }
        }

        private void turn_around_pressed(object sender, RoutedEventArgs e)
        {
            test_printer();
            if (roomba1_pressed == true)
            {
                csclient.StartClient("red", "a");
            }
            if (roomba2_pressed == true)
            {
                csclient.StartClient("blue", "a");
            }
            if (roomba3_pressed == true)
            {
                csclient.StartClient("green", "a");
            }
        }

        private void auto_control(object sender, RoutedEventArgs e)
        {
            manual_control = false;
            start_pressed = false;
            control_switcher.Visibility = System.Windows.Visibility.Visible;
            Start_button.Visibility = System.Windows.Visibility.Visible;
            clear_button.Visibility = System.Windows.Visibility.Visible;
            Pause_button.Visibility = System.Windows.Visibility.Visible;
            Automatic_button.Visibility = System.Windows.Visibility.Collapsed;
            Roomba1.Margin = new Thickness(411, 44, 0, 0);
            roomba1_pressed = false;
            Roomba2.Margin = new Thickness(411, 78, 0, 0);
            roomba2_pressed = false;
            Roomba3.Margin = new Thickness(411, 112, 0, 0);
            roomba3_pressed = false;
            clear_board_2();
            forward_button.Visibility = System.Windows.Visibility.Collapsed;
            left_button.Visibility = System.Windows.Visibility.Collapsed;
            right_button.Visibility = System.Windows.Visibility.Collapsed;
            turn_around.Visibility = System.Windows.Visibility.Collapsed;
        }
    }
}