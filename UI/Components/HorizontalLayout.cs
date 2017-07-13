using System;

namespace GPUGraphics2D.UI.Components
{
    public class HorizontalLayer : Layout
    {
        private float ContentValue = 0;
        public void AddComponent(RectangleComponent component)
        {
            component.Parent = this;
            component.X = ContentValue;
            this.Childs.Add(component);

            ContentValue += component.Width;
        }
        public void RemoveComponent(BaseUI component)
        {
            ContentValue = 0;
            this.Childs.Remove(component);
            Sort();
        }
        public void Sort()
        {
            for (var i = 0; i < this.Childs.Count; i++) 
            {
                this.Childs[i].X = ContentValue;
                ContentValue += ((RectangleComponent)Childs[i]).Width;
            }
        }
    }
}