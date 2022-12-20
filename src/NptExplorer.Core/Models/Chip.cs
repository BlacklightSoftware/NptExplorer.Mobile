using System.ComponentModel;
using System.Runtime.CompilerServices;
using NptExplorer.Core.Annotations;

namespace NptExplorer.Core.Models
{
	public class Chip : INotifyPropertyChanged
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public bool Selected { get; set; }

		public Chip(int id, string title)
		{
			Id = id;
			Title = title;
			Selected = false;
		}

		public event PropertyChangedEventHandler? PropertyChanged;

		[NotifyPropertyChangedInvocator]
		protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}