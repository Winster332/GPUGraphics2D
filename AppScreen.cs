using GPUGraphics2D.UI.Components;
using GPUGraphics2D.UI.Events.Mouse;
using GPUGraphics2D.UI.Components.Screen;

using System;
using System.Drawing;

namespace GPUGraphics2D
{
    public class AppScreen : BaseScreen
    {
        public override void Initialize()
        {
            var l1 = new AbsoluteLayout(10, 10, 300, 300, Color.White);
            var l2 = new AbsoluteLayout(10, 10, 100, 100, Color.Green);
            l1.AddComponent(l2);
            var tb = new TextBlock(50, 20, "STAS", "Arial", 11, StringAlignment.Center, Color.Black);
            var button = new ButtonCircle(10, 20, 25, new Font("Arial", 10), "BUTTON", Color.FromArgb(50, 50, 50));
            l2.AddComponent(button);
            l1.ListenerMouse.AddDown((m) => 
            {
                Console.WriteLine(m.UI);
            });
            AddComponent(l1);
            Window win = new Window(400, 400, 500, 500, "My Window");
            var scroll = new Scroll(10, 150, 20, 100);
            scroll.ChangeScroll+=(o, e)=>{ win.Title.Value = scroll.Value.ToString(); };
            win.AddComponent(scroll);
            this.AddComponent(win);
        }

        public override void Paused()
        {
        }

        public override void Resume()
        {
        }
    }
}