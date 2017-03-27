using LTTimer.iOS.View;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly:ExportRenderer(typeof(Button),typeof(CustonFontTextViewRenderer))]

namespace LTTimer.iOS.View
{
    public class CustonFontTextViewRenderer: ButtonRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Button> e)
        {
            base.OnElementChanged(e);

            var fontFamily = e.NewElement?.FontFamily;
            if (fontFamily != null)
            {
                var element = Control;
                element.TitleLabel.Font = UIFont.FromName(fontFamily,element.TitleLabel.Font.PointSize);
            }
        }
    }
}
