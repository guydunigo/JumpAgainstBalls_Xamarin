using System.Collections.Generic;
using Xamarin.Forms;
using SkiaSharp;
using SkiaSharp.Views.Forms;

namespace JumpAgainstBalls_Xamarin
{
    public class GameView : ContentView
	{
        public Label Txt { get; }
        internal BallPlayer Player { get; set; }
        internal List<Ball> Balls { get; set; }
        internal GameThreadObj ThreadObj { get; set; }
        public bool IsDemo { get; set; }

        public float WidthF { get => (float)(Content as SKCanvasView).CanvasSize.Width; }
        public float HeightF { get => (float)(Content as SKCanvasView).CanvasSize.Height; }

        public GameView ()
        {
            IsDemo = false;

            BackgroundColor = Color.Black;

            var canvasView = new SKCanvasView();
            canvasView.PaintSurface += CanvasView_PaintSurface;
            canvasView.SizeChanged += CanvasView_SizeChanged;
            Content = canvasView;
        }

        private void CanvasView_SizeChanged(object sender, System.EventArgs args)
        {
            ThreadObj.ResizeRequested = true;
        }

        private void CanvasView_PaintSurface(object sender, SKPaintSurfaceEventArgs args)
        {
            var surface = args.Surface;
            var canvas = surface.Canvas;
            var offsetY = ThreadObj.OffsetY;

            canvas.Clear();

            if (Player != null)
            {
                canvas.DrawCircle(
                    Player.X,
                    Player.Y + offsetY,
                    Player.Radius,
                    new SKPaint() { Color = Extensions.ToSKColor(Player.Color) });
            }

            if (Balls != null)
            {
                foreach (Ball b in Balls)
                {
                    canvas.DrawCircle(
                        b.X,
                        b.Y + offsetY,
                        b.Radius,
                        new SKPaint() { Color = Extensions.ToSKColor(b.Color) });
                }
            }
        }

        public void ReDraw()
        {
            (Content as SKCanvasView).InvalidateSurface();
        }
    }
}