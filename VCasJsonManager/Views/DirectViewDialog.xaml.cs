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
    /// DirectViewDialog.xaml の相互作用ロジック
    /// </summary>
    public partial class DirectViewDialog : Window
    {
        public DirectViewDialog()
        {
            InitializeComponent();
            DataContext = DIContainer.Container.Resolve<DirectViewDialogViewModel>();
        }
    }
}
