using Xamarin.Forms;

namespace NptExplorer.Mobile.Views
{
	public class PageBase : ContentPage
	{
	}

	public class PageBase<TViewModel> : PageBase
	{
		public TViewModel ViewModel { get; set; }

		public PageBase()
		{
			BindingContext = ViewModel = (TViewModel)Startup.ServiceProvider.GetService(typeof(TViewModel));
		}
	}
}