using System;
using System.Drawing;

using Color = System.Drawing.Color;

using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;

namespace GPUGraphics2D.UI
{
    public class Application : OpenTK.GameWindow
    {
        public Graphics2D g2d;
        public UIRenderer Renderer { get; set; }
        public UI.Components.Screen.FactoryScreen FactoryScreen { get; set; }
        private Action<Application> actionLoaded;
        public Application(Action<Application> actionLoaded) : base(400, 300)
        {
            this.actionLoaded = actionLoaded;
            this.VSync = VSyncMode.On;
            this.WindowState = WindowState.Maximized;
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            
            GL.ClearColor(0f, 0f, 0f, 1f);
            GL.Enable(EnableCap.DepthTest);
            GL.EnableClientState(ArrayCap.ColorArray);
            GL.EnableClientState(ArrayCap.VertexArray);

            g2d = new Graphics2D(Width, Height);
            Renderer = new UIGraphicsDrawing(g2d);
            FactoryScreen = new UI.Components.Screen.FactoryScreen(this);

            actionLoaded(this);
        }
        protected override void OnUnload(EventArgs e)
        {
            FactoryScreen.Dispose();
        }
        protected override void OnResize(EventArgs e)
        {
            GL.Viewport(0, 0, Width, Height);

            g2d.Resize(Width, Height);
            FactoryScreen.Resize(Width, Height);
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

            // g2d.Graphics.FillEllipse(new SolidBrush(Color.Red), 400, 700, 50, 50);
            // g2d.Graphics.DrawPolygon(new Pen(Color.Green, 4), new Point[] 
            // {
            //     new Point(400, 100), new Point(450, 200),
            //     new Point(300, 400), new Point(150, 150)
            // });
            FactoryScreen.Draw();


            g2d.EndRender();

            SwapBuffers();
        }
    }
}