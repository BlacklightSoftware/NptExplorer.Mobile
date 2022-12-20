using NptExplorer.Core.Enums;
using NptExplorer.Mobile.Constants;
using Xamarin.Forms;

namespace NptExplorer.Mobile.Controls
{
	public class BadgeControl : ContentView
	{
		public static readonly BindableProperty BadgeTypeProperty = BindableProperty.Create(
			nameof(BadgeType), typeof(int), typeof(BadgeControl), propertyChanged: BadgeTypePropertyChanged, defaultBindingMode: BindingMode.TwoWay);
		public static readonly BindableProperty CollectedProperty = BindableProperty.Create(
			nameof(Collected), typeof(bool), typeof(BadgeControl), propertyChanged: CollectedPropertyChanged, defaultBindingMode: BindingMode.TwoWay);

		public int BadgeType
		{
			get => (int)GetValue(BadgeTypeProperty);
			set
			{
				SetValue(BadgeTypeProperty, value);
				Build();
			}
		}

		public bool Collected
		{
			get => (bool)GetValue(CollectedProperty);
			set
			{
				SetValue(CollectedProperty, value);
				Build();
			}
		}

		private static void BadgeTypePropertyChanged(BindableObject bindable, object oldValue, object newValue)
		{
			((BadgeControl)bindable).BadgeType = (int)newValue;
		}

		private static void CollectedPropertyChanged(BindableObject bindable, object oldValue, object newValue)
		{
			 ((BadgeControl)bindable).Collected = (bool)newValue;
		}

		private readonly Color _wellbeingColour = Xamarin.Essentials.ColorConverters.FromHex("#D13333");
		private readonly Color _natureColour = Xamarin.Essentials.ColorConverters.FromHex("#7FB03D");
		private readonly Color _heritageColour = Xamarin.Essentials.ColorConverters.FromHex("#7D561B");
		private readonly Color _trailColour = Xamarin.Essentials.ColorConverters.FromHex("#1CADC0");

		private readonly Frame _container = new() { Padding = 5, CornerRadius = 5, Margin = new Thickness(5,0,5,0) };
		private readonly StackLayout _stack = new() { Orientation = StackOrientation.Horizontal, VerticalOptions = LayoutOptions.Center };
		private readonly Image _image = new() { HorizontalOptions = LayoutOptions.Start };
		private readonly FontImageSource _fontImage = new() { FontFamily = "IconFont", Size = 20, Color = Color.White };
		private readonly Label _label = new()
		{
			VerticalTextAlignment = TextAlignment.Center,
			VerticalOptions = LayoutOptions.Center,
			TextColor = Color.White,
			FontSize = 14
		};

		public BadgeControl()
		{
			Build();
		}

		private void Build()
		{
			_label.Text = Collected ? "1/1" : "0/1";
			switch ((BadgeTypes)BadgeType)
			{
				case BadgeTypes.Wellbeing:
					_container.BackgroundColor = _wellbeingColour;
					_fontImage.Glyph = IconConstants.Spa;
					break;
				case BadgeTypes.Nature:
					_container.BackgroundColor = _natureColour;
					_fontImage.Glyph = IconConstants.Leaf;
					break;
				case BadgeTypes.Heritage:
					_container.BackgroundColor = _heritageColour;
					_fontImage.Glyph = IconConstants.Bank;
					break;
				case BadgeTypes.Trail:
					_container.BackgroundColor = _trailColour;
					_fontImage.Glyph = IconConstants.Road;
					break;
			}

			_image.Source = _fontImage;
			_stack.Children.Add(_image);
			_stack.Children.Add(_label);
			_container.Content = _stack;
			Content = _container;
		}
	}
}