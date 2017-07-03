using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPUGraphics2D.SvgReader
{
    public class Poligon : IDraw
    {
        struct Point
        {
            public int x;
            public int y;
        }

        private List<Point> Points = new List<Point>();

        public Poligon(System.Xml.Linq.XElement xElement)
        {
            string sPoints = xElement.Attribute("points").Value;
            string[] sPairs = sPoints.Split(' ');
            foreach (var pair in sPairs)
            {
                if (pair.Length == 0)
                    continue;

                string[] once = pair.Split(',');
                Point p;
                p.x = Int32.Parse(once[0]);
                p.y = Int32.Parse(once[1]);
                Points.Add(p);
            }
        }

        public override string ToString()
        {
            string ret = "Poligon";

            foreach (var point in Points)
            {
                ret += " (" + point.x.ToString() + ", " + point.y.ToString() + ")";
            }

            return ret;
        }
    }
}
