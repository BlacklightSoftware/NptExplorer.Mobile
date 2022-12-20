using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using AndroidX.Core.Content;
using NptExplorer.Mobile.Controls;
using NptExplorer.Mobile.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(ImageEntry), typeof(ImageEntryRenderer))]
namespace NptExplorer.Mobile.Droid.Renderers
{
	public class ImageEntryRenderer : EntryRenderer
	{
		private ImageEntry element;

		public ImageEntryRenderer(Context context) : base(context)
		{
			
		}

		protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
		{
			base.OnElementChanged(e);

			if (e.OldElement != null || e.NewElement == null)
			{
				return;
			}

			element = (ImageEntry)this.Element;


			var editText = this.Control;
			if (!string.IsNullOrEmpty(element.Image))
			{
				switch (element.ImageAlignment)
				{
					case ImageAlignment.Left:
						editText.SetCompoundDrawablesWithIntrinsicBounds(GetDrawable(element.Image), null, null, null);
						break;
					case ImageAlignment.Right:
						editText.SetCompoundDrawablesWithIntrinsicBounds(null, null, GetDrawable(element.Image), null);
						break;
				}
			}

			//var shape = new ShapeDrawable(new Android.Graphics.Drawables.Shapes.RectShape());
			//shape.Paint.Color = Xamarin.Forms.Color.Black.ToAndroid();
			//shape.Paint.SetStyle(Paint.Style.Stroke);
			//editText.Background = shape;
			editText.CompoundDrawablePadding = 25;
			Control.Background.SetColorFilter(element.LineColor.ToAndroid(), PorterDuff.Mode.SrcAtop);
		}

		private BitmapDrawable GetDrawable(string imageEntryImage)
		{
			var resID = Resources.GetIdentifier(imageEntryImage, "drawable", this.Context.PackageName);
			var drawable = ContextCompat.GetDrawable(this.Context, resID);
			var bitmap = ((BitmapDrawable)drawable).Bitmap;

			return new BitmapDrawable(Resources, Bitmap.CreateScaledBitmap(bitmap, element.ImageWidth * 2, element.ImageHeight * 2, true));
		}
	}
}