using GPUGraphics2D.UI.Components;

namespace GPUGraphics2D.UI
{
    public interface UIRenderer
    {
        void TextBlock(TextBlock ui);
        void Border();
        void Layout(Layout ui);
        void ButtonRect(ButtonRect buttonRect);
        void Window(Window window);
        void ButtonCircle(ButtonCircle buttonCircle);
        void Scroll(Scroll scroll);
    }
}