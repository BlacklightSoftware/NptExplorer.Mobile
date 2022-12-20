using System;
using System.Globalization;
using NptExplorer.Core.Enums;
using NptExplorer.Mobile.Constants;
using Xamarin.Forms;

namespace NptExplorer.Mobile.Converters
{
	public class FacilityIconConvertor : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value == null)
			{
				return IconConstants.HelpCircle;
			}

			var facilities = (Facilities)value;
			return facilities switch
			{
				Facilities.CafeRestaurants => IconConstants.Coffee,
				Facilities.WheelchairAccess => IconConstants.WheelchairAccessibility,
				Facilities.DogFriendly => IconConstants.DogService,
				Facilities.Parking => IconConstants.Parking,
				Facilities.Playground => IconConstants.Paw,
				Facilities.Toilets => IconConstants.Toilet,
				_ => IconConstants.EmoticonConfused
			};
		}
		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}