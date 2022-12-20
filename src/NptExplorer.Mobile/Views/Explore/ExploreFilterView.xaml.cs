using System;
using NptExplorer.Mobile.ViewModels.Explore;
using Xamarin.Forms.Xaml;

namespace NptExplorer.Mobile.Views.Explore
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ExploreFilterView : PageBase<ExploreFilterViewModel>
	{
		public ExploreFilterView()
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

		protected override void OnAppearing()
		{
			base.OnAppearing();
			ViewModel.Init();
		}
	}
}