using NptExplorer.Core.Attributes;

namespace NptExplorer.Core.Enums
{
    public enum Activities
    {
	    [Language("Cycling", "Beicio")]
        Cycling = 1,
        [Language("Wildlife Observation", "Gwylio Bywyd Gwyllt")]
        WildlifeObservation = 2,
        [Language("Hiking", "Heicio")]
        Hiking = 3,
        [Language("Lakes", "Llynnoedd")]
        Lakes = 4,
        [Language("Waterfalls", "Rhaeadrau")]
        Waterfalls = 5,
        [Language("Volunteer", "Gwirfoddolwr")]
        Volunteer = 6
    }
}