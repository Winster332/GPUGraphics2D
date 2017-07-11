using System;
using System.Drawing;

namespace GPUGraphics2D.UI.Components
{
    public class TextBlock : BaseUI
    {
        public string Value { get; set; }
        public Font Font { get; set; }
        public Pen Pen { get; set; }
        public StringFormat StringFormat { get; set; }
        public new SolidBrush Color { get; set; }
        public TextBlock(float x, float y, string text, string fontString, float fontSize, StringAlignment aligment, Color color)
        {
            this.X = x;
            this.Y = y;
            this.Value = text;
            this.Font = new Font(fontString, fontSize);
            this.Color = new SolidBrush(color);
            this.StringFormat = new StringFormat();
            this.StringFormat.Alignment = aligment;
            this.StringFormat.LineAlignment = aligment;
        }
        public override void Draw()
        {
            UIRenderer.TextBlock(this);
        }

        public override void Dispose()
        {
        }
    }
}