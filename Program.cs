using System;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;

using Color = System.Drawing.Color;
using Pen = System.Drawing.Pen;
using SolidBrush = System.Drawing.SolidBrush;
using Point = System.Drawing.Point;

namespace GPUGraphics2D
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var app = new Application())
            {
                app.Run(60);
            }
        }
    }
    public class Application : GameWindow
    {
        public Graphics2D g2d;
        public Application() : base(400, 300)
        {
            this.VSync = VSyncMode.On;
            this.WindowState = WindowState.Maximized;
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            
            GL.ClearColor(.1f, .1f, .1f, 1f);
            GL.Enable(EnableCap.DepthTest);
            GL.EnableClientState(ArrayCap.ColorArray);
            GL.EnableClientState(ArrayCap.VertexArray);

            g2d = new Graphics2D(Width, Height);
        }
        protected override void OnUnload(EventArgs e)
        {
        }
        protected override void OnResize(EventArgs e)
        {
            GL.Viewport(0, 0, Width, Height);

            g2d.Resize(Width, Height);
        }
        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            if (Keyboard[Key.Escape])
                Exit();

			if (Keyboard[Key.F11])
                WindowState = WindowState.Fullscreen;
        }
        protected override void OnRenderFrame(FrameEventArgs e)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            GL.Enable(EnableCap.Blend);

            GL.BlendFunc(BlendingFactorSrc.One, BlendingFactorDest.DstAlpha);
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadIdentity();

            g2d.BeginRender();

            g2d.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            g2d.Graphics.DrawRectangle(new Pen(Color.Blue, 2), 500, 400, 100, 100);
            g2d.Graphics.FillEllipse(new SolidBrush(Color.Red), 400, 700, 50, 50);
            g2d.Graphics.DrawPolygon(new Pen(Color.Green, 4), new Point[] 
            {
                new Point(400, 100), new Point(450, 200),
                new Point(300, 400), new Point(150, 150)
            });

            g2d.EndRender();

            SwapBuffers();
        }
    }
}
