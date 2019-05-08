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
    /// MylistIdListDialog.xaml の相互作用ロジック
    /// </summary>
    public partial class MylistIdListDialog : Window
    {
        public MylistIdListDialog()
        {
            InitializeComponent();
            DataContext = DIContainer.Container.Resolve<MylistIdListDialogViewModel>();
        }
    }
}
