//
// VCasJsonManager
// Copyright 2019 TOMA
// MIT License
//
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

namespace VCasJsonManager.Views.Controls
{
    /// <summary>
    /// ゴースト表示処理の基本クラス
    /// </summary>
    public abstract class ElementHostAdornerBase : Adorner, IDisposable
    {
        /// <summary>
        /// AdornerLayer
        /// </summary>
        protected AdornerLayer AdornerLayer { get; }

        /// <summary>
        /// 表示するためのGrid
        /// </summary>
        protected Grid Host { get; set; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="adornedElement"></param>
        protected ElementHostAdornerBase(UIElement adornedElement)
            : base(adornedElement)
        {
            AdornerLayer = AdornerLayer.GetAdornerLayer(adornedElement);
            Host = new Grid();

            if (AdornerLayer != null)
            {
                AdornerLayer.Add(this);
            }
        }

        /// <summary>
        /// Override of VisualChildrenCount.
        /// </summary>
        protected override int VisualChildrenCount => 1;

        /// <summary>
        /// MeasureOverride
        /// </summary>
        /// <param name="constraint"></param>
        /// <returns></returns>
        protected override Size MeasureOverride(Size constraint)
        {
            Host.Measure(constraint);
            return base.MeasureOverride(constraint);
        }

        /// <summary>
        /// ArrangeOverride
        /// </summary>
        /// <param name="finalSize"></param>
        /// <returns></returns>
        protected override Size ArrangeOverride(Size finalSize)
        {
            Host.Arrange(new Rect(finalSize));
            return base.ArrangeOverride(finalSize);
        }

        /// <summary>
        /// GetVisualChild
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        protected override Visual GetVisualChild(int index)
        {
            if (VisualChildrenCount <= index)
            {
                throw new IndexOutOfRangeException();
            }
            return Host;
        }

        private bool _disposed;

        public void Dispose()
        {
            if (_disposed)
            {
                return;
            }
            _disposed = true;
            AdornerLayer.Remove(this);
        }

    }
}