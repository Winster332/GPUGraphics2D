using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPUGraphics2D.SvgReader
{
    public class Metadata : IDraw
    {
        private string id;

        public Metadata(System.Xml.Linq.XElement xElement)
        {
            id = xElement.Attribute("id").Value;
        }

        public override string ToString()
        {
            return String.Format("Metadata id={0}", id);
        }

    }
}
