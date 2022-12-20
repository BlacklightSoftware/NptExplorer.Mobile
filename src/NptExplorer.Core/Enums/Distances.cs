using System.ComponentModel;

namespace NptExplorer.Core.Enums
{
	public enum Distances
	{
		[Description("1-2km")]
		OneToTwoKm = 1,
		[Description("2-4km")]
		TwoToFourKm = 2,
		[Description("4-6km")]
		FourToSixKm = 3,
		[Description("8+km")]
		OverEightKm = 4,
	}
}