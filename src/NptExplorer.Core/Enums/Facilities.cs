using System.ComponentModel;
using NptExplorer.Core.Attributes;

namespace NptExplorer.Core.Enums
{
    public enum Facilities
    {
	    [Language("Cafe/Restaurants", "Caffi/Bwytai")]
	    [Description("Cafe/Restaurants")]
	    CafeRestaurants = 1,
	    [Language("Dog Friendly", "Addas i Gŵn")]
	    [Description("Dog Friendly")]
	    DogFriendly = 2,
	    [Language("Parking", "Parcio")]
	    Parking = 3,
	    [Language("Playground", "Maes Chwarae")]
 	    Playground = 4,
        [Language("Toilets", "Toiledau")]
	    Toilets = 5,
	    [Language("Wheelchair Access", "Hygyrch i Gadeiriau Olwyn")]
	    WheelchairAccess = 6
    }
}