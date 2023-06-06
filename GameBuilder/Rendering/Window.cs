using GameBuilder._Math;
using GameBuilder.Game;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace GameBuilder.Rendering
{
    internal class Window
    {
        public static int windowWidth;
        public static int windowHeight;

        public static BufferedGraphics buffer;
        public static bool IsReady = false;
        public static WindowInstance windowInstance;

        static Thread thread;

        public static void CreateWindow()
        {
            thread = new Thread(WindowHostThread);
            thread.Start();
        }


        // Frame Rate calculations
        static DateTime LastFrameTime = DateTime.MaxValue;
        static double AvrageFrameTime = 0;
        static int a = 0;
        // - - -

        public static void TickWindow()
        {
            if (windowInstance.Width != windowWidth || windowInstance.Height != windowHeight)
            {
                Debug.SendErrorMessage("Updated window size to " + windowInstance.Width + "x " + windowInstance.Height + "y");
                BufferedGraphicsContext context = BufferedGraphicsManager.Current;
                buffer = context.Allocate(windowInstance.CreateGraphics(), new Rectangle(0, 0, windowInstance.Width, windowInstance.Height));
                windowWidth = windowInstance.Width;
                windowHeight = windowInstance.Height;
                Graphics graphics = Window.buffer.Graphics;
                graphics.CompositingMode = CompositingMode.SourceOver;
                graphics.CompositingQuality = CompositingQuality.HighSpeed;
                graphics.PixelOffsetMode = PixelOffsetMode.None;
                graphics.InterpolationMode = InterpolationMode.NearestNeighbor;
            }


            // Frame Rate Calculations
            double FrameTime = DateTime.Now.Subtract(LastFrameTime).TotalMilliseconds;
            AvrageFrameTime += FrameTime;
            a++;
            if (a > 10)
            {
                a = 0;
                AvrageFrameTime = 0;
            }
            LastFrameTime = DateTime.Now;
            // - - -

        }

        static void WindowHostThread()
        {
            windowInstance = new WindowInstance();
            
            Application.Run(windowInstance);
        }

    }

    class WindowInstance : Form
    {
        public WindowInstance()
        {
            this.DoubleBuffered = true;

            this.WindowState = FormWindowState.Maximized;
            Window.windowWidth = this.Width;
            Window.windowHeight = this.Height;
            BufferedGraphicsContext context = BufferedGraphicsManager.Current;
            Window.buffer = context.Allocate(this.CreateGraphics(), new Rectangle(0, 0, this.Width, this.Height));
            Graphics graphics = Window.buffer.Graphics;
            graphics.CompositingMode = CompositingMode.SourceOver;
            graphics.CompositingQuality = CompositingQuality.HighSpeed;
            graphics.PixelOffsetMode = PixelOffsetMode.None;
            graphics.InterpolationMode = InterpolationMode.Default;
            Window.IsReady = true;
        }
    }
}
