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
    /// ImageListDialog.xaml の相互作用ロジック
    /// </summary>
    public partial class ImageListDialog : Window
    {
        public ImageListDialog()
        {
            InitializeComponent();
            DataContext = DIContainer.Container.Resolve<ImageListDialogViewModel>();
        }
    }
}
