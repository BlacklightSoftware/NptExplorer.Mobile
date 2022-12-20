using NptExplorer.Mobile.ViewModels.Help;
using Xamarin.Forms.Xaml;

namespace NptExplorer.Mobile.Views.Help
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class TermsAndConditionsView : PageBase<TermsAndConditionsViewModel>
	{
		public TermsAndConditionsView()
		{
			InitializeComponent();
		}
	}
}