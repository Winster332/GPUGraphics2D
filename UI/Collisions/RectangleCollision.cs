using System;

namespace GPUGraphics2D.UI.Collisions
{
    public class RectangleCollision : UICollision
    {
        bool UICollision.IsCollisionMouse(Components.BaseUI ui, float x, float y)
        {
            var rect = ui as Components.RectangleComponent;
            
            if (x >= rect.X && x <= rect.Width + rect.X && y >= rect.Y && y <= rect.Y + rect.Hieght)
                return true;
            else return false;
        }
    }
}