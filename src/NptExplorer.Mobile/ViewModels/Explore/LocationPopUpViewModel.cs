using NptExplorer.Mobile.Models;
using NptExplorer.Mobile.Services.Concrete;
using System.Windows.Input;
using NptExplorer.Mobile.ViewModels.Base;
using NptExplorer.Mobile.Factory;
using Xamarin.Forms;
using System.Threading.Tasks;

namespace NptExplorer.Mobile.ViewModels.Explore
{

	public class LocationPopUpViewModel : ExtendedBindableObject
	{
		private ExploreLocation _location;

		public ExploreLocation Location
		{
			get => _location;
			set
			{
				_location = value;
				RaisePropertyChanged(() => Location);
			}
		}

		public void Init(ExploreLocation loc)
		{
			if (loc == null)
			{
				return;
			}

			Location = loc;
		}
	}
}