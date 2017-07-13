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

            LayoutGrid grid = new LayoutGrid(400, 100, 400, 400, Color.FromArgb(0, 0, 0));
            grid.InitGrid(2, 2);
            grid.AddComponent(0, 0, new AbsoluteLayout(0, 0, 10, 10, Color.Green));
            grid.AddComponent(1, 0, new AbsoluteLayout(0, 0, 10, 10, Color.Red));
            grid.AddComponent(0, 1, new AbsoluteLayout(0, 0, 10, 10, Color.Blue));
            grid.AddComponent(1, 1, new AbsoluteLayout(0, 0, 10, 10, Color.Gray));
            this.AddComponent(grid);
            
            Window win = new Window(400, 400, 500, 500, "My Window");
            var scroll = new Scroll(10, 150, 100);
            var lsb = new ListBox(200, 200, 200, 200);
            scroll.ChangeScroll+=(o, e)=>{ win.Title.Value = scroll.Value.ToString(); };
            win.AddComponent(lsb);
            lsb.AddItem(new ButtonRect(0, 0, 20, 40, new Font("Arial", 12), "Sta",Color.Blue));
            this.AddComponent(win);

            var tabs = new TabLayout<TextBlock>(100, 100, 400, 30, Color.Silver);
            this.AddComponent(tabs);
            for (int i = 0; i < 3; i++)
                tabs.AddTab($"File {i}.cs", new TextBlock(10, 10, "1", "Arial", 14, 
                StringAlignment.Center, Color.Red)).Active = (name, ui) => {
                    Console.WriteLine("name: " + name);
                };
        }

        public override void Paused()
        {
        }

        public override void Resume()
        {
        }
    }
}