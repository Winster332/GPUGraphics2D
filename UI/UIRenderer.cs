using GPUGraphics2D.UI.Components;

namespace GPUGraphics2D.UI
{
    public interface UIRenderer
    {
        void TextBlock(TextBlock ui);
        void Border();
        void Layout(Layout ui);
        void ButtonRect(ButtonRect ui);
        void Window(Window ui);
        void ButtonCircle(ButtonCircle ui);
        void Scroll(Scroll ui);
        void ListBox(ListBox ui);
        void RectangleComponent(RectangleComponent ui);
    }
}