using NptExplorer.Mobile.Resources;
using NptExplorer.Mobile.Services.Abstract;
using NptExplorer.Mobile.ViewModels.Base;
using Xamarin.Forms;

namespace NptExplorer.Mobile.ViewModels.Help
{
	public class TermsAndConditionsViewModel : ViewModelBase
	{
		public HtmlWebViewSource TermsAndConditionsHtml { get; set; }

		public TermsAndConditionsViewModel(INavigationService navigationService) : base(navigationService)
		{
			var html = AppResources.TandC_Html;
			TermsAndConditionsHtml = new HtmlWebViewSource();
			TermsAndConditionsHtml.Html = html;
		}
	}
}