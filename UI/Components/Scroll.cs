using System;
using System.Drawing;

namespace GPUGraphics2D.UI.Components
{
    public class Scroll : RectangleComponent
    {
        public ButtonRect Button { get; set; }
        public float Value { get; set; } = 0;
        public int CountElements { get; set; } = 1;
        public event EventHandler ChangeScroll;
        public override void Dispose()
        {
        }
        public Scroll(float x, float y, float h)
        {
            this.Width = 10;
            this.Hieght = h;
            this.X = x;
            this.Y = y;
            this.Button = new ButtonRect(1, 1, 8, h / 2, new Font("", 12), "", Color.Green);
            bool isDown = false;
            float mdy = 0;
            Button.ListenerMouse.AddDown(m => {
                isDown = true;
                mdy = m.Y - Button.Y;
            });
            Button.ListenerMouse.AddMove(m => {
                if (isDown)
                {
                    var mvy = m.Y - this.Y - mdy;

                    if (mvy >= 0 && mvy <= Button.Hieght)
                    {
                        Button.Y = mvy;
                        Value = mvy;// * (this.Hieght / CountElements);
                        if (ChangeScroll != null)
                            ChangeScroll(this, null);
                    }
                }
            });
            Button.ListenerMouse.AddUp(m => {
                isDown = false;
            });
            this.AddComponent(Button);
        }

        public override void Draw()
        {
            UIRenderer.Scroll(this);

            Childs.ForEach(c => c.Draw());
        }
    }
}