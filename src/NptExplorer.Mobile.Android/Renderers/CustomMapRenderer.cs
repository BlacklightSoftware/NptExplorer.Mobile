using System.Collections.Generic;
using System.Linq;
using Android.Content;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using Android.Widget;
using NptExplorer.Core.Enums;
using NptExplorer.Mobile.Controls;
using NptExplorer.Mobile.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Maps.Android;

[assembly: ExportRenderer(typeof(CustomMap), typeof(CustomMapRenderer))]
namespace NptExplorer.Mobile.Droid.Renderers
{
	public class CustomMapRenderer : MapRenderer, GoogleMap.IInfoWindowAdapter
	{
		private IList<Pin> _customPins;

		public CustomMapRenderer(Context context) : base(context)
		{
		}

		protected override void OnElementChanged(Xamarin.Forms.Platform.Android.ElementChangedEventArgs<Map> e)
		{
			base.OnElementChanged(e);

			if (e.OldElement != null)
			{
				NativeMap.InfoWindowClick -= OnInfoWindowClick;
			}

			if (e.NewElement != null)
			{
				var formsMap = (CustomMap)e.NewElement;
				_customPins = formsMap.Pins;
			}
		}

		protected override void OnMapReady(GoogleMap map)
		{
			base.OnMapReady(map);

			NativeMap.InfoWindowClick += OnInfoWindowClick;
			NativeMap.SetInfoWindowAdapter(this);
		}

		protected override MarkerOptions CreateMarker(Pin pin)
		{
			var marker = new MarkerOptions();
			marker.SetPosition(new LatLng(pin.Position.Latitude, pin.Position.Longitude));
			marker.SetTitle(pin.Label);
			marker.SetSnippet(pin.Address);

			var customPin = (CustomPin)(Element as CustomMap).Pins.FirstOrDefault(p => p.Position == pin.Position);
			if (customPin != null)
			{
				switch (customPin.IconType)
				{
					case BadgeTypes.Wellbeing:
						marker.SetIcon(BitmapDescriptorFactory.FromResource(Resource.Drawable.wellbeing));
						break;
					case BadgeTypes.Nature:
						marker.SetIcon(BitmapDescriptorFactory.FromResource(Resource.Drawable.nature));
						break;
					case BadgeTypes.Heritage:
						marker.SetIcon(BitmapDescriptorFactory.FromResource(Resource.Drawable.heritage));
						break;
					default:
						marker.SetIcon(BitmapDescriptorFactory.FromResource(Resource.Drawable.anonymous));
						break;
				}
			}

			return marker;
		}

		private void OnInfoWindowClick(object sender, GoogleMap.InfoWindowClickEventArgs e)
		{
			var customPin = GetCustomPin(e.Marker);
			if (customPin == null)
			{
				return;
			}
		}

		public global::Android.Views.View GetInfoContents(Marker marker)
		{
			if (Android.App.Application.Context.GetSystemService(Context.LayoutInflaterService) is global::Android.Views.LayoutInflater inflater)
			{
				global::Android.Views.View view;

				var customPin = GetCustomPin(marker);
				if (customPin == null)
				{
					return null;
				}

				view = inflater.Inflate(Resource.Layout.MapInfoWindow, null);

				var infoTitle = view.FindViewById<TextView>(Resource.Id.InfoWindowTitle);
				var infoSubtitle = view.FindViewById<TextView>(Resource.Id.InfoWindowSubtitle);
				var badgeImage = view.FindViewById<ImageView>(Resource.Id.BadgeImage);

				if (infoTitle != null)
				{
					infoTitle.Text = customPin.Name;
				}
				if (infoSubtitle != null)
				{
					infoSubtitle.Text = customPin.BadgeLabel;
				}

				if (badgeImage != null)
				{
					switch (customPin.IconType)
					{
						case BadgeTypes.Wellbeing:
							badgeImage.SetImageResource(Resource.Drawable.wellbeing);
							break;
						case BadgeTypes.Nature:
							badgeImage.SetImageResource(Resource.Drawable.nature);
							break;
						case BadgeTypes.Heritage:
							badgeImage.SetImageResource(Resource.Drawable.heritage);
							break;
						default:
							badgeImage.SetImageResource(Resource.Drawable.anonymous);
							break;
					}
				}

				return view;
			}
			return null;
		}

		public global::Android.Views.View GetInfoWindow(Marker marker)
		{
			return null;
		}

		private CustomPin GetCustomPin(Marker annotation)
		{
			var position = new Position(annotation.Position.Latitude, annotation.Position.Longitude);
			foreach (var pin in _customPins)
			{
				if (pin.Position == position)
				{
					return (CustomPin)pin;
				}
			}
			return null;
		}
	}
}

