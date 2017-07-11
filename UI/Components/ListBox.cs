using System;
using System.Collections.Generic;

namespace GPUGraphics2D.UI.Components
{
    public class ListBox : RectangleComponent
    {
        public Scroll Scroll { get; set; }
        public List<RectangleComponent> Items { get; set; }
        public ListBox(float x, float y, float w, float h)
        {
            this.Width = w;
            this.Hieght = h;
            this.X = x;
            this.Y = y;
            this.Items = new List<RectangleComponent>();
            
            Scroll = new Scroll(0, 0, Width);
            this.AddComponent(Scroll);
        }
        public override void Dispose()
        {
        }
        public void AddItem(RectangleComponent item)
        {
            item.Parent = this;
            item.Width = this.Width - Scroll.Width - 1;
            item.X = Scroll.Width + 1;
            // item.UIRenderer = this.UIRenderer;
            Items.Add(item);
        }        
        public override void Draw()
        {
            UIRenderer.ListBox(this);

            Items.ForEach(item => UIRenderer.RectangleComponent(item));

            Childs.ForEach(c => c.Draw());
        }
    }
}