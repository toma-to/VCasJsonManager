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
    /// PreferencesDialog.xaml の相互作用ロジック
    /// </summary>
    public partial class PreferencesDialog : Window
    {
        public PreferencesDialog()
        {
            InitializeComponent();

            DataContext = DIContainer.Container.Resolve<PreferencesDialogViewModel>();
        }
    }
}
