using System;
using Android.Content;
using Android.Hardware;
using OverlaySample.Controls;
using OverlaySample.Droid.Renderers;
using OverlaySample.Droid.Views;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(CameraPreview), typeof(CameraPreviewRenderer))]
namespace OverlaySample.Droid.Renderers
{
    public class CameraPreviewRenderer : ViewRenderer<CameraPreview, NativeCameraPreview>
    {
        NativeCameraPreview cameraPreview;

        public CameraPreviewRenderer(Context context) : base(context)
        {
        }

        protected override async void OnElementChanged(ElementChangedEventArgs<CameraPreview> e)
        {
            base.OnElementChanged(e);

            if (Control == null)
            {
                cameraPreview = new NativeCameraPreview(Context);
                SetNativeControl(cameraPreview);
            }

            if (e.OldElement != null)
            {
                // Unsubscribe
                cameraPreview.Click -= OnCameraPreviewClicked;
            }
            if (e.NewElement != null)
            {

                try
                {
                    var status = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Camera);
                    if (status != PermissionStatus.Granted)
                    {
                        if (await CrossPermissions.Current.ShouldShowRequestPermissionRationaleAsync(Permission.Camera))
                        {
                           // await DisplayAlert("Need location", "Gunna need that location", "OK");
                        }

                        var results = await CrossPermissions.Current.RequestPermissionsAsync(Permission.Camera);
                        //Best practice to always check that the key exists
                        if (results.ContainsKey(Permission.Camera))
                            status = results[Permission.Camera];
                    }

                    if (status == PermissionStatus.Granted)
                    {
                        Control.Preview = Camera.Open((int)e.NewElement.Camera);
                    }
                    else if (status != PermissionStatus.Unknown)
                    {
                      // await DisplayAlert("Location Denied", "Can not continue, try again.", "OK");
                    }
                }
                catch (Exception ex)
                {

                   // LabelGeolocation.Text = "Error: " + ex;
                }
               

                // Subscribe
                cameraPreview.Click += OnCameraPreviewClicked;
            }
        }

        void OnCameraPreviewClicked(object sender, EventArgs e)
        {
            if (cameraPreview.IsPreviewing)
            {
                cameraPreview.Preview.StopPreview();
                cameraPreview.IsPreviewing = false;
            }
            else
            {
                cameraPreview.Preview.StartPreview();
                cameraPreview.IsPreviewing = true;
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                Control.Preview.Release();
            }
            base.Dispose(disposing);
        }
    }
}