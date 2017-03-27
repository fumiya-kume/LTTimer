using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using LTTimer.UWP.View;
using Xamarin.Forms.Platform.UWP;


[assembly: ExportRenderer(typeof(Xamarin.Forms.Button),typeof(CustonFontTextViewRenderer))]

namespace LTTimer.UWP.View
{
    public class CustonFontTextViewRenderer: ButtonRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.Button> e)
        {
            base.OnElementChanged(e);

            var fontfamily = e.NewElement?.FontFamily;

            if (fontfamily != null)
            {
                var element = (Button) Control;
                element.FontFamily = new FontFamily(e.NewElement.FontFamily);
            }
        }
    }
}
