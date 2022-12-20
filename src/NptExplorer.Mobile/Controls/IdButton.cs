using Xamarin.Forms;

namespace NptExplorer.Mobile.Controls
{
	public class IdButton : Button
	{
		public static readonly BindableProperty IdProperty =
			BindableProperty.Create(nameof(Id), typeof(int), typeof(IdButton), 0);

		public int Id
		{
			get => (int)GetValue(IdProperty);
			set => SetValue(IdProperty, value);
		}
	}
}