using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPUGraphics2D.SvgReader
{
    public class DrawFactory
    {
        public static IDraw GetDrawElement(System.Xml.Linq.XElement xElement)
        {
            try
            {
                switch(xElement.Name.LocalName)
                {
                    case "circle": return new Circle(xElement);
                    case "rect": return new Rect(xElement);
                    case "polygon": return new Poligon(xElement);
                    case "line": return new Line(xElement);
                    case "ellipse": return new Ellipse(xElement);
                    case "metadata": return new Metadata(xElement);
                }
            }
            catch (NullReferenceException)
            {
                Console.WriteLine("Error parse element: {0}", xElement);
                Console.WriteLine();
            }

            return new UnknownElement(xElement);
        }
    }
}
