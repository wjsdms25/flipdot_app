using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Windows;


namespace APP
{
    class Launcher:MainWindow
    {
        public void SaveXml(string sw_str)
        {
            XDocument xdoc = new XDocument(new XDeclaration("1.0", "UTF-16", null));
            XElement xroot = new XElement("LIST");
            xdoc.Add(xroot);

            XElement xe1 = new XElement("Time",
                new XAttribute("Id", "1"),
                new XElement("kind", "time"),
                new XElement("Switch", sw_str)

            );

            xroot.Add(xe1);
            xdoc.Save(@"C:\Temp\K.xml");
        }

        //public void LoadXml()
    }
}
