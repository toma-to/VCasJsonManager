//
// VCasJsonManager
// Copyright 2019 TOMA
// MIT License
//
using System.Windows;

namespace VCasJsonManager.Views.Controls
{
    /// <summary>
    /// ドロップ先の挿入カーソルを表示するAdorner
    /// </summary>
    public class InsertionAdorner : ElementHostAdornerBase
    {
        /// <summary>
        /// 挿入カーソル
        /// </summary>
        private InsertionCursor InsertionCursor { get; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="adornedElement"></param>
        /// <param name="showInTop"></param>
        public InsertionAdorner(UIElement adornedElement, bool showInTop)
            : base(adornedElement)
        {
            InsertionCursor = new InsertionCursor();

            Host.Children.Add(InsertionCursor);

            InsertionCursor.SetValue(VerticalAlignmentProperty,
                showInTop ? VerticalAlignment.Top : VerticalAlignment.Bottom);
            InsertionCursor.SetValue(HorizontalAlignmentProperty, HorizontalAlignment.Stretch);
        }

        /// <summary>
        /// 表示位置の更新
        /// </summary>
        /// <param name="showInTop"></param>
        public void UpdatePos(bool showInTop)
        {
            InsertionCursor.SetValue(VerticalAlignmentProperty,
                showInTop ? VerticalAlignment.Top : VerticalAlignment.Bottom);
        }
    }
}
