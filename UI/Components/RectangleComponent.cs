using System;

namespace GPUGraphics2D.UI.Components
{
    public abstract class RectangleComponent : BaseUI
    {
        public float Width { get; set; }
        public float Hieght { get; set; }
        public RectangleComponent()
        {
            Collision = new Collisions.RectangleCollision();
        }
    }
}