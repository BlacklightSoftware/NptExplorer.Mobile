using System.ComponentModel;
using NptExplorer.Core.Attributes;

namespace NptExplorer.Core.Enums
{
	public enum Difficulties
	{
		[Language("Easy", "Hawdd")]
		Easy = 1,
		[Language("Some Exertion", "Rhywfaint o waith caled")]
		SomeExertion = 2,
		[Language("Strenuous", "Anodd")]
		Strenuous = 3,
	}
}