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
    /// CueCardListDialog.xaml の相互作用ロジック
    /// </summary>
    public partial class CueCardListDialog : Window
    {
        public CueCardListDialog()
        {
            InitializeComponent();
            DataContext = DIContainer.Container.Resolve<CueCardListDialogViewModel>();
        }
    }
}
