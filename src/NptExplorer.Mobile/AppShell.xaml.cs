using NptExplorer.Mobile.Views;
using NptExplorer.Mobile.Views.Adventure;
using NptExplorer.Mobile.Views.Explore;
using NptExplorer.Mobile.Views.Help;
using NptExplorer.Mobile.Views.Profile;
using Xamarin.Forms;

namespace NptExplorer.Mobile
{
    public partial class AppShell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute("about", typeof(AboutView));
            Routing.RegisterRoute("landing", typeof(LandingView));
			Routing.RegisterRoute("language", typeof(LanguageView));
			Routing.RegisterRoute("signUp", typeof(SignUpView));
			Routing.RegisterRoute("rating", typeof(RatingView));

            Routing.RegisterRoute("adventureFilter", typeof(FilterView));
            Routing.RegisterRoute("adventureTrails", typeof(TrailsView));
            Routing.RegisterRoute("adventureTrailDetails", typeof(TrailDetailsView));
            Routing.RegisterRoute("adventureTrailNavigate", typeof(TrailNavigateView));
            Routing.RegisterRoute("adventureChallengeDetails", typeof(ChallengeDetailsView));
            Routing.RegisterRoute("adventureChallenges", typeof(ChallengesView));
            Routing.RegisterRoute("adventureChallengeFilter", typeof(ChallengeFilterView));
            Routing.RegisterRoute("adventureChallengeNavigate", typeof(ChallengeNavigateView));

            Routing.RegisterRoute("exploreExplore", typeof(ExploreView));
            Routing.RegisterRoute("explorefilters", typeof(ExploreFilterView));
            Routing.RegisterRoute("exploremap", typeof(ExploreMapView));
            Routing.RegisterRoute("exploreSearchedItem", typeof(ExploreSearchedItemView));

            Routing.RegisterRoute("profileFriends", typeof(ProfileFriendsView));
            Routing.RegisterRoute("profileAdventureBoard", typeof(ProfileAdventureBoardView));

            Routing.RegisterRoute("settings", typeof(SettingView));
            Routing.RegisterRoute("devSettings", typeof(DevSettingsView));

            Routing.RegisterRoute("helpTerms", typeof(TermsAndConditionsView));
            Routing.RegisterRoute("helpPrivacy", typeof(PrivacyPolicyView));
        }
    }
}
