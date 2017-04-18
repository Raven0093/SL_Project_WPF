using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace NavaidsDataLib
{
    public class NavaidDataXmlSerializer
    {
        public static void Serialize(NavaidsData data, string fileName)
        {
            if (data == null)
            {
                return;
            }

            XmlSerializer serializer1 = new XmlSerializer(typeof(NavaidsData));

            using (FileStream fileStream = new FileStream(fileName, FileMode.Create))
            {
                serializer1.Serialize(fileStream, data);
            }
        }


        public static NavaidsData Deserialize(string fileName)
        {
            NavaidsData result = null;

            XmlSerializer serializer1 = new XmlSerializer(typeof(NavaidsData));

            using (FileStream fileStream = new FileStream(fileName, FileMode.Open))
            {
                result = (NavaidsData)serializer1.Deserialize(fileStream);
            }

            return result;

        }
    }
}
