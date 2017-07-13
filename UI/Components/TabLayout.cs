using System;
using System.Drawing;
using System.Collections.Generic;

namespace GPUGraphics2D.UI.Components
{
    public class TabLayout<T> : HorizontalLayer
    {
        public List<Tab<T>> Tabs { get; set; }    
        public TabLayout(float x, float y, float w, float h, Color color)
        {
            this.X = x;
            this.Y = y;
            this.Width = w;
            this.Hieght = h;
            this.Background = new SolidBrush(color);
            this.Tabs = new List<Tab<T>>(); 
        }
        public Tab<T> AddTab(string name, T ui)
        {
            var t = new Tab<T>(ui, name);
            Tabs.Add(t);

            t.Width = 80;
            t.Hieght = this.Hieght;
            
            this.AddComponent(t);
            return t;
        }
        public class Tab<T> : RectangleComponent
        {
            public T UI { get; set; }
            public Action<T, string> Active { get; set; }
            public string Name { get; set; }
            public ButtonCircle ButtonClose { get; set; }
            public TextBlock Text { get; set; }
            public Tab(T UI, string name)
            {
                this.UI = UI;
                this.Name = name;
                this.ButtonClose = new ButtonCircle(this.Width / 2, this.Hieght / 2, 5, 
                    new Font("Arial", 14), "", Color.FromArgb(0, 255, 0));
                    this.AddComponent(ButtonClose);
                this.Text = new TextBlock(4, 5, name, "Arial", 13, StringAlignment.Center, Color.Black);
                Text.StringFormat.Alignment = StringAlignment.Near;
                Text.StringFormat.LineAlignment = StringAlignment.Near;
                this.AddComponent(Text);

                this.ListenerMouse.AddEnter(m => this.Color = Color.FromArgb(100 , 100, 100));
                this.ListenerMouse.AddLeave(m => this.Color = Color.FromArgb(10 , 10, 10));
            }
            public override void Draw()
            {
                this.ButtonClose.X = this.Width - this.ButtonClose.Radius * 3;
                this.ButtonClose.Y = this.Hieght / 2 - this.ButtonClose.Radius;
                UIRenderer.RectangleComponent(this);

                Childs.ForEach(c => c.Draw());
            }
            public override void Dispose()
            {
                
            }
        }
    }
}