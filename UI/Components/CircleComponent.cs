using System;

namespace GPUGraphics2D.UI.Components
{
    public abstract class CircleComponent : BaseUI
    {
        public float Radius { get; set; }
        public CircleComponent()
        {
            Collision = new Collisions.CircleCollision();
        }
    }
}