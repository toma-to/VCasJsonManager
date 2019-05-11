//
// VCasJsonManager
// Copyright 2019 TOMA
// MIT License
//
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace VCasJsonManager.Views.Controls
{
    /// <summary>
    /// ドラッグ中のコンテントのゴースト表示
    /// </summary>
    public class DragContentAdorner : ElementHostAdornerBase
    {
        /// <summary>
        /// TranslateTransform
        /// </summary>
        private TranslateTransform Translate { get; }

        /// <summary>
        /// 表示位置のオフセット
        /// </summary>
        private Point Offset { get; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="adornedElement"></param>
        /// <param name="draggedData"></param>
        /// <param name="dataTemplate"></param>
        /// <param name="offset"></param>
        public DragContentAdorner(UIElement adornedElement, Point offset)
            : base(adornedElement)
        {
            Offset = offset;

            var brush = new VisualBrush(adornedElement) { Opacity = 0.7 };
            var bound = VisualTreeHelper.GetDescendantBounds(adornedElement);
            var rect = new Rectangle() { Width = bound.Width, Height = bound.Height };
            rect.Fill = brush;
            Host.Children.Add(rect);

            Translate = new TranslateTransform { X = 0, Y = 0 };
            Host.RenderTransform = Translate;
        }

        /// <summary>
        /// 表示位置の設定
        /// </summary>
        /// <param name="screenPosition"></param>
        public void SetScreenPosition(Point screenPosition)
        {
            var pos = AdornedElement.PointFromScreen(screenPosition);
            Translate.X = pos.X - Offset.X;
            Translate.Y = pos.Y - Offset.Y;
            AdornerLayer.Update();

        }
    }
}
