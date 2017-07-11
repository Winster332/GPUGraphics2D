namespace GPUGraphics2D.UI.Events.Mouse
{
    public class MouseDate
    {
        public float X { get; set; }
        public float Y { get; set; }
        public Components.BaseUI UI { get; set; }
        public MouseDate(float x, float y, Components.BaseUI ui)
        {
            this.X = x;
            this.Y = y;
            this.UI = ui;
        }
    }
}