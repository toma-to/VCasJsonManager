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
    /// VrDebugDialog.xaml の相互作用ロジック
    /// </summary>
    public partial class VrDebugDialog : Window
    {
        public VrDebugDialog()
        {
            InitializeComponent();
            DataContext = DIContainer.Container.Resolve<VrDebugDialogViewModel>();
        }
    }
}
