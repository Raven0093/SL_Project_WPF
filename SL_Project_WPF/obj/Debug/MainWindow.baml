   M S B A M L   `   `   `  ����I  ESL_Project_WPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null/clr-namespace:SL_Project_WPFSL_Project_WPF  5   	   d `Microsoft.Maps.MapControl.WPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35tRclr-namespace:Microsoft.Maps.MapControl.WPF;assembly=Microsoft.Maps.MapControl.WPFMicrosoft.Maps.MapControl.WPF 5   	   O Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem 5   	       SL_Project_WPF.MainWindow   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   .localclr-namespace:SL_Project_WPF     5   	   cbmapRclr-namespace:Microsoft.Maps.MapControl.WPF;assembly=Microsoft.Maps.MapControl.WPF     5   	   6sys&clr-namespace:System;assembly=mscorlib     5   	   -   5          �Name$  
mainWindow��5   	    � Title$ 
FSX_Tracer��5   	   $	��800��6   $	��640��6)   .��5      �  � ColumnDefinitions 5   
   �� 5      $
��480* �6   �� 5      $
��180* �6   .+�5   
   � $��0��6    � RowDefinitions 5      �� 5      $
��550* �6    �� 5      $	��30* �6    �� 5      $	��30* �6    �� 5      $
��120* �6    .+�5      � $��0��6   .+�5      '  !Microsoft.Maps.MapControl.WPF.Map  -   5          Name$ myMap��5      +  %Microsoft.Maps.MapControl.WPF.MapCore   CredentialsProviderT  NMicrosoft.Maps.MapControl.WPF.Design.ApplicationIdCredentialsProviderConverter$F @AsfQQ9JwTbSTistpoxj5ahNtFut1osjKUEWDyv9KVuIpkqm9u5pzQEdwgUb3RSbJ 6*      Mode;  5Microsoft.Maps.MapControl.WPF.Design.MapModeConverter$ Aerial 6�   5      � 5"      $��1��6   .+�5#      ��  �� Orientation$ 
Horizontal=�6   .+�5$      �� $��Longitude [degrees]:��6   �� 5%      $��60��6h   ��6   �� *DataContext.UserLat+ �� ElementName$ 
mainWindow��	 �� Mode$	 TwoWay=�6h   �� 5&      $��Latitude [degrees]:��6   �� 5'      $��60��6h   ��6   �� *DataContext.UserLng+$ 
mainWindow��$	 TwoWay=�6h   �� 5(      $��
Set coords��6f   ��6   �� *DataContext.SetLatLonCommand+$ 
mainWindow��6}   5)      5*      � 5+      $��2��6   .+�5,      �� $ 
Horizontal=�6   .+�5-      �� $��Altitude [km]:��6   �� 5.      $��60��6m   ��6   �� *DataContext.UserAltitude+$ 
mainWindow��$	 TwoWay=�6m   �� 5/      $��Set altitude��6h   ��6   �� * DataContext.SetAltitudeCommand+$ 
mainWindow��6�   50      51      � 53      $��3��6    55      �� 56      $��* �6'   �� 57      $��* �6'   �� 58      $��* �6'   .+�5:      � $��0��6   .+�5;      �� $ Vertical=�6!   .+�5<      �� $��Center map on plane��6n   $��5q�6�   ��6!   �� *" DataContext.AutoMapCenterCommand+$ 
mainWindow����5=      ��5>   "   True��65   5@      �� 5B      $��Reset zoom settings��6m   $��5q�6�   ��6!   �� *!DataContext.ResetMapZoomCommand+$ 
mainWindow��6�   �� 5D      $��Simconnect: Connect��6t   $��5q�6(  ��6!   �� *(&DataContext.ConnectToSimconnectCommand+$ 
mainWindow��y�6�   �� *'%DataContext.SimconnectConnectionState+$ 
mainWindow�� (  $ConnectionStateToVisibilityConverter
 �� 	Converter#
 [  65  �� 5F      $��Simconnect: Disconnect��6y   $��5q�68  ��6!   �� *-+DataContext.DisconnectFromSimconnectCommand+$ 
mainWindow��y�6�   �� *'%DataContext.SimconnectConnectionState+$ 
mainWindow�� 0 ,InvertedConnectionStateToVisibilityConverter#
 [ 6E  5G      5H      � 5I      $��1��6   .+�5J      �� $ Vertical=�6!   .+�5L      �� $ ��Plane Longitude [degrees]:��6    �� 5M      ��6$   �� *DataContext.PlaneVM.Longitude+$ 
mainWindow�� �� StringFormat$ N5��$	 OneWay=��� 5O      $��Plane Latitude [degrees]:��6    �� 5P      ��6$   �� *DataContext.PlaneVM.Latitude+$ 
mainWindow��$ N5��$	 OneWay=��� 5R      $��Plane Altitude [km]:��6    �� 5S      ��6$   �� *DataContext.PlaneVM.Altitude+$ 
mainWindow��$ N5��$	 OneWay=�5T      5U      � 5V      $��2��6   .+�5W      �� $ Vertical=�6!   .+�5X      �� $��Plane Airspeed [knots]:��6    �� 5Y      ��6$   �� *DataContext.PlaneVM.Airspeed+$ 
mainWindow��$ N2��$	 OneWay=��� 5[      $#��Plane VerticalSpeed [ft/min]:��6    �� 5\      ��6$   �� *#!DataContext.PlaneVM.VerticalSpeed+$ 
mainWindow��$ N2��$	 OneWay=��� 5^      $��Plane GroundSpeed [m/s]:��6    �� 5_      ��6$   �� *!DataContext.PlaneVM.GroundSpeed+$ 
mainWindow��$ N2��$	 OneWay=�5a      5b      5d      5f      � 5g   
   $��1��6    5h      �� 5i      $��70 �6    �� 5j      $��* �6    �� 5k      $��20 �6    .+�5m      � $��0��6   .+�5n      �� $ Vertical=�6   .+�5o      �� $"��Max distance to navaids[km]:��6   �� 5p      $��5q�6�   ��6   �� *!DataContext.DistanceKmValueList+$ 
mainWindow�� �� SelectedItem 6o   �� *)'DataContext.SelectedDistanceKmValueList+$ 
mainWindow��$	 TwoWay=�6�   5q      5r      � 5t      $��1��6   .+�5u      � y�6   �� *DataContext.NavaidsSettings+$ 
mainWindow�� / +InvertedNavaidSettingsToVisibilityConverter#
 [  5v      �� 5w      $��30 �6(   �� 5x      $��* �6(   .+�5z      � $��0��6   .+�5{      �� $��Navaids:��6    5|      � 5}      $��1��6   .+�5~      �� .��5      �� $��Stretch=�6�   ��6(   �� *DataContext.NavaidsText+$ 
mainWindow��$ N2��$	 OneWay=�6�   5�      5�      5�      � 5�      y�6   �� *DataContext.NavaidsSettings+$ 
mainWindow�� ' #NavaidSettingsToVisibilityConverter#
 [ .+�5�      �� $ Vertical=�6!   .+�5�      �� ��6#   �� **(DataContext.NavaidSettings.StringVor_dme+$ 
mainWindow�� d� 	IsChecked 6x   �� *(&DataContext.NavaidSettings.BoolVor_dme+$ 
mainWindow��6�   �� 5�      ��6#   �� *&$DataContext.NavaidSettings.StringNdb+$ 
mainWindow�� 6t   �� *$"DataContext.NavaidSettings.BoolNdb+$ 
mainWindow��6�   �� 5�      ��6#   �� *&$DataContext.NavaidSettings.StringDme+$ 
mainWindow�� 6t   �� *$"DataContext.NavaidSettings.BoolDme+$ 
mainWindow��6�   �� 5�      ��6#   �� *&$DataContext.NavaidSettings.StringVor+$ 
mainWindow�� 6t   �� *$"DataContext.NavaidSettings.BoolVor+$ 
mainWindow��6�   �� 5�      ��6#   �� *)'DataContext.NavaidSettings.StringVortac+$ 
mainWindow�� 6w   �� *'%DataContext.NavaidSettings.BoolVortac+$ 
mainWindow��6�   �� 5�      ��6#   �� **(DataContext.NavaidSettings.StringNdb_dme+$ 
mainWindow�� 6x   �� *(&DataContext.NavaidSettings.BoolNdb_dme+$ 
mainWindow��6�   �� 5�      ��6#   �� *(&DataContext.NavaidSettings.StringTacan+$ 
mainWindow�� 6v   �� *&$DataContext.NavaidSettings.BoolTacan+$ 
mainWindow��6�   �� 5�      $��UpdateNavaidsFiles��6s   ��6!   �� *'%DataContext.UpdateNavaidsFilesCommand+$ 
mainWindow��6�   �� 5�      $ 
Horizontal=�6%   .+�5�      �� $��Navaids data:��6$   �� 5�      ��6(   �� *DataContext.NavaidsDataLoad+$ 
mainWindow��$ N2��$	 OneWay=�5�      5�      5�      5�      � 5�      $��2��6   .+�5�      �� $��Settings��6k   ��6   �� *'%DataContext.ChangeSettingsViewCommand+$ 
mainWindow��6�   5�      5�      5�      5�      