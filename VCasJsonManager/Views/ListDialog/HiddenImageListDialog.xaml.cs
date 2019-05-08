//
// VCasJsonManager
// Copyright 2019 TOMA
// MIT License
//
using System.Windows;

using Unity;
using VCasJsonManager.ViewModels;

namespace VCasJsonManager.Views
{

    /// <summary>
    /// HiddenImageListDialog.xaml の相互作用ロジック
    /// </summary>
    public partial class HiddenImageListDialog : Window
    {
        public HiddenImageListDialog()
        {
            InitializeComponent();
            DataContext = DIContainer.Container.Resolve<HiddenImageListDialogViewModel>();
        }
    }
}
