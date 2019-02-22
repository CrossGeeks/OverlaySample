using System;
using System.Collections.Generic;
using OverlaySample.Controls;
using Xamarin.Forms;

namespace OverlaySample
{
    public partial class MapPage : ContentPage
    {
        public MapPage()
        {
            InitializeComponent();
        }

        void Handle_ValueChanged(object sender, Xamarin.Forms.ValueChangedEventArgs e)
        {

            if (e.NewValue >= 0 && e.NewValue < 30)
            {
                overlayView.OverlayBackgroundColor = Color.FromHex("#ffffff");
            }
            else if (e.NewValue >= 30 && e.NewValue < 60)
            {
                overlayView.OverlayBackgroundColor = Color.FromHex("#fde5e5");
            }
            else if (e.NewValue >= 60 && e.NewValue < 90)
            {
                overlayView.OverlayBackgroundColor = Color.FromHex("#fccccc");
            }
            else if (e.NewValue >= 90 && e.NewValue < 120)
            {
                overlayView.OverlayBackgroundColor = Color.FromHex("#fab2b2");
            }
            else if (e.NewValue >= 120 && e.NewValue < 150)
            {
                overlayView.OverlayBackgroundColor = Color.FromHex("#f99999");
            }
            else if (e.NewValue >= 150 && e.NewValue < 180)
            {
                overlayView.OverlayBackgroundColor = Color.FromHex("#f87f7f");
            }
            else if (e.NewValue >= 180 && e.NewValue < 210)
            {
                overlayView.OverlayBackgroundColor = Color.FromHex("#f66666");
            }
            else if (e.NewValue >= 210 && e.NewValue < 240)
            {
                overlayView.OverlayBackgroundColor = Color.FromHex("#f54c4c");
            }
            else if (e.NewValue >= 240 && e.NewValue < 270)
            {
                overlayView.OverlayBackgroundColor = Color.FromHex("#f33232");
            }
            else if (e.NewValue >= 270 && e.NewValue < 300)
            {
                overlayView.OverlayBackgroundColor = Color.FromHex("#f21919");
            }
            else if (e.NewValue >= 300 && e.NewValue < 330)
            {
                overlayView.OverlayBackgroundColor = Color.FromHex("#f10000");
            }
            else if (e.NewValue >= 330 && e.NewValue <= 360)
            {
                overlayView.OverlayBackgroundColor = Color.FromHex("#ff0000");
            }
        }

        void Handle_Toggled(object sender, Xamarin.Forms.ToggledEventArgs e)
        {
            if (e.Value)
            {
                overlayView.Shape = OverlayShape.Heart;
            }
            else
            {
                overlayView.Shape = OverlayShape.Circle;
            }
        }
    }
}
