namespace NptExplorer.Mobile.Validations
{
	public class IsValidEmailRule<T> : IValidationRule<T>
	{
		public string ValidationMessage { get; set; }

		public bool Check(T value)
		{
			try
			{
				if (value == null || string.IsNullOrEmpty(value.ToString()))
				{
					return true;
				}

				var email = new System.Net.Mail.MailAddress($"{value}");
				return email.Address == $"{value}";
			}
			catch
			{
				return false;
			}
		}
	}
}