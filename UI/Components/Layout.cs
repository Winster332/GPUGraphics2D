using System;
using System.Drawing;

namespace GPUGraphics2D.UI.Components
{
    public class Layout : RectangleComponent
    {
        public SolidBrush Background { get; set; }
        public new Color Color {
            get { return Background.Color; }
            set { Background.Color = value; }
        }
        public Layout()
        {
        }
        public override void Dispose()
        {
        }
        public override void Draw()
        {
            UIRenderer.Layout(this);
            Childs.ForEach(component => component.Draw());
        }
    }
}