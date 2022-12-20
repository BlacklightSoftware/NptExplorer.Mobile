using System;
using System.Globalization;
using System.Web;
using NptExplorer.Mobile.Constants;
using Xamarin.Forms;

namespace NptExplorer.Mobile.Converters
{
	public class ImageToUriConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			var imageName = value.ToString();
			var uri = string.IsNullOrEmpty(imageName) ? string.Empty : Uri.EscapeUriString($"{SystemConstants.BlobUri}/images/{imageName.ToLower()}");
			return uri;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}