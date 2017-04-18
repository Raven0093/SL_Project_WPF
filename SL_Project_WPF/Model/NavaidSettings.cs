using NavaidsDataLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SL_Project_WPF
{
    public class NavaidSettings
    {
        public bool BoolVor_dme { get; set; }
        public bool BoolNdb { get; set; }
        public bool BoolDme { get; set; }
        public bool BoolVor { get; set; }
        public bool BoolVortac { get; set; }
        public bool BoolNdb_dme { get; set; }
        public bool BoolTacan { get; set; }


        public string StringVor_dme { get { return EnumExtensions.GetEnumAttributeValue(NavaidTypesEnum.VOR_DME); } }
        public string StringNdb { get { return EnumExtensions.GetEnumAttributeValue(NavaidTypesEnum.NDB); } }
        public string StringDme { get { return EnumExtensions.GetEnumAttributeValue(NavaidTypesEnum.DME); } }
        public string StringVor { get { return EnumExtensions.GetEnumAttributeValue(NavaidTypesEnum.VOR); } }
        public string StringVortac { get { return EnumExtensions.GetEnumAttributeValue(NavaidTypesEnum.VORTAC); } }
        public string StringNdb_dme { get { return EnumExtensions.GetEnumAttributeValue(NavaidTypesEnum.NDB_DME); } }
        public string StringTacan { get { return EnumExtensions.GetEnumAttributeValue(NavaidTypesEnum.TACAN); } }
    }
}
