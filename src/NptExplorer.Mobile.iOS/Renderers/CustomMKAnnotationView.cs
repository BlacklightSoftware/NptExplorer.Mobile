using MapKit;

namespace NptExplorer.Mobile.iOS.Renderers
{
	public class CustomMkAnnotationView : MKAnnotationView
	{
		public string Name { get; set; }

		public string BadgeLabel { get; set; }

		public CustomMkAnnotationView(IMKAnnotation annotation, string id)
			: base(annotation, id)
		{
		}
	}
}