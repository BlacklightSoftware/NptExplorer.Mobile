using System;
using NptExplorer.Core.Enums;
using NptExplorer.Mobile.ViewModels.Base;

namespace NptExplorer.Mobile.ViewModels
{
	public class BadgePopUpViewModel : ExtendedBindableObject
	{
		private bool _showNature;
		private bool _showWellbeing;
		private bool _showHeritage;
		private bool _showTrail;

		public bool ShowNature
		{
			get => _showNature;
			set
			{
				_showNature = value;
				RaisePropertyChanged(() => ShowNature);
			}
		}

		public bool ShowWellbeing
		{
			get => _showWellbeing;
			set
			{
				_showWellbeing = value;
				RaisePropertyChanged(() => ShowWellbeing);
			}
		}

		public bool ShowHeritage
		{
			get => _showHeritage;
			set
			{
				_showHeritage = value;
				RaisePropertyChanged(() => ShowHeritage);
			}
		}

		public bool ShowTrail
		{
			get => _showTrail;
			set
			{
				_showTrail = value;
				RaisePropertyChanged(() => ShowTrail);
			}
		}

		public BadgePopUpViewModel()
		{
			_showNature = _showWellbeing = _showHeritage = _showTrail = false;
		}

		public void Init(BadgeTypes badge)
		{
			switch (badge)
			{
				case BadgeTypes.Wellbeing:
					ShowWellbeing = true;
					break;
				case BadgeTypes.Nature:
					ShowNature = true;
					break;
				case BadgeTypes.Heritage:
					ShowHeritage = true;
					break;
				case BadgeTypes.Trail:
					ShowTrail = true;
					break;
				default:
					throw new ArgumentOutOfRangeException(nameof(badge), badge, null);
			}
		}
	}
}