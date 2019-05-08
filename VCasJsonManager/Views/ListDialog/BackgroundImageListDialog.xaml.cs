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
    /// BackgroudImageListDialog.xaml の相互作用ロジック
    /// </summary>
    public partial class BackgroundImageListDialog : Window
    {
        public BackgroundImageListDialog()
        {
            InitializeComponent();
            DataContext = DIContainer.Container.Resolve<BackgroundImageListDialogViewModel>();
        }
    }
}
