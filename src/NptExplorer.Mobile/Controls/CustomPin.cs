using System;
using NptExplorer.Core.Enums;
using Xamarin.Forms.Maps;

namespace NptExplorer.Mobile.Controls
{
	public class CustomPin : Pin
	{
		public int ItemId { get; set; }
		public string Name { get; set; }
		public string BadgeLabel { get; set; }
		public BadgeTypes IconType { get; set; }

		public event Action<object> CustomInfoWindowClicked;

		public void InvokeInfoWindow()
		{
			CustomInfoWindowClicked(this);
		}
	}
}