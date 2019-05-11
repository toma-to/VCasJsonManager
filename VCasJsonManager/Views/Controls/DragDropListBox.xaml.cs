//
// VCasJsonManager
// Copyright 2019 TOMA
// MIT License
//
using System;
using System.Collections;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using VCasJsonManager.Views.Controls.Extensions;

namespace VCasJsonManager.Views.Controls
{
    /// <summary>
    /// DragDropListBox.xaml の相互作用ロジック
    /// </summary>
    public partial class DragDropListBox : UserControl
    {
        /// <summary>
        /// ItemsSource
        /// </summary>
        public IList ItemsSource { get => (IList)GetValue(ItemsSourceProperty); set => SetValue(ItemsSourceProperty, value); }

        /// <summary>
        /// ItemsSource DependencyProperty
        /// </summary>
        public static readonly DependencyProperty ItemsSourceProperty =
            DependencyProperty.Register(nameof(ItemsSource), typeof(IList), typeof(DragDropListBox), new FrameworkPropertyMetadata(null));

        /// <summary>
        /// ItemTemplate
        /// </summary>
        public DataTemplate ItemTemplate { get => (DataTemplate)GetValue(ItemTemplateProperty); set => SetValue(ItemTemplateProperty, value); }

        /// <summary>
        /// ItemTemplate DependencyProperty
        /// </summary>
        public static readonly DependencyProperty ItemTemplateProperty =
            DependencyProperty.Register(nameof(ItemTemplate), typeof(DataTemplate), typeof(DragDropListBox), new PropertyMetadata(null));

        /// <summary>
        /// AlternationCount
        /// </summary>
        public int AlternationCount { get => (int)GetValue(AlternationCountProperty); set => SetValue(AlternationCountProperty, value); }

        /// <summary>
        /// AlternationCount DependencyProperty
        /// </summary>
        public static readonly DependencyProperty AlternationCountProperty =
            DependencyProperty.Register(nameof(AlternationCount), typeof(int), typeof(DragDropListBox), new PropertyMetadata(null));


        /// <summary>
        /// ドラッグドロップ操作の開始位置
        /// </summary>
        private Point? InitialPosition { get; set; }

        /// <summary>
        /// ドラッグ中のリストアイテムコンテナ
        /// </summary>
        private FrameworkElement DraggingContainer { get; set; }

        /// <summary>
        /// ドラッグ中のアイテムのインデックス
        /// </summary>
        private int? DraggingItemIndex { get; set; }

        /// <summary>
        /// ドラッグ中のデータ
        /// </summary>
        private object DraggingData { get; set; }

        /// <summary>
        /// ドラッグ開始位置のリストアイテムからのオフセット
        /// </summary>
        private Point? OffsetFromItem { get; set; }

        /// <summary>
        /// ドラッグ中のコンテントのゴースト表示
        /// </summary>
        private DragContentAdorner ContentAdorner { get; set; }

        /// <summary>
        /// 挿入カーソルのAdorner
        /// </summary>
        private InsertionAdorner InsertionAdorner { get; set; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public DragDropListBox()
        {
            InitializeComponent();
            root.DataContext = this;
        }

        /// <summary>
        /// ドラッグアンドドロップ用データの消去
        /// </summary>
        private void CleanupData()
        {
            InitialPosition = null;
            DraggingContainer = null;
            DraggingItemIndex = null;
            DraggingData = null;
            OffsetFromItem = null;
            ContentAdorner?.Dispose();
            ContentAdorner = null;
            InsertionAdorner?.Dispose();
            InsertionAdorner = null;
        }

        /// <summary>
        /// Unload
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UserControl_Unloaded(object sender, RoutedEventArgs e)
        {
            CleanupData();
        }

        /// <summary>
        /// ドラッグ領域のPreviewMouseLeftButtonDown
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DragArea_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var container = listBox.GetItemContainerFromRoutedEvent(e);
            if (container == null)
            {
                return;
            }
            var index = listBox.ItemContainerGenerator.IndexFromContainer(container);
            if (index < 0)
            {
                return;
            }

            DraggingContainer = container;
            DraggingData = container.DataContext;
            DraggingItemIndex = index;
            InitialPosition = PointToScreen(e.GetPosition(this));
            OffsetFromItem = container.PointFromScreen(InitialPosition.Value);
        }

        /// <summary>
        /// リストボックスのPreviewMouseMove
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListBox_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            if (sender != listBox || ContentAdorner != null)
            {
                return;
            }

            var curPos = PointToScreen(e.GetPosition(this));
            var move = (InitialPosition - curPos) ?? new Vector(0, 0);
            if (e.LeftButton != MouseButtonState.Pressed
                || DraggingData == null
                || InitialPosition == null
                || DraggingItemIndex == null
                || OffsetFromItem == null
                || (Math.Abs(move.X) <= SystemParameters.MinimumHorizontalDragDistance
                    && Math.Abs(move.Y) <= SystemParameters.MinimumVerticalDragDistance))
            {
                return;
            }

            ContentAdorner = new DragContentAdorner(DraggingContainer, OffsetFromItem.Value);
            ContentAdorner.SetScreenPosition(curPos);

            DragDrop.DoDragDrop(listBox, DraggingData, DragDropEffects.Move);
            CleanupData();
        }

        /// <summary>
        /// リストボックスのPreviewMouseUp
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListBox_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            CleanupData();
        }

        /// <summary>
        /// リストボックスのPreviewDragOver
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListBox_PreviewDragOver(object sender, DragEventArgs e)
        {
            var curPos = PointToScreen(e.GetPosition(this));
            ContentAdorner.SetScreenPosition(curPos);

            var container = listBox.GetItemContainerFromRoutedEvent(e);
            if (InsertionAdorner != null && container != null)
            {
                InsertionAdorner.UpdatePos(!container.IsDropPositionBottomHalf(e));
            }

        }

        /// <summary>
        /// リストボックスのPreviewDrop
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListBox_PreviewDrop(object sender, DragEventArgs e)
        {
            var destIndex = listBox.GetDropTargetIndex(e);
            if (!(DraggingItemIndex is int sourceIndex))
            {
                return;
            }

            if (destIndex == sourceIndex)
            {
                return;
            }

            ItemsSource.RemoveAt(sourceIndex);

            if (destIndex < 0 || destIndex > ItemsSource.Count)
            {
                ItemsSource.Add(DraggingData);
            }
            else
            {
                destIndex -= destIndex > sourceIndex ? 1 : 0;
                ItemsSource.Insert(destIndex, DraggingData);
            }
        }

        /// <summary>
        /// リストボックスのPreviewDragEnter
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListBox_PreviewDragEnter(object sender, DragEventArgs e)
        {
            var container = listBox.GetItemContainerFromRoutedEvent(e);
            if (container == null)
            {
                if (listBox.ItemContainerGenerator.ContainerFromIndex(ItemsSource.Count - 1) is FrameworkElement lastCont)
                {
                    InsertionAdorner = new InsertionAdorner(lastCont, false);
                }
            }
            else
            {
                InsertionAdorner = new InsertionAdorner(container, !container.IsDropPositionBottomHalf(e));
            }
        }

        /// <summary>
        /// リストボックスのPreviewDragLeave
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListBox_PreviewDragLeave(object sender, DragEventArgs e)
        {
            InsertionAdorner?.Dispose();
            InsertionAdorner = null;
        }
    }
}
