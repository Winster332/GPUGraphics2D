using System;
using System.Collections.Generic;

namespace GPUGraphics2D.UI.Events.Mouse
{
    public class ListenerMouse
    {
        private List<Action<MouseDate>> MouseDown { get; set; }
        private List<Action<MouseDate>> MouseMove { get; set; }
        private List<Action<MouseDate>> MouseUp { get; set; }
        private List<Action<MouseDate>> MouseLeave { get; set; }
        private List<Action<MouseDate>> MouseEnter { get; set; }
        private bool IsMouseOver;
        private Components.BaseUI UI { get; set; }
        public ListenerMouse(Components.BaseUI ui)
        {
            this.UI = ui;
            this.IsMouseOver = true;
            
            MouseDown = new List<Action<MouseDate>>();
            MouseMove = new List<Action<MouseDate>>();
            MouseUp = new List<Action<MouseDate>>();
            MouseLeave = new List<Action<MouseDate>>();
            MouseEnter = new List<Action<MouseDate>>();
        }
        public void AddDown(Action<MouseDate> action) => MouseDown.Add(action);
        public void AddMove(Action<MouseDate> action) => MouseMove.Add(action);
        public void AddUp(Action<MouseDate> action) => MouseUp.Add(action);
        public void AddLeave(Action<MouseDate> action) => MouseLeave.Add(action);
        public void AddEnter(Action<MouseDate> action) => MouseEnter.Add(action);

        public void RemoveDown(Action<MouseDate> action) => MouseDown.Remove(action);
        public void RemoveMove(Action<MouseDate> action) => MouseMove.Remove(action);
        public void RemoveUp(Action<MouseDate> action) => MouseUp.Remove(action);
        public void RemoveLeave(Action<MouseDate> action) => MouseLeave.Remove(action);
        public void RemoveEnter(Action<MouseDate> action) => MouseEnter.Remove(action);

        public void ActiveDown(float x, float y, Components.BaseUI ui) => MouseDown.ForEach(m => m(new MouseDate(x, y, ui)));
        public void ActiveMove(float x, float y, Components.BaseUI ui) => MouseMove.ForEach(m => m(new MouseDate(x, y, ui)));
        public void ActiveUp(float x, float y, Components.BaseUI ui) => MouseUp.ForEach(m => m(new MouseDate(x, y, ui)));
        public void ActiveLeave(float x, float y, Components.BaseUI ui) => MouseLeave.ForEach(m => m(new MouseDate(x, y, ui)));
        public void ActiveEnter(float x, float y, Components.BaseUI ui) => MouseEnter.ForEach(m => m(new MouseDate(x, y, ui)));

        public bool OnMouseDown(Components.BaseUI ui, float x, float y)
        {
            if (ui.Collision != null)
            {
                var isCollision = ui.Collision.IsCollisionMouse(ui, x, y);

                if (isCollision)
                {
                    var isChildCollision = false;
                    ui.Childs.ForEach(childUI =>
                    {
                        if (ui.ListenerMouse.OnMouseDown(childUI, x, y))
                            isChildCollision = true;
                    });
                    if (!isChildCollision)
                        ui.ListenerMouse.ActiveDown(x, y, ui);
                }

                return isCollision;
            } else return false;
        }
        public bool OnMouseMove(Components.BaseUI ui, float x, float y)
        {
            if (ui.Collision != null)
            {
                var isCollision = ui.Collision.IsCollisionMouse(ui, x, y);

                if (isCollision)
                {
                    var isChildCollision = false;
                    ui.Childs.ForEach(childUI =>
                    {
                        if (ui.ListenerMouse.OnMouseMove(childUI, x, y))
                            isChildCollision = true;
                    });
                    if (!isChildCollision)
                    {
                        if (ui.ListenerMouse.IsMouseOver) {
                            ui.ListenerMouse.ActiveEnter(x, y, ui);
                            ui.ListenerMouse.IsMouseOver = false;
                        }  
                        ui.ListenerMouse.ActiveMove(x, y, ui);
                    } 
                
                } else if (!ui.ListenerMouse.IsMouseOver)
                {
                    ui.ListenerMouse.IsMouseOver = true;
                    ui.ListenerMouse.ActiveLeave(x, y, ui);
                }
                return isCollision;
            } else return false;
        }
        private void LeaveAndEnter(bool b)
        {
        }
        public bool OnMouseUp(Components.BaseUI ui, float x, float y)
        {   
            if (ui.Collision != null)
            {
                var isCollision = ui.Collision.IsCollisionMouse(ui, x, y);

                if (isCollision)
                {
                    var isChildCollision = false;
                    ui.Childs.ForEach(childUI =>
                    {
                        if (ui.ListenerMouse.OnMouseUp(childUI, x, y))
                            isChildCollision = true;
                    });
                    if (!isChildCollision)
                        ui.ListenerMouse.ActiveUp(x, y, ui);
                }

                return isCollision;
            } else return false;
        }
    }
}