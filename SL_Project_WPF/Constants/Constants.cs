using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SL_Project_WPF
{
    public static class Constants
    {
        public const int MaxZoom = 20;
        public const int MinZoom = 1;
        public const int ZoomStep = 1;
        public const int StartZoom = 1;
        public const int MaxChartXValue = 100;
        public static readonly List<int> MaxDistanceKmList = new List<int>() { 100, 200, 300, 400, 500, 750, 1000, 2000 };
        public const int UpdateNavaidsDelay = 1000;

    }
}
