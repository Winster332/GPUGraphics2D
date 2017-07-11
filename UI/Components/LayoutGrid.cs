using System;
using System.Drawing;
using System.Collections.Generic;

namespace GPUGraphics2D.UI.Components
{
    public class LayoutGrid : Layout
    {
        public new Layout[,] Childs { get; set; }
        private int CountX { get; set; }
        private int CountY { get; set; }
        public LayoutGrid(float x, float y, float w, float h, Color color)
        {
            this.X = x;
            this.Y = y;
            this.Width = w;
            this.Hieght = h;
            this.Background = new SolidBrush(color); 
        }
        public void InitGrid(int countWidth, int countHeight)
        {
            this.CountX = countWidth;
            this.CountY = countHeight;

            Childs = new Layout[countWidth, countHeight];


            for (int ix = 0; ix < countWidth; ix++)
                for (int iy = 0; iy < countHeight; iy++)
                {
                    Childs[ix, iy] = null;
                }
        }
        public void AddComponent(int x, int y, Layout component)
        {
            var stepWidth = this.Width / CountX;
            var stepHeight = this.Hieght / CountY;

            Childs[x, y] = component;
            Childs[x, y].X = stepWidth * x;
            Childs[x, y].Y = stepHeight * y;
            Childs[x, y].Width = this.Width / CountX;
            Childs[x, y].Hieght = this.Hieght / CountY;
            Childs[x, y].Parent = this;
        }
        public override void Draw()
        {
            UIRenderer.Layout(this);
            
            for (int ix = 0; ix < CountX; ix++)
                for (int iy = 0; iy < CountY; iy++)
                {
                    if (Childs[ix, iy] != null)
                        UIRenderer.Layout(Childs[ix, iy]);
                }
        }
        public void RemoveComponent(int x, int y)
        {
            this.Childs[x, y] = null;
        }
    }
}