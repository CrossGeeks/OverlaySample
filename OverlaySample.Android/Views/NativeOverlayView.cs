using System;
using Android.Content;
using Android.Graphics;
using Android.Views;
using OverlaySample.Controls;

namespace OverlaySample.Droid.Views
{
  
    public class NativeOverlayView : View
    {
        Bitmap windowFrame;


        float overlayOpacity = 0.5f;


        bool showOverlay = false;
        public bool ShowOverlay
        {
            get { return showOverlay; }
            set
            {
                bool repaint = !showOverlay;
                showOverlay = value;
                if (repaint)
                {
                    Redraw();
                }
            }
        }


        public float Opacity
        {
            get { return overlayOpacity; }
            set
            {
                overlayOpacity = value;
                Redraw();
            }
        }

        Color overlayColor = Color.Gray;
        public Color OverlayBackgroundColor
        {
            get { return overlayColor; }
            set
            {
                overlayColor = value;
                Redraw();

            }
        }

        OverlayShape overlayShape = OverlayShape.Circle;

        public OverlayShape Shape
        {
            get { return overlayShape; }
            set
            {
                overlayShape = value;
                Redraw();

            }
        }


        public NativeOverlayView(Context context, bool showOverlay = false) : base(context)
        {
            ShowOverlay = showOverlay;
            SetWillNotDraw(false);
        }

        protected override void OnDraw(Canvas canvas)
        {
            base.OnDraw(canvas);
            if (ShowOverlay)
            {
                if (windowFrame == null)
                {
                    CreateWindowFrame();
                }
                canvas.DrawBitmap(windowFrame, 0, 0, null);
            }
        }
        void Redraw()
        {
            if (ShowOverlay)
            {
                windowFrame?.Recycle();
                windowFrame = null;
                Invalidate();
            }
        }
        void CreateWindowFrame()
        {
            float width = this.Width;
            float height = this.Height;
          
            windowFrame = Bitmap.CreateBitmap((int)width, (int)height, Bitmap.Config.Argb8888);
            Canvas osCanvas = new Canvas(windowFrame);
            Paint paint = new Paint(PaintFlags.AntiAlias)
            {
                Color = OverlayBackgroundColor,
                Alpha = (int)(255 * Opacity)
            };

            RectF outerRectangle = new RectF(0, 0, width, height);

            osCanvas.DrawRect(outerRectangle, paint);

            paint.SetXfermode(new PorterDuffXfermode(PorterDuff.Mode.Clear));


            switch(Shape)
            {
                case OverlayShape.Circle:

                    float radius = Math.Min(width,height) * 0.45f;
                    osCanvas.DrawCircle(width / 2, (height / 2), radius, paint);

                    break;
                default:

                    Path path = new Path();
                    // Starting point
                    path.MoveTo(width / 2, height / 5);

                    // Upper left path
                    path.CubicTo(5 * width / 14, 0,
                            0, height / 15,
                            width / 28, 2 * height / 5);

                    // Lower left path
                    path.CubicTo(width / 14, 2 * height / 3,
                            3 * width / 7, 5 * height / 6,
                            width / 2, height);

                    // Lower right path
                    path.CubicTo(4 * width / 7, 5 * height / 6,
                            13 * width / 14, 2 * height / 3,
                            27 * width / 28, 2 * height / 5);

                    // Upper right path
                    path.CubicTo(width, height / 15,
                            9 * width / 14, 0,
                            width / 2, height / 5);

                    osCanvas.DrawPath(path, paint);
                    break;
            }


        }

        protected override void OnLayout(bool changed, int l, int t, int r, int b)
        {
            base.OnLayout(changed, l, t, r, b);
            windowFrame?.Recycle();
            windowFrame = null;
        }
    }
}
