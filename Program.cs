using System;
using System.Collections.Generic;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;

using Color = System.Drawing.Color;
using Pen = System.Drawing.Pen;
using SolidBrush = System.Drawing.SolidBrush;
using Point = System.Drawing.Point;

namespace GPUGraphics2D
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var app = new UI.Application((preApp) => 
            {
                preApp.FactoryScreen.SetScteen(new AppScreen());
            }))
            {
                app.Run(60);
            }
        }
    }
}
