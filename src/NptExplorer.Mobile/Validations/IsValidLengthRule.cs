namespace NptExplorer.Mobile.Validations
{
	public class IsValidLengthRule<T> : IValidationRule<T>
	{
		public string ValidationMessage { get; set; }
		public int MinimumLength { get; set; }
		public int MaximumLength { get; set; }

		public bool Check(T value)
		{
			return value is string str && str.Length >= MinimumLength && str.Length <= MaximumLength;
		}
	}
}