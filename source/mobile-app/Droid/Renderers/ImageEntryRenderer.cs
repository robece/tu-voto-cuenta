using System;
using System.Threading.Tasks;
using Android.Content;
using Android.Content.Res;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Support.V4.Content;
using Android.Support.V4.Content.Res;
using Android.Widget;
using TuVotoCuenta.Controls;
using TuVotoCuenta.Droid.Renderers;
using FFImageLoading;
using FFImageLoading.Svg.Platform;
using FFImageLoading.Views;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(ImageEntry), typeof(ImageEntryRenderer))]
namespace TuVotoCuenta.Droid.Renderers
{
    public class ImageEntryRenderer : EntryRenderer
    {
        Context context = null;

        public ImageEntryRenderer(Context context) : base(context)
        {
            this.context = context;
        }

        ImageEntry element;
        protected async override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            try
			{
				base.OnElementChanged(e);

                if (e.OldElement != null || e.NewElement == null)
                    return;

                element = (ImageEntry)this.Element;

                var editText = this.Control;
                if (!string.IsNullOrEmpty(element.Image))
                {
                    switch (element.ImageAlignment)
                    {
                        case ImageAlignment.Left:
                            editText.SetCompoundDrawablesWithIntrinsicBounds(await GetDrawable(element.Image), null, null, null);
                            break;
                        case ImageAlignment.Right:
                            editText.SetCompoundDrawablesWithIntrinsicBounds(null, null, await GetDrawable(element.Image), null);
                            break;
                    }
                }
                editText.CompoundDrawablePadding = 25;
                Control.Background.SetColorFilter(Android.Graphics.Color.Rgb(48, 48, 48), PorterDuff.Mode.SrcAtop);
			}
			catch (Exception)
			{
                //in case any unknown exception in render prevent any notification or bubble
			}
        }

        async Task<BitmapDrawable> GetDrawable(string imageEntryImage)
        {
            return await ImageService.Instance.LoadCompiledResource(imageEntryImage)
                                                  .WithCustomDataResolver(new SvgDataResolver(30, 0, true))
                                                  .AsBitmapDrawableAsync();
        }
    }
}