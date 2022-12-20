using NptExplorer.Core.Attributes;

namespace NptExplorer.Core.Enums
{
	public enum BadgeTypes
	{
		[Language("", "")]
		Anon = 0,
		[Language("Wellbeing", "Llesiant")]
		Wellbeing = 1,
		[Language("Nature", "Natur")]
		Nature = 2,
		[Language("Heritage", "Treftadaeth")]
		Heritage = 3,
		[Language("Trail", "Llwybr")]
		Trail = 4,
	}
}