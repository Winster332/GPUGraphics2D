namespace GPUGraphics2D.UI.Components.Screen
{
    public abstract class BaseScreen : RectangleComponent
    {
        public abstract void Initialize();
        public abstract void Paused();
        public abstract void Resume();
        public override void Dispose()
        {
        }
        public override void Draw()
        {
            Childs.ForEach(component => component.Draw());            
        }
        public void ShowWindow(Window window)
        {
            window.Parent = this;
            Childs.Add(window);
        }
    }
}