using System;
using System.Collections.Generic;
using NptExplorer.Mobile.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NptExplorer.Mobile.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SignUpView : PageBase<SignUpViewModel>
	{
		public SignUpView()
		{
			InitializeComponent();
		}

		protected override void OnAppearing()
		{
			base.OnAppearing();
			ViewModel.Init();
		}
	}
}