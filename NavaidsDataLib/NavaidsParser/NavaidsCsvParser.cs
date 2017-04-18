using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NavaidsDataLib
{
    public class NavaidsCsvParser
    {
        class NavaidsCsvParserConstants
        {
            public const string IdInCsvHeader = "ident";
            public const string NameInCsvHeader = "name";
            public const string TypeInCsvHeader = "type";
            public const string FreqInCsvHeader = "frequency_khz";
            public const string LatitudeInCsvHeader = "latitude_deg";
            public const string LongitudeInCsvHeader = "longitude_deg";
            public const string ElevationInCsvHeader = "elevation_ft";

        }

        public static NavaidsData Parse(string fileName)
        {
            NavaidsData navaidsData = new NavaidsData();

            int idIndex = 0;
            int nameIndex = 0;
            int typeIndex = 0;
            int freqIndex = 0;
            int latitudeIndex = 0;
            int longitudeIndex = 0;
            int elevationIndex = 0;

            using (TextFieldParser parser = new TextFieldParser(fileName))
            {
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(",");

                if (!parser.EndOfData)
                {
                    string[] fields = parser.ReadFields();
                    for (int i = 0; i < fields.Length; i++)
                    {
                        if (!string.IsNullOrEmpty(fields[i]))
                        {
                            switch (fields[i])
                            {
                                case NavaidsCsvParserConstants.IdInCsvHeader:
                                    idIndex = i;
                                    break;
                                case NavaidsCsvParserConstants.NameInCsvHeader:
                                    nameIndex = i;
                                    break;
                                case NavaidsCsvParserConstants.TypeInCsvHeader:
                                    typeIndex = i;
                                    break;
                                case NavaidsCsvParserConstants.FreqInCsvHeader:
                                    freqIndex = i;
                                    break;
                                case NavaidsCsvParserConstants.LatitudeInCsvHeader:
                                    latitudeIndex = i;
                                    break;
                                case NavaidsCsvParserConstants.LongitudeInCsvHeader:
                                    longitudeIndex = i;
                                    break;
                                case NavaidsCsvParserConstants.ElevationInCsvHeader:
                                    elevationIndex = i;
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                }
                while (!parser.EndOfData)
                {
                    //Process row
                    string[] fields = parser.ReadFields();

                    var id = fields[idIndex];
                    var name = fields[nameIndex];
                    var typeString = fields[typeIndex];
                    var freq = fields[freqIndex];
                    var latitudeString = fields[latitudeIndex];
                    var longitudeString = fields[longitudeIndex];
                    var elevationString = fields[elevationIndex];

                    double latitude = 0;
                    double longitude = 0;
                    double elevation = 0;

                    NavaidTypesEnum type = EnumExtensions.GetTagKeyEnum<NavaidTypesEnum>(typeString);
                    if (type == NavaidTypesEnum.None)
                    {
                        Console.WriteLine(typeString);
                    }

                    if (!string.IsNullOrWhiteSpace(latitudeString))
                    {
                        latitude = double.Parse(latitudeString, System.Globalization.CultureInfo.InvariantCulture);
                    }
                    if (!string.IsNullOrWhiteSpace(longitudeString))
                    {
                        longitude = double.Parse(longitudeString, System.Globalization.CultureInfo.InvariantCulture);
                    }
                    if (!string.IsNullOrWhiteSpace(elevationString))
                    {
                        elevation = double.Parse(elevationString, System.Globalization.CultureInfo.InvariantCulture);
                    }


                    Navaid navaid = new Navaid()
                    {
                        Freq = freq,
                        Id = id,
                        Latitude = latitude,
                        Longitude = longitude,
                        Type = type,
                        Elevation = elevation,
                        Name = name
                    };

                    navaidsData.Navaids.Add(navaid);
                }
            }

            return navaidsData;
        }
    }
}
