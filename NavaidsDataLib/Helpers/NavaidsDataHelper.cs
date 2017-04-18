using System;
using System.Collections.Generic;
using System.Device.Location;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace NavaidsDataLib
{
    public class NavaidsDataHelper
    {
        public static double GetDistance(double lat1, double lon1, double lat2, double lon2)
        {
            var sCoord = new GeoCoordinate(lat1, lon1);
            var eCoord = new GeoCoordinate(lat2, lon2);

            return sCoord.GetDistanceTo(eCoord);
        }

        public static List<Distance> GetDistanceList(List<Navaid> navaids, double lat, double lon, double maxDistanceInMeters, NavaidTypesEnum[] types)
        {
            List<Distance> result = new List<Distance>();
            if (navaids != null)
            {
                foreach (var navaid in navaids)
                {
                    double distance = GetDistance(lat, lon, navaid.Latitude, navaid.Longitude);
                    if (distance <= maxDistanceInMeters)
                    {

                        if (Array.FindIndex(types, x => x == navaid.Type) != -1)
                        {
                            result.Add(new Distance() { DistanceToNavaid = distance, Navaid = navaid });
                        }
                    }
                }
            }
            return result;
        }

        public static void UpdateDataFiles()
        {

            using (WebClient client = new WebClient())
            {
                client.DownloadFile(Constants.NavaidsCsvDataUrl,
                                    Constants.NavaidsCsvTmpFileName);
            }

            if (File.Exists(Constants.NavaidsXmlFileName))
            {
                File.Delete(Constants.NavaidsXmlFileName);
            }
            if (File.Exists(Constants.NavaidsCsvFileName))
            {
                File.Delete(Constants.NavaidsCsvFileName);
            }

            File.Move(Constants.NavaidsCsvTmpFileName, Constants.NavaidsCsvFileName);


        }

        public static NavaidsData LoadData()
        {
            NavaidsData result = null;

            if (File.Exists(Constants.NavaidsXmlFileName))
            {
                result = NavaidDataXmlSerializer.Deserialize(Constants.NavaidsXmlFileName);
            }
            else
            {
                if (File.Exists(Constants.NavaidsCsvFileName))
                {
                    result = NavaidsCsvParser.Parse(Constants.NavaidsCsvFileName);

                    NavaidDataXmlSerializer.Serialize(result, Constants.NavaidsXmlFileName);
                }
                else
                {
                    UpdateDataFiles();
                    result = LoadData();
                }
            }
            return result;
        }
    }
}
