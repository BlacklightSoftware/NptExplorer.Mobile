using System.Text.RegularExpressions;

namespace NptExplorer.Mobile.Validations
{
	public class IsValidUserName<T> : IValidationRule<T>
	{
		public string ValidationMessage { get; set; }

		public bool Check(T value)
		{
			try
			{
				if (value == null || string.IsNullOrEmpty(value.ToString()))
				{
					return false;
				}

				const string pattern = @"^\w+$";
				var regex = new Regex(pattern, RegexOptions.Compiled);
				return regex.IsMatch(value.ToString());
			}
			catch
			{
				return false;
			}
		}
	}
}