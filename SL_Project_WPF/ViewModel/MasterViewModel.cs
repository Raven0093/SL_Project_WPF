using Microsoft.Maps.MapControl.WPF;
using NavaidsDataLib;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SL_Project_WPF
{
    //PAN JANEK!
    class MasterViewModel : INotifyPropertyChanged
    {
        #region Private Fileds

        //Models
        private MapViewModel _mapVM;
        private PlaneViewModel _planeVM;

        //SimConnect
        private FSXConnectionManager.CONNECTION_STATE _simconnectConnectionState;

        //Navaids data
        private NavaidsData _navaidsData;
        private bool _navaidsDataLoaded;
        private int _selectedDistanceKmValueList;
        private string _navaidsText;
        private object _navaidsDataLock;
        private object _navaidsDataLoadedLock;
        private object _selectedDistanceKmValueLock;
        private string _navaidsDataLoad;

        //Navaids setttings on view
        private bool _navaidsSettings;
        private NavaidSettings _navaidSettings;
        private object _navaidSettingsLock;

        private CancellationTokenSource _updateNavaidsCancelTokenSource;

        //Plane data
        private Location _planeLocation;
        private object _planeLocationLock;

        //Map data
        private int _zoom;
        private int _minZoom;
        private int _maxZoom;
        private bool _nextMouseButtonDownOff;
        private bool _autoMapCenter;

        //User data
        private double _userLat;
        private double _userLng;
        private double _userAltitude;
        #endregion

        #region Constructors

        public MasterViewModel(Map mapV)
        {

            //Models
            _planeVM = new PlaneViewModel();
            _mapVM = new MapViewModel(mapV);

            //Simconnect
            _simconnectConnectionState = FSXConnectionManager.CONNECTION_STATE.Disconnected;
            ConnectionManager.DataReceived += ProcessSimConnectMessage;
            ConnectionManager.ChangeConnectionState += ChangeSimconnectConnection;

            //Navaids data
            _navaidsDataLock = new object();
            _navaidsDataLoaded = false;
            _navaidsDataLoadedLock = new object();
            _navaidSettingsLock = new object();
            _navaidSettings = new NavaidSettings() { BoolNdb = true, BoolVor = true };
            _navaidsSettings = false;
            _selectedDistanceKmValueLock = new object();
            DistanceKmValueList = new ObservableCollection<int>(Constants.MaxDistanceKmList);
            SelectedDistanceKmValueList = DistanceKmValueList.FirstOrDefault();

            //Map data
            _zoom = Constants.StartZoom;
            _maxZoom = Constants.MaxZoom;
            _minZoom = Constants.MinZoom;
            _mapVM.Zoom = Constants.StartZoom;
            _autoMapCenter = true;
            _nextMouseButtonDownOff = false;
            _mapVM.MouseDoubleClickEvent += MouseDoubleClick;
            _mapVM.MouseWheelEvent += MouseWheelEvent;
            _mapVM.MouseDownEvent += MouseDown;

            //Plane data

            _planeLocationLock = new object();


            ResetMapZoom();
            ConnectToSimconnect();
            LoadNavaidsDataTask();

        }

        #endregion

        #region Properties

        public PlaneViewModel PlaneVM
        {
            get
            {
                return _planeVM;
            }
        }

        public MapViewModel MapVM
        {
            get
            {
                return _mapVM;
            }
            set
            {
                _mapVM = value;
                OnPropertyChanged(nameof(MapVM));
            }
        }

        public FSXConnectionManager ConnectionManager
        {
            get
            {
                return FSXConnectionManager.Instance;
            }
        }

        public NavaidSettings NavaidSettings
        {
            get
            {
                lock (_navaidSettingsLock)
                {
                    return _navaidSettings;
                }
            }
            set
            {
                lock (_navaidSettingsLock)
                {
                    _navaidSettings = value;
                }
                OnPropertyChanged(nameof(NavaidSettings));
            }
        }

        public ObservableCollection<int> DistanceKmValueList { get; set; }

        public bool NavaidsSettings
        {
            get
            {
                return _navaidsSettings;
            }
            set
            {
                _navaidsSettings = value;
                OnPropertyChanged(nameof(NavaidsSettings));
            }
        }

        public int SelectedDistanceKmValueList
        {
            get
            {
                lock (_selectedDistanceKmValueLock)
                {
                    return _selectedDistanceKmValueList;
                }

            }
            set
            {
                lock (_selectedDistanceKmValueLock)
                {
                    _selectedDistanceKmValueList = value;
                }
                OnPropertyChanged(nameof(SelectedDistanceKmValueList));

            }
        }

        public string NavaidsText
        {
            get
            {
                return _navaidsText;
            }
            set
            {
                _navaidsText = value;
                OnPropertyChanged(nameof(NavaidsText));

            }
        }

        public string UserAltitude
        {
            get
            {
                return _userAltitude.ToString();
            }
            set
            {
                Regex regex = new Regex(@"^(\d+)([.,](\d+))?$");
                if (regex.IsMatch(value) && !string.IsNullOrEmpty(value))
                {
                    _userAltitude = double.Parse(value.Replace(',', '.'), CultureInfo.InvariantCulture);
                    OnPropertyChanged(nameof(UserAltitude));
                }
            }
        }

        public string UserLat
        {
            get
            {
                return _userLat.ToString();
            }
            set
            {
                Regex regex = new Regex(@"^(\d+)([.,](\d+))?$");
                if (regex.IsMatch(value) && !string.IsNullOrEmpty(value))
                {
                    _userLat = double.Parse(value.Replace(',', '.'), CultureInfo.InvariantCulture);
                    OnPropertyChanged(nameof(UserLat));
                }
            }
        }

        public string UserLng
        {
            get
            {
                return _userLng.ToString();
            }
            set
            {
                Regex regex = new Regex(@"^(\d+)([.,](\d+))?$");
                if (regex.IsMatch(value) && !string.IsNullOrEmpty(value))
                {
                    _userLng = double.Parse(value.Replace(',', '.'), CultureInfo.InvariantCulture);
                    OnPropertyChanged(nameof(UserLng));
                }
            }
        }

        public bool AutoMapCenter
        {
            get
            {
                return _autoMapCenter;
            }
            set
            {
                _autoMapCenter = value;
            }
        }

        public FSXConnectionManager.CONNECTION_STATE SimconnectConnectionState
        {
            get
            {
                return _simconnectConnectionState;
            }
            set
            {
                _simconnectConnectionState = value;
                OnPropertyChanged(nameof(SimconnectConnectionState));
            }

        }

        public string NavaidsDataLoad
        {
            get
            {
                return _navaidsDataLoad;
            }
            set
            {
                _navaidsDataLoad = value;
                OnPropertyChanged(nameof(NavaidsDataLoad));
            }

        }

        #endregion

        #region ICommands

        public ICommand ResetMapZoomCommand
        {
            get { return new CommandImpl(ResetMapZoom); }
        }

        public ICommand ChangeSettingsViewCommand
        {
            get { return new CommandImpl(ChangeSettingsView); }
        }

        public ICommand AutoMapCenterCommand
        {
            get { return new CommandImpl<bool>(SetAutoMapCenter); }
        }

        public ICommand ConnectToSimconnectCommand
        {
            get { return new CommandImpl(ConnectToSimconnect); }
        }

        public ICommand DisconnectFromSimconnectCommand
        {
            get { return new CommandImpl(DisconnectFromSimconnect); }
        }

        public ICommand SetLatLonCommand
        {
            get { return new CommandImpl(SetLatLon); }
        }

        public ICommand SetAltitudeCommand
        {
            get { return new CommandImpl(SetAltitude); }
        }

        public ICommand UpdateNavaidsFilesCommand
        {
            get { return new CommandImpl(UpdateNavaidsFiles); }
        }

        #endregion

        #region Private Methods

        //Naviads
        private void StartUpdatingNavaids()
        {
            _updateNavaidsCancelTokenSource = new CancellationTokenSource();
            Task.Factory.StartNew(() =>
            {
                UpdateNavaids(_updateNavaidsCancelTokenSource.Token);
            }, _updateNavaidsCancelTokenSource.Token);
        }

        private void StopUpdatingNavaids()
        {
            _updateNavaidsCancelTokenSource?.Cancel();
        }

        private void UpdateNavaidsFiles()
        {
            Task.Factory.StartNew(() =>
            {
                StopUpdatingNavaids();
                NavaidsDataLoad = "Downloading";
                NavaidsDataHelper.UpdateDataFiles();
                LoadNavaidsDataTask();
                StartUpdatingNavaids();
            });
        }

        private void UpdateNavaids(CancellationToken ct)
        {

            while (true)
            {
                if (ct.IsCancellationRequested)
                {
                    break;
                }

                lock (_navaidsDataLoadedLock)
                {
                    if (!_navaidsDataLoaded)
                    {
                        Task.Delay(Constants.UpdateNavaidsDelay).GetAwaiter().GetResult();
                        continue;
                    }
                }

                Location planePoint;
                lock (_planeLocationLock)
                {
                    if (_planeLocation == null)
                    {
                        Task.Delay(Constants.UpdateNavaidsDelay).GetAwaiter().GetResult();
                        continue;
                    }
                    planePoint = new Location(_planeLocation);
                }


                List<Distance> navaidsDistanceList = null;
                lock (_navaidsDataLock)
                {
                    double maxDistanceInM = SelectedDistanceKmValueList * 1000;
                    navaidsDistanceList = NavaidsDataHelper.GetDistanceList(_navaidsData.Navaids, planePoint.Latitude, planePoint.Longitude, maxDistanceInM, NavaidSettingsToArray());
                }

                _mapVM.RemovePushPins();
                NavaidsText = "";
                foreach (var navaidDistance in navaidsDistanceList)
                {
                    Location loc = new Location(navaidDistance.Navaid.Latitude, navaidDistance.Navaid.Longitude);

                    _mapVM.AddPushPin(loc, navaidDistance.Navaid.Id);
                    NavaidsText += navaidDistance.Navaid.ToString();
                    NavaidsText += Environment.NewLine;
                }
                Task.Delay(Constants.UpdateNavaidsDelay).GetAwaiter().GetResult();

            }
        }

        private void LoadNavaidsDataTask()
        {
            Task.Factory.StartNew(() =>
            {

                lock (_navaidsDataLoadedLock)
                {
                    NavaidsDataLoad = "Loading";
                    _navaidsDataLoaded = false;
                }
                NavaidsData navaidsData = NavaidsDataHelper.LoadData();
                lock (_navaidsDataLock)
                {
                    _navaidsData = navaidsData;
                }
                lock (_navaidsDataLoadedLock)
                {
                    NavaidsDataLoad = "Loaded";
                    _navaidsDataLoaded = true;
                }
            });
        }

        private NavaidTypesEnum[] NavaidSettingsToArray()
        {
            List<NavaidTypesEnum> result = new List<NavaidTypesEnum>();
            lock (_navaidSettingsLock)
            {
                if (NavaidSettings.BoolDme)
                {
                    result.Add(NavaidTypesEnum.DME);
                }
                if (NavaidSettings.BoolNdb)
                {
                    result.Add(NavaidTypesEnum.NDB);
                }
                if (NavaidSettings.BoolNdb_dme)
                {
                    result.Add(NavaidTypesEnum.NDB_DME);
                }
                if (NavaidSettings.BoolTacan)
                {
                    result.Add(NavaidTypesEnum.TACAN);
                }
                if (NavaidSettings.BoolVor)
                {
                    result.Add(NavaidTypesEnum.VOR);
                }
                if (NavaidSettings.BoolVortac)
                {
                    result.Add(NavaidTypesEnum.VORTAC);
                }
                if (NavaidSettings.BoolVor_dme)
                {
                    result.Add(NavaidTypesEnum.VOR_DME);
                }

            }
            return result.ToArray();


        }

        //Mouse
        private void MouseDown(Location point)
        {
            if (!_nextMouseButtonDownOff)
            {
                AutoMapCenter = false;

            }
            _nextMouseButtonDownOff = false;
        }

        private void MouseWheelEvent(int delta, Location point)
        {

            if (delta < 0)
            {
                _maxZoom -= Constants.ZoomStep;
                if (_maxZoom < Constants.MinZoom)
                {
                    _maxZoom = Constants.MinZoom;
                }

            }
            else
            {
                _maxZoom += Constants.ZoomStep;
                if (_maxZoom > Constants.MaxZoom)
                {
                    _maxZoom = Constants.MaxZoom;
                }
            }

            if (!_autoMapCenter)
            {
                if (delta < 0)
                {
                    _zoom -= Constants.ZoomStep;
                    if (_zoom < Constants.MinZoom)
                    {
                        _zoom = Constants.MinZoom;
                    }
                }
                else
                {
                    _zoom += Constants.ZoomStep;
                    if (_zoom > Constants.MaxZoom)
                    {
                        _zoom = Constants.MaxZoom;
                    }
                }
                _mapVM.Center = point;
                _mapVM.Zoom = _zoom;

            }
        }

        private void MouseDoubleClick(Location point)
        {
            _nextMouseButtonDownOff = true;
            SendLatLonToSimconnect(point.Latitude, point.Longitude);
            AutoMapCenter = true;

        }

        //Simconnect
        private void ConnectToSimconnect()
        {
            if (ConnectionManager.Connect())
            {
                StartUpdatingNavaids();
            }
        }

        private void DisconnectFromSimconnect()
        {
            StopUpdatingNavaids();
            ConnectionManager.Disconnect();
        }

        private void ProcessSimConnectMessage(SimConnectDataStruct dataStruct)
        {
            _planeVM.Update(dataStruct);

            Location planePoint = new Location(_planeVM.Latitude, _planeVM.Longitude);
            lock (_planeLocationLock)
            {
                _planeLocation = planePoint;
            }
            _mapVM.AddLocationToPolygon(planePoint);

            if (_autoMapCenter)
            {
                _mapVM.Center = planePoint;
                _zoom = (int)Math.Min(_maxZoom - Math.Ceiling(_planeVM.GroundSpeed / (_maxZoom - _minZoom)), _maxZoom);
                _mapVM.Zoom = _zoom;

            }
        }

        private void ChangeSimconnectConnection(FSXConnectionManager.CONNECTION_STATE state)
        {
            SimconnectConnectionState = state;
        }

        private void SendLatLonToSimconnect(double lat, double lng)
        {
            SimConnectDataStruct planeStructure = (SimConnectDataStruct)_planeVM;
            planeStructure.latitude = lat;
            planeStructure.longitude = lng;
            _mapVM.RemoveMarkers();
            FSXConnectionManager.Instance.SendDataToSimconnect(planeStructure);

        }

        private void SendAltitudeToSimconnect(double altitude)
        {
            SimConnectDataStruct planeStructure = (SimConnectDataStruct)_planeVM;
            planeStructure.altitude = altitude;
            FSXConnectionManager.Instance.SendDataToSimconnect(planeStructure);

        }

        //User interface
        private void SetAutoMapCenter(bool value)
        {
            AutoMapCenter = value;
        }

        private void SetLatLon()
        {
            SendLatLonToSimconnect(_userLat, _userLng);
            AutoMapCenter = true;
        }

        private void SetAltitude()
        {
            SendAltitudeToSimconnect(_userAltitude);
        }

        private void ResetMapZoom()
        {
            _maxZoom = Constants.MaxZoom;
            _minZoom = Constants.MinZoom;
        }

        private void ChangeSettingsView()
        {
            NavaidsSettings = !NavaidsSettings;
        }
        #endregion

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}