using System.Collections.Generic;
using System.Linq;
using CoreGraphics;
using Foundation;
using MapKit;
using NptExplorer.Core.Enums;
using NptExplorer.Mobile.Controls;
using NptExplorer.Mobile.iOS.Renderers;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Maps.iOS;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(CustomMap), typeof(CustomMapRenderer))]
namespace NptExplorer.Mobile.iOS.Renderers
{
	public class CustomMapRenderer : MapRenderer
	{
		//private UIView _customPinView;
		private IList<Pin> _customPins;
		private UITapGestureRecognizer _tapGesture;

		protected override void OnElementChanged(ElementChangedEventArgs<View> e)
		{
			base.OnElementChanged(e);

			if (e.OldElement != null)
			{
				var nativeMap = Control as MKMapView;
				nativeMap.GetViewForAnnotation = null;
				nativeMap.CalloutAccessoryControlTapped -= OnCalloutAccessoryControlTapped;
				nativeMap.DidSelectAnnotationView -= OnDidSelectAnnotationView;
				nativeMap.DidDeselectAnnotationView -= OnDidDeselectAnnotationView;
			}

			if (e.NewElement != null)
			{
				var formsMap = (CustomMap)e.NewElement;
				var nativeMap = Control as MKMapView;
				_customPins = formsMap.Pins;

				nativeMap.GetViewForAnnotation = GetViewForAnnotation;
				nativeMap.CalloutAccessoryControlTapped += OnCalloutAccessoryControlTapped;
				nativeMap.DidSelectAnnotationView += OnDidSelectAnnotationView;
				nativeMap.DidDeselectAnnotationView += OnDidDeselectAnnotationView;
			}
		}

		protected override MKAnnotationView GetViewForAnnotation(MKMapView mapView, IMKAnnotation annotation)
		{
			MKAnnotationView annotationView = null;

			if (annotation is MKUserLocation)
			{
				return null;
			}

			var customPin = GetCustomPin(annotation as MKPointAnnotation);
			if (customPin == null)
			{
				return null;
			}

			UIImage image;

			switch (customPin.IconType)
			{
				case BadgeTypes.Wellbeing:
					image = UIImage.FromBundle("wellbeing", NSBundle.MainBundle, UITraitCollection.CurrentTraitCollection);
					break;
				case BadgeTypes.Nature:
					image = UIImage.FromBundle("nature", NSBundle.MainBundle, UITraitCollection.CurrentTraitCollection);
					break;
				case BadgeTypes.Heritage:
					image = UIImage.FromBundle("heritage", NSBundle.MainBundle, UITraitCollection.CurrentTraitCollection);
					break;
				case BadgeTypes.Trail:
				default:
					image = UIImage.FromBundle("anonymous", NSBundle.MainBundle, UITraitCollection.CurrentTraitCollection);
					break;
			}

			annotationView = mapView.DequeueReusableAnnotation(customPin.Name);
			if (annotationView == null)
			{
				annotationView = new CustomMkAnnotationView(annotation, customPin.Name)
				{
					Image = image,
					CalloutOffset = new CGPoint(0, 0),
					LeftCalloutAccessoryView = new UIImageView(image),
					
				};
				((CustomMkAnnotationView)annotationView).Name = customPin.Name;
				((CustomMkAnnotationView)annotationView).BadgeLabel = customPin.BadgeLabel;
			}
			annotationView.CanShowCallout = true;

			return annotationView;
		}

		private void OnCalloutAccessoryControlTapped(object sender, MKMapViewAccessoryTappedEventArgs e)
		{
			var customView = e.View as CustomMkAnnotationView;
		}

		private void OnDidSelectAnnotationView(object sender, MKAnnotationViewEventArgs e)
		{
			_tapGesture = new UITapGestureRecognizer(() =>
			{
				GetCustomPin((MKPointAnnotation)e.View.Annotation).InvokeInfoWindow();
			});
			e.View.AddGestureRecognizer(_tapGesture);
			//var customView = e.View as CustomMkAnnotationView;

			//_customPinView = new UIView
			//{
			//	Frame = new CGRect(0, 0, 200, 84)
			//};

			//var label = new UILabel()
			//{
			//	Text = customView.BadgeLabel
			//};
			//_customPinView.AddSubview(label);
			//_customPinView.Center = new CGPoint(0, -(e.View.Frame.Height - 20));
			//e.View.AddSubview(_customPinView);
		}

		private void OnDidDeselectAnnotationView(object sender, MKAnnotationViewEventArgs e)
		{
			e.View.RemoveGestureRecognizer(_tapGesture);
			_tapGesture = null;

			//if (!e.View.Selected)
			//{
				//_customPinView.RemoveFromSuperview();
				//_customPinView.Dispose();
				//_customPinView = null;
			//}
		}

		private CustomPin GetCustomPin(MKPointAnnotation annotation)
		{
			if (_customPins == null || annotation == null)
			{
				return null;
			}

			var position = new Position(annotation.Coordinate.Latitude, annotation.Coordinate.Longitude);
			return _customPins.Where(pin => pin.Position == position).Cast<CustomPin>().FirstOrDefault();
		}
	}
}