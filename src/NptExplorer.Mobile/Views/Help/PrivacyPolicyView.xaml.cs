using NptExplorer.Mobile.ViewModels.Help;
using Xamarin.Forms.Xaml;

namespace NptExplorer.Mobile.Views.Help
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PrivacyPolicyView : PageBase<PrivacyPolicyViewModel>
	{
		public PrivacyPolicyView()
		{
			InitializeComponent();
		}
	}
}