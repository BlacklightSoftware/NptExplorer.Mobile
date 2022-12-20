using System.IO.Compression;
using AutoMapper;
using NptExplorer.Core.Enums;
using NptExplorer.Core.Models;
using NptExplorer.Core.Models.SocialMedia;
using NptExplorer.Dto.Models;
using NptExplorer.Mobile.Factory;
using NptExplorer.Mobile.Models;
using NptExplorer.Mobile.Services.Abstract;
using User = NptExplorer.Core.Models.User;

namespace NptExplorer.Mobile.Mapper
{
    public class AppMapperConfiguration
    {
        public static MapperConfiguration Configure()
        {
            return new(cfg =>
            {
	            cfg.CreateMap<Filters, FiltersDto>();
	            cfg.CreateMap<ExploreFilters, ExploreFiltersDto>();

	            cfg.CreateMap<TrailDto, Trail>()
		            .ForMember(dest => dest.Difficulty, action => action.MapFrom(src => (Difficulties)src.Difficulty))
		            .ForMember(dest => dest.Language, action => action.MapFrom(src => SetLanguage()));
	            cfg.CreateMap<ChallengeOverviewDto, ChallengeOverview>()
		            .ForMember(dest => dest.Language, action => action.MapFrom(src => SetLanguage()));
	            cfg.CreateMap<ChallengeBadgeDto, Badge>()
		            .ForMember(dest => dest.BadgeTypeId, action => action.MapFrom(src => (BadgeTypes)src.BadgeTypeId));
	            cfg.CreateMap<LatLongDto, LatLong>();
	            cfg.CreateMap<LatLong, LatLongDto>();
	            cfg.CreateMap<PointOfInterestDto, PointOfInterest>()
		            .ForMember(dest => dest.Language, action => action.MapFrom(src => SetLanguage()));
	            cfg.CreateMap<ChallengeDto, Challenge>()
		            .ForMember(dest => dest.Language, action => action.MapFrom(src => SetLanguage()));

	            cfg.CreateMap<LocationDto, Location>()
		            .ForMember(dest => dest.Language, action => action.MapFrom(src => SetLanguage()));
	            cfg.CreateMap<LocationOverviewDto, LocationOverview>()
		            .ForMember(dest => dest.Language, action => action.MapFrom(src => SetLanguage()));
	            cfg.CreateMap<HighlightDto, Highlight>()
		            .ForMember(dest => dest.Language, action => action.MapFrom(src => SetLanguage()));

	            cfg.CreateMap<UserDto, User>();
	            cfg.CreateMap<PointOfInterest, ChallengePoi>();
	            cfg.CreateMap<LocationOverview, ExploreLocation>();
	            cfg.CreateMap<TrailRouteDto, TrailRoute>()
		            .ForMember(dest => dest.Difficulty, action => action.MapFrom(src => (Difficulties)src.Difficulty))
		            .ForMember(dest => dest.Language, action => action.MapFrom(src => SetLanguage()));

	            cfg.CreateMap<Core.Models.SocialMedia.User, Tweeter>()
		            .ForMember(dest => dest.Id, action => action.MapFrom(src => src.id))
		            .ForMember(dest => dest.Name, action => action.MapFrom(src => src.name))
		            .ForMember(dest => dest.Username, action => action.MapFrom(src => src.username))
		            .ForMember(dest => dest.ProfileImageUrl, action => action.MapFrom(src => src.profile_image_url));

	            cfg.CreateMap<PrerequisiteDataDto, PrerequisiteData>();

			});
        }

        private static string SetLanguage()
        {
	        var settingsService = LocatorFactory.Resolve<ISettingsService>();
	        var language = settingsService.GetLanguage();
	        return string.IsNullOrEmpty(language) ? "en" : language;
        }
    }
}