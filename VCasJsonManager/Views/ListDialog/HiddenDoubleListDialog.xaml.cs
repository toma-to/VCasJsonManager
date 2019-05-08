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
    /// HiddenDoubleListDialog.xaml の相互作用ロジック
    /// </summary>
    public partial class HiddenDoubleListDialog : Window
    {
        public HiddenDoubleListDialog()
        {
            InitializeComponent();
            DataContext = DIContainer.Container.Resolve<HiddenDoubleListDialogViewModel>();
        }
    }
}
