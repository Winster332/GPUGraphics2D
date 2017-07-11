using System;
using System.Drawing;

namespace GPUGraphics2D.UI.Components
{
    public class Window : RectangleComponent
    {
        public SolidBrush Background { get; set; }
        public AbsoluteLayout Cap { get; set; }
        public TextBlock Title { get; set; }
        public ButtonCircle ButtonClose { get; set; }
        public ButtonCircle ButtonMinimized { get; set; }
        public ButtonCircle ButtonMaximized { get; set; }
        public new Color Color 
        {
            get { return Background.Color; }
            set { Background.Color = value; }
        }
        public override void Dispose()
        {
            ButtonClose.Dispose();
            ButtonMaximized.Dispose();
            
            this.Parent.Childs.Remove(this);
        }
        public Window(float x, float y, float w, float h, String text)
        {
            this.X = x;
            this.Y = y;
            this.Width = w;
            this.Hieght = h;
            this.Background = new SolidBrush(Color.FromArgb(50, 50, 50));
            
            this.Title = new TextBlock(x + this.Width / 2, y, text, "Arial", 12, 
            StringAlignment.Center, Color.FromArgb(192,192,192));

            this.Cap = new AbsoluteLayout(0, 0, w, 20, Color.FromArgb(100, 100, 100));
            //Cap.AddComponent(Title);
            ButtonClose = new ButtonCircle(2, 2, 7, new Font("Arial", 12), "", 
                Color.FromArgb(200, 70, 70));
            ButtonMaximized = new ButtonCircle(18, 2, 7, new Font("Arial", 12), "", 
                Color.FromArgb(220, 140, 0));
            ButtonClose.ListenerMouse.AddUp(m => Dispose());

            Cap.AddComponent(ButtonMaximized);
            Cap.AddComponent(ButtonClose);

            bool isDown = false;
            float mdx = 0, mdy = 0;
            Cap.ListenerMouse.AddDown(m => {
                isDown = true;
                mdx = m.X - m.UI.X;
                mdy = m.Y - m.UI.Y;
            });
            Cap.ListenerMouse.AddMove(m => {
                if (isDown)
                {
                    this.X = m.X - mdx;
                    this.Y = m.Y - mdy;
                }
            });
            Cap.ListenerMouse.AddUp(m => {
                isDown = false;
            });
            this.AddComponent(Cap);
        }

        public override void Draw()
        {
            Title.X = this.X + this.Width / 2;
            Title.Y = this.Y + 10; 
            
            UIRenderer.Window(this);
          //  UIRenderer.Layout(Cap);

            //UIRenderer.ButtonCircle(ButtonClose);
            Childs.ForEach(c => c.Draw());
            UIRenderer.TextBlock(Title);
        }
    }
}