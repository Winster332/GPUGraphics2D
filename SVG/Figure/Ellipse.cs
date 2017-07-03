using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPUGraphics2D.SvgReader
{
    public class Ellipse : IDraw
    {
        private int cx = 0;
        private int cy = 0;
        private int rx = 0;
        private int ry = 0;

        public Ellipse(System.Xml.Linq.XElement xElement)
        {
            cx = Int32.Parse(xElement.Attribute("cx").Value);
            cy = Int32.Parse(xElement.Attribute("cy").Value);
            rx = Int32.Parse(xElement.Attribute("rx").Value);
            ry = Int32.Parse(xElement.Attribute("ry").Value);
        }

        public override string ToString()
        {
            return String.Format("Ellipse x={0}, y={1}, rx={2}, ry = {3}",
                cx, cy, rx, ry);
        }
    }
}
