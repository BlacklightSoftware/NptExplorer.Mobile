using Microsoft.Extensions.DependencyInjection;
using NptExplorer.Mobile.Mapper;
using NptExplorer.Mobile.Services.Abstract;
using NptExplorer.Mobile.Services.Concrete;
using NptExplorer.Mobile.ViewModels;
using NptExplorer.Mobile.ViewModels.Adventure;
using NptExplorer.Mobile.ViewModels.Explore;
using NptExplorer.Mobile.ViewModels.Help;
using NptExplorer.Mobile.ViewModels.Profile;

namespace NptExplorer.Mobile
{
	public static class DependencyInjectionContainer
	{
		public static IServiceCollection ConfigureServices(this IServiceCollection services)
		{
            #region Services
            services.AddSingleton<IAdventureService, AdventureService>();
            services.AddSingleton<IAuthService, AuthService>();
            services.AddSingleton<ICacheService, CacheService>();
            services.AddSingleton<IConnectionService, ConnectionService>();
            services.AddSingleton<IDependencyService, DependencyService>();
            services.AddSingleton<IDialogService, DialogService>();
            services.AddSingleton<ILocationService, LocationService>();
            services.AddSingleton<INavigationService, NavigationService>();
			services.AddSingleton<IRequestProviderService, RequestProviderService>();
			services.AddSingleton<ISettingsService, SettingsService>();
			services.AddSingleton<ITwitterService, TwitterService>();
			services.AddSingleton<IUserService, UserService>();


#if DEBUG
			services.AddSingleton<ILoggingService, DebugLoggingService>();
#else
			services.AddSingleton<ILoggingService, AppCenterLoggingService>();
#endif

			var config = AppMapperConfiguration.Configure().CreateMapper();
			services.AddSingleton(config);

			#endregion

			return services;
		}

		public static IServiceCollection ConfigureViewModels(this IServiceCollection services)
		{
			#region Adventure View Models
			services.AddTransient<AdventureHomeViewModel>();
			services.AddTransient<ChallengeDetailsViewModel>();
			services.AddTransient<ChallengeFilterViewModel>();
			services.AddTransient<ChallengeNavigateViewModel>();
			services.AddTransient<ChallengesViewModel>();
			services.AddTransient<FilterViewModel>();
			services.AddTransient<PoiPopUpViewModel>();
			services.AddTransient<TrailDetailsViewModel>();
			services.AddTransient<TrailNavigateViewModel>();
			services.AddTransient<TrailsViewModel>();
			services.AddTransient<LogTrailPopUpViewModel>();
			#endregion

			#region Explore View Models
			services.AddTransient<ExploreViewModel>();
			services.AddTransient<ExploreMapViewModel>();
			services.AddTransient<ExploreSearchedItemViewModel>();
			services.AddTransient<LocationPopUpViewModel>();
			#endregion
			
			#region Help View Models
			services.AddTransient<PrivacyPolicyViewModel>();
			services.AddTransient<TermsAndConditionsViewModel>();
			#endregion

			#region Profile View Models
			services.AddTransient<ProfileViewModel>();
			services.AddTransient<ProfileFriendsViewModel>();
			services.AddTransient<ProfileAdventureBoardViewModel>();
			#endregion

			#region Resources View Models
			#endregion

			#region Other View Models
			services.AddTransient<AboutViewModel>();
			services.AddTransient<BadgePopUpViewModel>();
			services.AddTransient<ExploreFilterViewModel>();
			services.AddTransient<FilterViewModel>();
			services.AddTransient<LandingViewModel>();
			services.AddTransient<LanguageViewModel>();
            services.AddTransient<RatingViewModel>();
            services.AddTransient<ResourcesViewModel>();
            services.AddTransient<SettingViewModel>();
            services.AddTransient<SignUpViewModel>();
            services.AddTransient<DevSettingsViewModel>();
			#endregion

			return services;
		}
	}
}