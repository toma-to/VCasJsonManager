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
    /// CommentListDialog.xaml の相互作用ロジック
    /// </summary>
    public partial class CommentListDialog : Window
    {
        public CommentListDialog()
        {
            InitializeComponent();
            DataContext = DIContainer.Container.Resolve<CommentListDialogViewModel>();
        }
    }
}
