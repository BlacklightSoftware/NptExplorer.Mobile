using NptExplorer.Core.Models;
using NptExplorer.Mobile.ViewModels.Base;

namespace NptExplorer.Mobile.ViewModels.Adventure
{

	public class PoiPopUpViewModel : ExtendedBindableObject
	{
		private PointOfInterest _pointOfInterest;

		public PointOfInterest PointOfInterest
		{
			get => _pointOfInterest;
			set
			{
				_pointOfInterest = value;
				RaisePropertyChanged(() => PointOfInterest);
			}
		}

		public void Init(PointOfInterest poi)
		{
			if (poi == null)
			{
				return;
			}

			PointOfInterest = poi;
		}
	}
}