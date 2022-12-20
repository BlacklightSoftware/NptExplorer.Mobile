using NptExplorer.Mobile.ViewModels;
using Xamarin.Forms.Xaml;

namespace NptExplorer.Mobile.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AboutView : PageBase<AboutViewModel>
	{
		public AboutView()
		{
			InitializeComponent();
		}
	}
}