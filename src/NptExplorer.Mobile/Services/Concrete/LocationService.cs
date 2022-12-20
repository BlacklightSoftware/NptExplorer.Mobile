using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using NptExplorer.Core.Models;
using NptExplorer.Dto.Models;
using NptExplorer.Dto.Requests;
using NptExplorer.Mobile.Constants;
using NptExplorer.Mobile.Services.Abstract;

namespace NptExplorer.Mobile.Services.Concrete
{
	public class LocationService : ILocationService
	{
		private readonly IMapper _mapper;
		private readonly IRequestProviderService _requestProviderService;
		private readonly ILoggingService _loggingService;
		public LocationService(ILoggingService loggingService, IMapper mapper, IRequestProviderService requestProviderService)
		{
			_mapper = mapper;
			_requestProviderService = requestProviderService;
			_loggingService = loggingService;
		}

		public async Task<List<LocationOverview>> GetLocationsOverview(LocationRequest request)
		{
			var response = await _requestProviderService.AttemptAndRetry(() => _requestProviderService.Post<LocationRequest, List<LocationOverviewDto>>(ApiUrls.GetLocationOverviews, request));
			return _mapper.Map<List<LocationOverview>>(response);
		}

		public async Task<Location> GetLocation(string id)
		{
			var response = await _requestProviderService.AttemptAndRetry(() => _requestProviderService.Get<LocationDto>(ApiUrls.GetLocation + id));
			return _mapper.Map<Location>(response);
		}

		public async Task<List<LocationOverview>> GetSearchedLocation(LocationRequest request)
		{
			try
			{
				var response = await _requestProviderService.AttemptAndRetry(() => _requestProviderService.Post<LocationRequest, List<LocationOverviewDto>>(ApiUrls.GetSearchedLocation, request));

				var locations = _mapper.Map<List<LocationOverview>>(response);
				return locations;
			}
			catch (Exception ex)
			{
				var test = ex;
				return new List<LocationOverview>();
			}
		}

		public async Task<List<LocationOverview>> GetLocations(LocationRequest request)
		{
			var response = await _requestProviderService.AttemptAndRetry(() => _requestProviderService.Post<LocationRequest, List<LocationOverviewDto>>(ApiUrls.GetLocations, request));
			var locations = _mapper.Map<List<LocationOverview>>(response);
			return locations;
		}
	}
}
