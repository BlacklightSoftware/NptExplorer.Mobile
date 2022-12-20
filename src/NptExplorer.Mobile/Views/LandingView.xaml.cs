using NptExplorer.Mobile.ViewModels;
using Xamarin.Forms.Xaml;

namespace NptExplorer.Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LandingView : PageBase<LandingViewModel>
    {
        public LandingView()
        {
            InitializeComponent();
        }
    }
}