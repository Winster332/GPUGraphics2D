using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPUGraphics2D.SvgReader
{
    public class UnknownElement : IDraw
    {
        private string name;

        public UnknownElement(System.Xml.Linq.XElement xElement = null)
        {
            if (xElement == null)
                return;
            
            name = xElement.Name.LocalName;
        }


        public override string ToString()
        {
            return String.Format("Unknown element {0}", name);
        }
    }
}
