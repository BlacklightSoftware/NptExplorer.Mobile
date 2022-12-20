using System;
using System.Globalization;
using NptExplorer.Core.Enums;
using NptExplorer.Mobile.Resources;
using Xamarin.Forms;

namespace NptExplorer.Mobile.Converters
{
	public class DifficultyConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			var difficulty = (Difficulties)value;
			return difficulty switch
			{
				Difficulties.Easy => AppResources.Difficulty_Easy,
				Difficulties.SomeExertion => AppResources.Difficulty_SomeExertion,
				Difficulties.Strenuous => AppResources.Difficulty_Strenuous,
				_ => AppResources.Difficulty_Unknown
			};
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}