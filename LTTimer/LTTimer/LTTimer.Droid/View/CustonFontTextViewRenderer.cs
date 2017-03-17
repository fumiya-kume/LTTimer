using Android.Graphics;
using Android.Widget;
using LTTimer.Droid.View;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Button = Xamarin.Forms.Button;

[assembly:ExportRenderer(typeof(Button),typeof(CustonFontTextViewRenderer))]

namespace LTTimer.Droid.View
{
    public class CustonFontTextViewRenderer: ButtonRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.Button> e)
        {
            base.OnElementChanged(e);

            var fontFamily = e.NewElement?.FontFamily?.ToLower();
            if (fontFamily != null)
            {
                var label = (TextView)Control;
                
                label.Typeface = Typeface.CreateFromAsset(Context.Assets,e.NewElement.FontFamily);
            }
        }
    }
}