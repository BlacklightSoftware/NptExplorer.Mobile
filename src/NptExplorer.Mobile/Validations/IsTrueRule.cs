namespace NptExplorer.Mobile.Validations
{
	public class IsTrueRule<T> : IValidationRule<T>
	{
		public string ValidationMessage { get; set; }

		public bool Check(T value)
		{
			return bool.Parse($"{value}");
		}
	}
}