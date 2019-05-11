//
// VCasJsonManager
// Copyright 2019 TOMA
// MIT License
//
using System.Windows;
using System.Windows.Controls;


namespace VCasJsonManager.Views.Controls.Extensions
{
    /// <summary>
    /// DragDropListで使用する拡張メソッドの定義
    /// </summary>
    public static class DragDropListExtensions
    {
        /// <summary>
        /// RoutedEventArgsのOriginalSourceから、ItemsContolのアイテムのコンテナとなるエレメントを取得
        /// </summary>
        /// <param name="self"></param>
        /// <param name="eventArgs"></param>
        /// <returns></returns>
        public static FrameworkElement GetItemContainerFromRoutedEvent(this ItemsControl self, RoutedEventArgs eventArgs)
        {
            if (!(eventArgs.OriginalSource is UIElement element))
            {
                return null;
            }
            var container = self.ContainerFromElement(element);

            return container as FrameworkElement;
        }

        /// <summary>
        /// ドロップ対象のインデックスの取得
        /// </summary>
        /// <param name="self"></param>
        /// <param name="eventArgs"></param>
        /// <returns></returns>
        public static int GetDropTargetIndex(this ItemsControl self, DragEventArgs eventArgs)
        {
            var container = self.GetItemContainerFromRoutedEvent(eventArgs);
            if (container == null)
            {
                return -1;
            }
            var index = self.ItemContainerGenerator.IndexFromContainer(container);
            if (index < 0)
            {
                return index;
            }

            var pos = eventArgs.GetPosition(container);
            if (container.IsDropPositionBottomHalf(eventArgs))
            {
                index++;
            }
            return index;
        }

        /// <summary>
        /// ドロップ先がアイテムの上下どちらであるかの判定
        /// </summary>
        /// <param name="self"></param>
        /// <param name="eventArgs"></param>
        /// <returns></returns>
        public static bool IsDropPositionBottomHalf(this FrameworkElement self, DragEventArgs eventArgs)
        {
            var pos = eventArgs.GetPosition(self);
            return pos.Y > self.ActualHeight / 2;
        }

    }
}
