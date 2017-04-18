using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace NavaidsDataLib
{
    [XmlType("NavaidsData")]
    public class NavaidsData
    {
        [XmlArray("Navaids")]
        [XmlArrayItem("Navaid")]
        public List<Navaid> Navaids { get; set; }

        public NavaidsData()
        {
            Navaids = new List<Navaid>();
        }
    }
}
