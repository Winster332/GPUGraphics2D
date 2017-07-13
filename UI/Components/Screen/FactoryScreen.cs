using GPUGraphics2D.UI.Components.Screen;
using System.Collections.Generic;

namespace GPUGraphics2D.UI.Components.Screen
{
    public class FactoryScreen
    {
        public List<BaseScreen> PoolScreens { get; set; }
        public BaseScreen CurrentScreen { get; set; }
        private Application app;
        public FactoryScreen(Application app)
        {   
            this.app = app;
            this.PoolScreens = new List<BaseScreen>();
            CurrentScreen = null;

            this.app.MouseDown += (o, e) => CurrentScreen?.ListenerMouse.OnMouseDown(CurrentScreen, e.X, e.Y);
            this.app.MouseMove += (o, e) => CurrentScreen?.ListenerMouse.OnMouseMove(CurrentScreen, e.X, e.Y);
            this.app.MouseUp += (o, e) => CurrentScreen?.ListenerMouse.OnMouseUp(CurrentScreen, e.X, e.Y);
        }
        public void Draw()
        {
            CurrentScreen?.Draw();
        }
        public void Dispose()
        {
            CurrentScreen?.Dispose();
        }
        public void Select(int index)
        {
            CurrentScreen.Paused();
            CurrentScreen = PoolScreens[index];
            CurrentScreen.Resume();
        }
        public void SetScteen(BaseScreen scrren)
        {
            CurrentScreen?.Paused();
            CurrentScreen?.Dispose();
            CurrentScreen = scrren;
            CurrentScreen.UIRenderer = app.Renderer;
            CurrentScreen.Width = app.Width;
            CurrentScreen.Hieght = app.Height;
            CurrentScreen.Initialize();
            CurrentScreen.Resume();
        }
        public void Resize(float w, float h)
        {
            CurrentScreen.Width = w;
            CurrentScreen.Hieght = h;
        }
    }
}