using System;
using CoreAnimation;
using CoreGraphics;
using OverlaySample.Controls;
using UIKit;

namespace OverlaySample.iOS.Views
{
    public class NativeOverlayView : UIView
    {



        bool showOverlay = true;
        public bool ShowOverlay
        {
            get { return showOverlay; }
            set
            {
                showOverlay = value;

                if (showOverlay)
                    AddOverlayLayer();
                else
                    RemoveOverlayLayer();
            }
        }

        CAShapeLayer overlayLayer = null;

        public float Opacity { get; set; } = 0.5f;
        public UIColor OverlayBackgroundColor { get; set; } = UIColor.Clear;
        public OverlayShape OverlayShape { get; set; } = OverlayShape.Circle;



        UIBezierPath GetHeartOverlayPath(CGRect originalRect, float scale)
        {
            var scaledWidth = (originalRect.Size.Width * scale);
            var scaledXValue = ((originalRect.Size.Width) - scaledWidth) / 2;
            var scaledHeight = (originalRect.Size.Height * scale);
            var scaledYValue = ((originalRect.Size.Height) - scaledHeight) / 2;

            var scaledRect = new CGRect(x: scaledXValue, y: scaledYValue, width: scaledWidth, height: scaledHeight);
            UIBezierPath path = new UIBezierPath();

            path.MoveTo(new CGPoint(x: originalRect.Size.Width / 2, y: scaledRect.Y + scaledRect.Size.Height));


            path.AddCurveToPoint(new CGPoint(x: scaledRect.X, y: scaledRect.Y + (scaledRect.Size.Height / 4)),
                controlPoint1: new CGPoint(x: scaledRect.X + (scaledRect.Size.Width / 2), y: scaledRect.Y + (scaledRect.Size.Height * 3 / 4)),
                controlPoint2: new CGPoint(x: scaledRect.X, y: scaledRect.Y + (scaledRect.Size.Height / 2)));

            path.AddArc(new CGPoint(scaledRect.X + (scaledRect.Size.Width / 4), scaledRect.Y + (scaledRect.Size.Height / 4)),
                (scaledRect.Size.Width / 4),
                (nfloat)Math.PI,
                 0,
                 true);

            path.AddArc(new CGPoint(scaledRect.X + (scaledRect.Size.Width * 3 / 4), scaledRect.Y + (scaledRect.Size.Height / 4)),
                  (scaledRect.Size.Width / 4),
                 (nfloat)Math.PI,
                 0,
                  true);

            path.AddCurveToPoint(new CGPoint(x: originalRect.Size.Width / 2, y: scaledRect.Y + scaledRect.Size.Height),
            controlPoint1: new CGPoint(x: scaledRect.X + scaledRect.Size.Width, y: scaledRect.Y + (scaledRect.Size.Height / 2)),
            controlPoint2: new CGPoint(x: scaledRect.X + (scaledRect.Size.Width / 2), y: scaledRect.Y + (scaledRect.Size.Height * 3 / 4)));

            path.ClosePath();

            return path;
        }

        UIBezierPath GetCircularOverlayPath()
        {
            int radius = (int)(Bounds.Width / 2) - 20;
            
            UIBezierPath circlePath = UIBezierPath.FromRoundedRect(new CGRect(Bounds.GetMidX() - radius, Bounds.GetMidY() - radius, 2.0 * radius, 2.0 * radius), radius);
            circlePath.ClosePath();
            return circlePath;
        }
    
        public void AddOverlayLayer()
        {
            UIBezierPath path = UIBezierPath.FromRoundedRect(new CGRect(Frame.X, Frame.Y, this.Frame.Width, this.Frame.Height), 0);


            path.AppendPath(OverlayShape == OverlayShape.Circle ? GetCircularOverlayPath() : GetHeartOverlayPath(path.Bounds, 0.95f));

            path.UsesEvenOddFillRule = true;
          
               CAShapeLayer fillLayer = new CAShapeLayer();
                fillLayer.Path = path.CGPath;
                fillLayer.FillRule = CAShapeLayer.FillRuleEvenOdd;
                fillLayer.FillColor =  OverlayBackgroundColor.CGColor;
                fillLayer.Opacity = Opacity;
                overlayLayer = fillLayer;
               Layer.AddSublayer(fillLayer);

        }

       
        public void UpdatePath()
        {

            UIBezierPath path = UIBezierPath.FromRoundedRect(new CGRect(Frame.X, Frame.Y, this.Frame.Width, this.Frame.Height), 0);

            path.AppendPath(OverlayShape == OverlayShape.Circle ? GetCircularOverlayPath() : GetHeartOverlayPath(path.Bounds, 0.95f));

            overlayLayer.Path = path.CGPath;
        }

        public void UpdateOpacity()
        {
            if(overlayLayer!=null)
                overlayLayer.Opacity = Opacity;
        }

        public void UpdateFillColor()
        {
            if (overlayLayer != null)
                overlayLayer.FillColor = OverlayBackgroundColor.CGColor;
        }

        public void RemoveOverlayLayer()
        {
            //if(Layer.Sublayers!=null)
            //foreach (var l in Layer.Sublayers)
            //{
            //    l.RemoveFromSuperLayer();
            //}

            overlayLayer?.RemoveFromSuperLayer();
        }


    }
}
