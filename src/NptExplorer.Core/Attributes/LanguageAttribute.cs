using System;

namespace NptExplorer.Core.Attributes
{
	public class LanguageAttribute : Attribute
	{
		public string EnglishText { get; private set; }
		public string WelshText { get; private set; }

		public LanguageAttribute(string englishText, string welshText)
		{
			this.EnglishText = englishText;
			this.WelshText = welshText;
		}
	}
}