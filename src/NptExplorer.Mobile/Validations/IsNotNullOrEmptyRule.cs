namespace NptExplorer.Mobile.Validations
{
	public class IsNotNullOrEmptyRule<T> : IValidationRule<T>
	{
		public string ValidationMessage { get; set; }

		public bool Check(T value)
		{
			return value != null && (value is not string str || !string.IsNullOrWhiteSpace(str));
		}
	}
}