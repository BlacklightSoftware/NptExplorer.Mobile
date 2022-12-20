using System.ComponentModel;
using NptExplorer.Mobile.ViewModels;
using Xamarin.Forms.Xaml;

namespace NptExplorer.Mobile.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ResourcesView : PageBase<ResourcesViewModel>
	{
		public ResourcesView()
		{
			InitializeComponent();
			ViewModel.PropertyChanged += Vm_PropertyChanged;
		}

		private void Vm_PropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			TitleLabel.Text = e.PropertyName switch
			{
				nameof(ViewModel.ScreenTitle) => ((ResourcesViewModel)BindingContext).ScreenTitle,
				_ => TitleLabel.Text
			};
		}
	}
}