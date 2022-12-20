using NptExplorer.Mobile.Resources;

namespace NptExplorer.Mobile.Helpers
{
	public static class Labels
	{
		public static string GetHelloLabel(string name)
		{
			var greeting = AppResources.Global_Greeting;
			var label = $"{greeting} {name}";
			return label;
		}
	}
}