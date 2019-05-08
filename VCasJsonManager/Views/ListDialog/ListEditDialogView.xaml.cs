//
// VCasJsonManager
// Copyright 2019 TOMA
// MIT License
//
using System.Windows;
using System.Windows.Controls;

namespace VCasJsonManager.Views
{

    /// <summary>
    /// ListEditDialogView.xaml の相互作用ロジック
    /// </summary>
    public partial class ListEditDialogView : UserControl
    {
        public ListEditDialogView()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            inputText.Focus();
        }
    }
}
