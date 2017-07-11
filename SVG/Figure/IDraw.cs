using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPUGraphics2D.SvgReader
{
    public class IDraw
    {
        public string LayerId { get; set; }

        public virtual void Draw(Graphics2D g)
        {
            Console.WriteLine(this);
        }

        public override string ToString()
        {
            return String.Format("LayerId={0}", LayerId);
        }

    }
}
