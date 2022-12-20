using System.Collections.Generic;
using Xamarin.Forms.Maps;

namespace NptExplorer.Mobile.Controls
{
	public class CustomMap : Map
	{
		public CustomMap()
		{
			CustomPins = new List<CustomPin>();
		}

		public List<CustomPin> CustomPins { get; set; }
	}
}