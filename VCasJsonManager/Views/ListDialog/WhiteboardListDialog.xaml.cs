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
    /// WhiteboardListDialog.xaml の相互作用ロジック
    /// </summary>
    public partial class WhiteboardListDialog : Window
    {
        public WhiteboardListDialog()
        {
            InitializeComponent();
            DataContext = DIContainer.Container.Resolve<WhitboardListDialogViewModel>();
        }
    }
}
