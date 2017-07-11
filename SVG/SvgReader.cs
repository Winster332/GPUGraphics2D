using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace GPUGraphics2D.SvgReader
{
    public class SvgReader
    {
        private static List<IDraw> graphs = new List<IDraw>();
        public static List<IDraw> LoadGraphElements(string svgFileName)
        {
            graphs.Clear();
            XDocument svgDoc = XDocument.Load(svgFileName);
            XNamespace rootNamespace = svgDoc.Root.Name.NamespaceName;
            Console.WriteLine(rootNamespace + "g");   
            var layers = svgDoc.Root.Elements(rootNamespace + "g");

            foreach (var layer in layers)
            {
                foreach (var graphElement in layer.Elements())
                {
                    IDraw drawElement = DrawFactory.GetDrawElement(graphElement);
                    drawElement.LayerId = layer.Attribute("id").Value;
                    graphs.Add(drawElement);
                }                
            }

            foreach (var item in graphs)
            {
                Console.WriteLine(item);
            }

            return graphs;
        }

        public static void GenerateCodeFile()
        {

        }
    }
}

