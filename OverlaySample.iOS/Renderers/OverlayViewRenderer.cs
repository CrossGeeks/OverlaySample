using System;
using System.ComponentModel;
using CoreGraphics;
using OverlaySample.Controls;
using OverlaySample.iOS.Renderers;
using OverlaySample.iOS.Views;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(OverlayView), typeof(OverlayViewRenderer))]
namespace OverlaySample.iOS.Renderers
{
    public class OverlayViewRenderer : ViewRenderer<OverlayView, NativeOverlayView>
    {
        protected override void OnElementChanged(ElementChangedEventArgs<OverlayView> e)
        {
            base.OnElementChanged(e);

            if (Control == null)
            {
                SetNativeControl(new NativeOverlayView()
                {
                    ContentMode= UIViewContentMode.ScaleToFill
                });
            }

            if (e.NewElement != null)
            {
                Control.Opacity = (float)Element.OverlayOpacity;
                Control.ShowOverlay = Element.ShowOverlay;
                Control.OverlayBackgroundColor = Element.OverlayBackgroundColor.ToUIColor();
                Control.OverlayShape = Element.Shape;
            }
        }
        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (e.PropertyName == OverlayView.OverlayOpacityProperty.PropertyName)
            {
                Control.Opacity = (float)Element.OverlayOpacity;
                Control.UpdateOpacity();
            }
            else if (e.PropertyName == OverlayView.OverlayBackgroundColorProperty.PropertyName)
            {
                Control.OverlayBackgroundColor = Element.OverlayBackgroundColor.ToUIColor();
                Control.UpdateFillColor();
            }
            else if (e.PropertyName == OverlayView.ShapeProperty.PropertyName)
            {
                Control.OverlayShape = Element.Shape;
                Control.UpdatePath();
            }
            else if (e.PropertyName == OverlayView.ShowOverlayProperty.PropertyName)
            {
                Control.ShowOverlay = Element.ShowOverlay;
            }
           

        }

        bool rendered = false;
        public override void LayoutSubviews()
        {
            base.LayoutSubviews();
            if(!rendered && Control.ShowOverlay)
            {
                Control.AddOverlayLayer();
                rendered = true;
            }
          
        }


    }
}
