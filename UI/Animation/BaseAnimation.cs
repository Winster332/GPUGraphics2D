namespace GPUGraphics2D.UI.Animation
{
    public class BaseAnimation
    {
        public float Begin { get; set; }
        public float End { get; set; }
        public float Step { get; set; }
        public float Time { get; set; }


        public void Update()
        {
            if (Time < End)
                Time += Step;
        }
    }
}