namespace GPUGraphics2D.UI.Components
{
    public class VerticalLayout : Layout
    {
        private float ContentValue = 0;
        public void AddComponent(RectangleComponent component)
        {
            component.Parent = this;
            this.Childs.Add(component);

            ContentValue += component.Hieght;
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
                this.Childs[i].Y = ContentValue;
                ContentValue += ((RectangleComponent)Childs[i]).Hieght;
            }
        }       
    }
}