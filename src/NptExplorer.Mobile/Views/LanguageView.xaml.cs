using NptExplorer.Mobile.ViewModels;
using System;
using Xamarin.Forms.Xaml;

namespace NptExplorer.Mobile.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LanguageView : PageBase<LanguageViewModel>
    {
        public LanguageView()
        {
	        try
	        {
		        InitializeComponent();
	        }
	        catch (Exception ex)
	        {
		        var err = ex.Message;
	        }
        }
    }
}