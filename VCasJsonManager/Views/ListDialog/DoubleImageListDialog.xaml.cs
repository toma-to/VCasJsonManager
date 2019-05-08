//
// VCasJsonManager
// Copyright 2019 TOMA
// MIT License
//
using System.Windows;

using Unity;
using VCasJsonManager.ViewModels.ListDialog;

namespace VCasJsonManager.Views.ListDialog
{

    /// <summary>
    /// DoubleImageListDialog.xaml の相互作用ロジック
    /// </summary>
    public partial class DoubleImageListDialog : Window
    {
        public DoubleImageListDialog()
        {
            InitializeComponent();
            DataContext = DIContainer.Container.Resolve<DoubleImageListDialogViewModel>();
        }
    }
}
