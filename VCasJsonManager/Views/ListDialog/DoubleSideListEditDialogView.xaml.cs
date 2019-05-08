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
    /// DoubleSideListEditDialogView.xaml の相互作用ロジック
    /// </summary>
    public partial class DoubleSideListEditDialogView : UserControl
    {
        public DoubleSideListEditDialogView()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            inputText.Focus();
        }
    }
}
