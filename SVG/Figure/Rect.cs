using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPUGraphics2D.SvgReader
{    
    public class Rect : IDraw
    {
        private int x = 0;
        private int y = 0;
        private int width = 0;
        private int height = 0;

        public Rect(System.Xml.Linq.XElement xElement)
        {
            x = Int32.Parse(xElement.Attribute("x").Value);
            y = Int32.Parse(xElement.Attribute("y").Value);
            width = Int32.Parse(xElement.Attribute("width").Value);
            height = Int32.Parse(xElement.Attribute("height").Value);
        }

        public override string ToString()
        {
            return String.Format("Rect x={0}, y={1}, width={2}, height={3}", 
                x, y, width, height);
        }
    }
}
