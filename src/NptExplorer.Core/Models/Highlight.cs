namespace NptExplorer.Core.Models
{
	public class Highlight
	{
		public int Sequence { get; set; }
		public string? HighlightEnglish { get; set; }
		public string? HighlightWelsh { get; set; }

		public string? Language { get; set; }
		public string? HighlightText => Language == "cy" ? HighlightWelsh : HighlightEnglish;
	}
}