using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace NavaidsDataLib
{
    [XmlType("Navaid")]
    public class Navaid
    {
        [XmlElement("Id")]
        public string Id { get; set; }
        [XmlElement("Name")]
        public string Name { get; set; }
        [XmlElement("Type")]
        public NavaidTypesEnum Type { get; set; }
        [XmlElement("Freq")]
        public string Freq { get; set; }
        [XmlElement("Latitude_Deg")]
        public double Latitude { get; set; }
        [XmlElement("Longitude_Deg")]
        public double Longitude { get; set; }
        [XmlElement("Elevation_Ft")]
        public double Elevation { get; set; }


        public override string ToString()
        {
            return string.Format("{0}{1}{2}{3}{4}{5}{6}",
                $"Id: {Id} {Environment.NewLine}",
                $"Name: {Name} {Environment.NewLine}",
                $"Type: {EnumExtensions.GetEnumAttributeValue(Type)} {Environment.NewLine}",
                $"Freq: {Freq} {Environment.NewLine}",
                $"Latitude_Deg: {Latitude} {Environment.NewLine}",
                $"Longitude_Deg: {Longitude} {Environment.NewLine}",
                $"Elevation_Ft: {Elevation} {Environment.NewLine}"
                );
        }


    }
}
