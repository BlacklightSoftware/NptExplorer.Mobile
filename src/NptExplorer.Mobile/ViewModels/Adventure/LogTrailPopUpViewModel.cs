using NptExplorer.Mobile.Models;
using NptExplorer.Mobile.ViewModels.Base;

namespace NptExplorer.Mobile.ViewModels.Adventure
{
	public class LogTrailPopUpViewModel : ExtendedBindableObject
	{
		private ChallengeTrail _trail;

		public ChallengeTrail Trail
		{
			get => _trail;
			set
			{
				_trail = value;
				RaisePropertyChanged(() => Trail);
			}
		}

		public void Init(ChallengeTrail trail)
		{
			if (trail == null)
			{
				return;
			}

			Trail = trail;
		}
	}
}