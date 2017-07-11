namespace GPUGraphics2D.UI.Components
{
    public class AbsoluteLayout : Layout
    {
        public AbsoluteLayout(float x, float y, float w, float h, System.Drawing.Color color)
        {
            this.X = x;
            this.Y = y;
            this.Width = w;
            this.Hieght = h;
            this.Background = new System.Drawing.SolidBrush(color);
        }
    }
}