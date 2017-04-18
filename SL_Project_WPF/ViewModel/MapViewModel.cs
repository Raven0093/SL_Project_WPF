using Microsoft.Maps.MapControl.WPF;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;

namespace SL_Project_WPF
{
    public class MapViewModel : INotifyPropertyChanged
    {

        #region Events

        public event Action<Location> MouseDoubleClickEvent;
        public event Action<Location> MouseDownEvent;
        public event Action<int, Location> MouseWheelEvent;

        #endregion

        #region Privates Fields

        private Map _mapV;
        private List<Location> _pointsList;
        private List<Pushpin> _pushpinList;
        private object _centerLock;
        private int _zoom;
        private int _minZoom;
        private int _maxZoom;

        #endregion

        #region Properties

        public Location Center
        {
            get
            {
                return _mapV.Center;
            }
            set
            {
                Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action(() =>
                {
                    _mapV.Center = value;
                    OnPropertyChanged(nameof(Center));
                }));
            }

        }

        public int Zoom
        {
            get
            {
                return _zoom;
            }
            set
            {
                _zoom = value;
                Debug.WriteLine(_zoom);

                Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action(() =>
                {
                    _mapV.ZoomLevel = value;
                    OnPropertyChanged(nameof(Zoom));
                }));
            }
        }

        #endregion

        #region Constructors

        public MapViewModel(Map mapV)
        {
            _mapV = mapV;
            _pointsList = new List<Location>();
            SubscribeEvents();
            _centerLock = new object();
            _pushpinList = new List<Pushpin>();
        }

        #endregion

        #region Private Methods
        private void AddPolygon(MapPolygon polygon)
        {
            if (_mapV.Children != null)
            {
                try
                {
                    Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action(() =>
                    {
                        polygon.Fill = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.Blue);
                        polygon.Stroke = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.Green);
                        polygon.StrokeThickness = 5;
                        polygon.Opacity = 0.7;

                        _mapV.Children.Add(polygon);
                    }));
                }
                catch (Exception e)
                {

                }
            }
        }

        private void UnsubscribeEvents()
        {
            this._mapV.PreviewMouseDoubleClick -= MouseDoubleClickHandler;
            this._mapV.MouseDown -= MouseDownHandler;
            this._mapV.PreviewMouseWheel -= MouseWheelHendler;
        }

        private void SubscribeEvents()
        {
            this._mapV.PreviewMouseDoubleClick += MouseDoubleClickHandler;
            this._mapV.MouseDown += MouseDownHandler;
            this._mapV.PreviewMouseWheel += MouseWheelHendler;
        }

        private void MouseDownHandler(object sender, MouseEventArgs e)
        {
            e.Handled = true;
            Point mousePosition = e.GetPosition(_mapV);
            Location polygonPointLocation = _mapV.ViewportPointToLocation(mousePosition);
            MouseDownEvent?.Invoke(polygonPointLocation);
        }

        private void MouseWheelHendler(object sender, MouseWheelEventArgs e)
        {
            e.Handled = true;
            Point mousePosition = e.GetPosition(_mapV);
            Location polygonPointLocation = _mapV.ViewportPointToLocation(mousePosition);
            MouseWheelEvent?.Invoke(e.Delta, polygonPointLocation);
        }

        private void MouseDoubleClickHandler(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
            Point mousePosition = e.GetPosition(_mapV);
            Location polygonPointLocation = _mapV.ViewportPointToLocation(mousePosition);
            MouseDoubleClickEvent?.Invoke(polygonPointLocation);

        }

        #endregion

        #region Public Methods

        public void AddLocationToPolygon(Location point)
        {
            Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action(() =>
            {
                if (_pointsList.Any())
                {
                    MapPolygon polygon = new MapPolygon();
                    polygon.Locations = new LocationCollection() {
                    _pointsList.Last(),
                    point
                };

                    AddPolygon(polygon);
                }
                else
                {
                    MapPolygon polygon = new MapPolygon();
                    polygon.Locations = new LocationCollection() {
                   point,
                   point
                };

                    AddPolygon(polygon);
                }
            }));

            _pointsList.Add(point);
        }

        public void RemovePushPins()
        {
            Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action(() =>
            {

                if (_mapV?.Children != null)
                {

                    if (_pushpinList != null)
                    {

                        foreach (var pushpin in _pushpinList)
                        {
                            _mapV.Children.Remove(pushpin);
                        }
                    }

                    _pushpinList.Clear();
                }
            }));
        }

        public void RemoveMarkers()
        {
            _pushpinList.Clear();
            _pointsList.Clear();
            if (_mapV?.Children != null)
            {
                this._mapV.Children.Clear();
            }
        }

        public void AddPushPin(Location point, string text)
        {
            Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action(() =>
            {
                Pushpin pin = new Pushpin();
                pin.PositionOrigin = PositionOrigin.BottomLeft;
                pin.Location = point;
                pin.Content = text;
                _mapV.Children.Add(pin);
                _pushpinList.Add(pin);
            }));
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
