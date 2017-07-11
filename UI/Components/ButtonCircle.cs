using System;
using System.Drawing;

namespace GPUGraphics2D.UI.Components
{
    public class ButtonCircle : CircleComponent
    {
        public SolidBrush Background { get; set; }
        public new Color Color {
            get { return Background.Color; }
            set { Background.Color = value; }
        }
        public TextBlock Text { get; set; }
        public ButtonCircle(float x, float y, float rad, Font font, string text, Color color)
        {
            this.Text = new TextBlock(x, y, text, font.OriginalFontName, 
                font.Size, StringAlignment.Center, Color.FromArgb(192, 192, 192));
            this.X = x;
            this.Y = y;
            this.Radius = rad;
            this.Background = new SolidBrush(color);
            
            var baseColor = color;
            this.ListenerMouse.AddEnter(m => Color = Color.FromArgb(100, 100, 100));
            this.ListenerMouse.AddLeave(m => Color = color);
        }
        public override void Dispose()
        {
        }
        public override void Draw()
        {
            Text.X = this.X + this.Radius;
            Text.Y = this.Y + this.Radius;

            UIRenderer.ButtonCircle(this);
            UIRenderer.TextBlock(Text);
            
            Childs.ForEach(ui => ui.Draw());
        }
    }
}