using System.Collections.Generic;
using System.Linq;
using MvvmHelpers;
using NptExplorer.Core.Enums;


namespace NptExplorer.Core.Models
{
    public class Location : ObservableObject
    {
		public int Id { get; set; }
		public string? NameEnglish { get; set; }
		public string? NameWelsh { get; set; }
		public string? DescriptionEnglish { get; set; }
		public string? DescriptionWelsh { get; set; }
		public string? PrimaryImage { get; set; }
		public string? MapImage { get; set; }
		public string? Type { get; set; }
		public string? Address { get; set; }
		public string? What3Words { get; set; }
		public string? NearestBusStop { get; set; }
		public string? Website { get; set; }
		public string? Parking { get; set; }
		public string? ParkingCharge { get; set; }
		public string? GeneralInformation { get; set; }
		public string? ResourceLink { get; set; }
		public string? GetInvolved { get; set; }
		public string? GetInvolvedLink { get; set; }
		public string? LearnMore { get; set; }
		public string? LearnMoreLink { get; set; }
		public string? Businesses { get; set; }
		public double Latitude { get; set; }
		public double Longitude { get; set; }
		public bool ExplorePreSelected { get; set; }
		public bool AdventurePreSelected { get; set; }
		public Badge? Badges { get; set; }
		public List<int> Facilities { get; set; }
		public List<string> BusStops { get; set; }
		public List<Highlight> HighlightsList { get; set; }


		public string? Language { get; set; }

		public string? LocationDescription => Language == "cy" ? DescriptionWelsh : DescriptionEnglish;
		public string? LocationName => Language == "cy" ? NameWelsh : NameEnglish;
		public string? DisplayBusStops => string.Join(", ", BusStops);

		public Location()
		{
			Facilities = new List<int>();
			BusStops = new List<string>();
			HighlightsList = new List<Highlight>();
		}
	}
}