using System;
using GPUGraphics2D.UI.Components;

namespace GPUGraphics2D.UI.Collisions
{
    public class CircleCollision : Collisions.UICollision
    {
        bool UICollision.IsCollisionMouse(BaseUI ui, float x, float y)
        {
            var c = ui as Components.CircleComponent;
            float distance = (float)Math.Sqrt(Math.Pow(c.X + c.Radius - x, 2) + Math.Pow(c.Y + c.Radius - y, 2));
            
            if (distance <= c.Radius)
                return true;
            else return false;
        }
    }
}