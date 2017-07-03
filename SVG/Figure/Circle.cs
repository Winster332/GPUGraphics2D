using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPUGraphics2D.SvgReader
{
    public class Circle : IDraw
    {
        private int cx = 0;
        private int cy = 0;
        private int r = 0;

        public Circle(System.Xml.Linq.XElement xElement)
        {
            cx = Int32.Parse(xElement.Attribute("cx").Value);
            cy = Int32.Parse(xElement.Attribute("cy").Value);
            r = Int32.Parse(xElement.Attribute("r").Value);
        }

        public override string ToString()
        {
            return String.Format("Circle x={0}, y={1}, r={2}", cx, cy, r);
        }
    }
}
