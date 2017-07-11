using System;
using System.Drawing;
using System.Collections.Generic;

using GPUGraphics2D.UI.Events.Mouse;

namespace GPUGraphics2D.UI.Components
{
    public abstract class BaseUI : IDisposable
    {
        public Color Color { get; set; }
        private float _x, _y;
        public float FactX { get { return _x; } private set { _x = value; } }
        public float FactY { get { return _y; } private set { _y = value; } }
        public float X
        {
            get 
            {
                return Parent != null ? (Parent.X + _x) : _x;
            }
            set 
            {
                _x = value;
            }
        }
        public float Y
        {
            get 
            {
                return Parent != null ? (Parent.Y + _y) : _y;
            }
            set 
            {
                _y = value;
            }
        }
        public List<BaseUI> Childs { get; set; }
        public BaseUI Parent { get; set; }
        private UIRenderer uirenderer;
        public UIRenderer UIRenderer 
        {
            get { return Parent != null ? Parent.UIRenderer : uirenderer; }
            set { uirenderer = value; }
        }
        public Collisions.UICollision Collision { get; set; }
        public ListenerMouse ListenerMouse { get; set; }
        public BaseUI()
        {
            Childs = new List<BaseUI>();
            ListenerMouse = new ListenerMouse(this);
        }
        public void AddComponent(BaseUI ui)
        {
            ui.Parent = this;
       //     ui.UIRenderer = this.UIRenderer;
            this.Childs.Add(ui);
        }
        public void SetLocalPosition(float x, float y)
        {
            if (Parent == null)
            {
                X = x;
                Y = y;
            } else {
                X = Parent.X + x;
                Y = Parent.Y + y;
            }
        }
        public abstract void Draw();
        public abstract void Dispose();
    }
}