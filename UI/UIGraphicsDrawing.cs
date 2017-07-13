using System;
using System.Drawing;

using GPUGraphics2D.UI.Components;

namespace GPUGraphics2D.UI
{
    public class UIGraphicsDrawing : UIRenderer
    {
        private Graphics2D g2d;
        public UIGraphicsDrawing(Graphics2D g2d)
        {
            this.g2d = g2d;
        }
        void UIRenderer.Border()
        {
        }

        void UIRenderer.ButtonCircle(ButtonCircle ui)
        {
            g2d.Graphics.FillEllipse(ui.Background, ui.X, ui.Y, ui.Radius * 2, ui.Radius * 2);
        }

        void UIRenderer.ButtonRect(ButtonRect ui)
        {
            g2d.Graphics.FillRectangle(ui.Background, ui.X, ui.Y, ui.Width, ui.Hieght);
        }

        void UIRenderer.Layout(Layout ui)
        {
            g2d.Graphics.FillRectangle(ui.Background, ui.X, ui.Y, ui.Width, ui.Hieght);
        }

        void UIRenderer.ListBox(ListBox ui)
        {
            g2d.Graphics.FillRectangle(new SolidBrush(Color.Gray), ui.X, ui.Y, ui.Width, ui.Hieght);
        }

        void UIRenderer.RectangleComponent(RectangleComponent ui)
        {
            g2d.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(92, 192, 192)), ui.X, ui.Y, ui.Width, ui.Hieght);
        }

        void UIRenderer.Scroll(Scroll ui)
        {
            g2d.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(192, 192, 192)), ui.X, ui.Y, ui.Width, ui.Hieght);
        }

        void UIRenderer.TextBlock(TextBlock ui)
        {
            g2d.Graphics.DrawString(ui.Value, ui.Font, ui.Color, ui.X, ui.Y, ui.StringFormat);
        }

        void UIRenderer.Window(Window ui)
        {
            g2d.Graphics.FillRectangle(ui.Background, ui.X, ui.Y, ui.Width, ui.Hieght);
        }
    }
}