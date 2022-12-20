using System;
using System.Globalization;
using Xamarin.Forms;

namespace NptExplorer.Mobile.Converters
{
	public class RedGreenColorConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value == null)
			{
				return Color.Black;
			}

			return string.Equals((string)value, "True", StringComparison.Ordinal) ? Color.Green : Color.Red;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}