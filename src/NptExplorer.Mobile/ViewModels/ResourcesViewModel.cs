using NptExplorer.Mobile.Services.Abstract;
using NptExplorer.Mobile.ViewModels.Base;
using NptExplorer.Mobile.Views;
using System.Windows.Input;
using Xamarin.Forms;

namespace NptExplorer.Mobile.ViewModels
{
    public class ResourcesViewModel : ViewModelBase
    {
        public ResourcesViewModel(INavigationService navigationService) : base(navigationService)
        {
            ExecuteGeneralVisible = new Command(ChangeGeneralVisible);
            ExecuteGetInvolved = new Command(ChangeGetInvolvedVisble);
            ExecuteLearnMore = new Command(ChangeIsLearningmMoreVisible);
			ExecuteOpenSetting = new Command(OpenSetting);

	}
	public bool IsGeneralVisible { get; set; } = true;
        public bool IsGetInvolvedVisible { get; set; } = false;
        public bool IsLearnMoreVisible { get; set; } = false;
        public bool IsSettingVisible { get; set; } = false;
        public bool IsResourceVisible { get; set; } = true;
        public string ResourceButtonColorGeneral { get; set; } = "#006273";
        public string ResourceButtonColorGetInvolved { get; set; }
        public string ResourceButtonColorLearnMore { get; set; }
        public string Name { get; set; } = "Hello Yannick";
        public ICommand ExecuteGeneralVisible { get; set; }
        public ICommand ExecuteGetInvolved { get; set; }
        public ICommand ExecuteLearnMore { get; set; }
        public ICommand ExecuteOpenSetting { get; set; }

        public void ChangeGeneralVisible()
        {
            IsGeneralVisible = true;
            IsGetInvolvedVisible = false;
            IsLearnMoreVisible = false;
            ChangeResourceButtonColor("General", "#006273");
            UpdateResourceButton();
            UpdateResourceVisibility();
        }

        public void ChangeGetInvolvedVisble()
        {
            IsGetInvolvedVisible = true;
            IsLearnMoreVisible = false;
            IsGeneralVisible = false;
            ChangeResourceButtonColor("GetInvolved", "#006273");
            UpdateResourceButton();
            UpdateResourceVisibility();
        }

        public void ChangeIsLearningmMoreVisible()
        {
            IsLearnMoreVisible = true;
            IsGeneralVisible = false;
            IsGetInvolvedVisible = false;
            ChangeResourceButtonColor("LearnMore", "#006273");
            UpdateResourceButton();
            UpdateResourceVisibility();

        }

        public async void OpenSetting()
        {
			await NavigationService.GoToAsync("/settings");
		}

        public void UpdateResourceButton()
        {
            OnPropertyChanged(nameof(ResourceButtonColorGeneral));
            OnPropertyChanged(nameof(ResourceButtonColorGetInvolved));
            OnPropertyChanged(nameof(ResourceButtonColorLearnMore));
        }

        public void UpdateResourceVisibility()
        {
            OnPropertyChanged(nameof(IsLearnMoreVisible));
            OnPropertyChanged(nameof(IsGeneralVisible));
            OnPropertyChanged(nameof(IsGetInvolvedVisible));
        }

        public void ChangeResourceButtonColor(string btnType,string color)
        {
            switch (btnType)
            {
                case "General":
                    ResourceButtonColorGeneral = color;
                    ResourceButtonColorGetInvolved = "transparent";
                    ResourceButtonColorLearnMore = "transparent";
                    break;
                case "GetInvolved":
                    ResourceButtonColorGeneral = "transparent";
                    ResourceButtonColorGetInvolved = color;
                    ResourceButtonColorLearnMore = "transparent";
                    break;
                case "LearnMore":
                    ResourceButtonColorGeneral = "transparent";
                    ResourceButtonColorGetInvolved = "transparent";
                    ResourceButtonColorLearnMore = color;
                    break;
                default:
                    ResourceButtonColorGeneral = "transparent";
                    ResourceButtonColorGetInvolved = "transparent";
                    ResourceButtonColorLearnMore = "transparent";
                    break;
            }
        }
    }
}