﻿using Rota.Custom_Pins;
using System.Collections.ObjectModel;
using TK.CustomMap;
using TK.CustomMap.Api;
using TK.CustomMap.Api.Google;
using TK.CustomMap.Overlays;
using Xamarin.Forms;

namespace Rota.ViewModels
{
    public class AddRouteViewModel
    {
         IPlaceResult _fromPlace, _toPlace;
         Position _from, _to;

        public ObservableCollection<TKCustomMapPin> Pins { get;  set; }
        public ObservableCollection<TKRoute> Routes { get;  set; }
        public MapSpan Bounds { get;  set; }

        public Command<IPlaceResult> FromSelectedCommand
        {
            get
            {
                return new Command<IPlaceResult>(async (p) => 
                {
                    if(Device.OS == TargetPlatform.iOS)
                    {
                        TKNativeiOSPlaceResult placeResult = (TKNativeiOSPlaceResult)p;
                        _fromPlace = placeResult;
                        _from = placeResult.Details.Coordinate;
                    }
                    else
                    {
                        GmsPlacePrediction placeResult = (GmsPlacePrediction)p;
                        _fromPlace = placeResult;
                        var details = await TKNativePlacesApi.Instance.GetDetails(placeResult.PlaceId);

                        _from = details.Coordinate;
                    }
                });
            }
        }
        public Command<IPlaceResult> ToSelectedCommand
        {
            get
            {
                return new Command<IPlaceResult>(async (p) => 
                {
                    if(Device.OS == TargetPlatform.iOS)
                    {
                        TKNativeiOSPlaceResult placeResult = (TKNativeiOSPlaceResult)p;
                        _toPlace = placeResult;
                        _to = placeResult.Details.Coordinate;
                    }
                    else
                    {
                        GmsPlacePrediction placeResult = (GmsPlacePrediction)p;
                        _toPlace = placeResult;
                        var details = await TKNativePlacesApi.Instance.GetDetails(placeResult.PlaceId);

                        _to = details.Coordinate;
                    }
                });
            }
        }

        public Command AddRouteCommand
        {
            get
            {
                return new Command(() => 
                {
                    if (_toPlace == null || _fromPlace == null) return;

                    var route = new TKRoute
                    {
                        TravelMode = TKRouteTravelMode.Driving,
                        Source = _from,
                        Destination = _to,
                        Color = Color.FromHex("#11988D"),
                        LineWidth= 10.0f
                    };

                    Pins.Add(new RoutePin 
                    {
                        Route = route,
                        IsSource = true,
                        IsDraggable = true,
                        Position = _from,
                        Title = _fromPlace.Description,
                        ShowCallout = true,
                        DefaultPinColor = Color.Green
                    });
                    Pins.Add(new RoutePin
                    {
                        Route = route,
                        IsSource = false,
                        IsDraggable = true,
                        Position = _to,
                        Title = _toPlace.Description,
                        ShowCallout = true,
                        DefaultPinColor = Color.Red
                    });

                    Routes.Add(route);

                    Application.Current.MainPage.Navigation.PopAsync();
                });
            }
        }

        public AddRouteViewModel(ObservableCollection<TKRoute> routes, ObservableCollection<TKCustomMapPin> pins, MapSpan bounds)
        {
            Routes = routes;
            Pins   = pins;
            Bounds = bounds;
        }
    }
}
