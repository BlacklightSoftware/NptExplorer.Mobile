using NptExplorer.Mobile.ViewModels;
using Xamarin.Forms.Xaml;

namespace NptExplorer.Mobile.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class RatingView : PageBase<RatingViewModel>
	{
		public RatingView()
		{
			InitializeComponent();
		}
	}
}