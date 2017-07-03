using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPUGraphics2D.SvgReader
{
    public class Line : IDraw
    {
        public int x1 = 0;
        public int y1 = 0;
        public int x2 = 0;
        public int y2 = 0;

        public Line(System.Xml.Linq.XElement xElement)
        {
            x1 = Int32.Parse(xElement.Attribute("x1").Value);
            y1 = Int32.Parse(xElement.Attribute("y1").Value);
            x2 = Int32.Parse(xElement.Attribute("x2").Value);
            y2 = Int32.Parse(xElement.Attribute("y2").Value);
        }

        public override string ToString()
        {
            return String.Format("Line x1={0}, y1={1}, x2={2}, y2={3}", 
                x1, y1, x2, y2);
        }
    }
}
