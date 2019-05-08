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
    /// CharacterModelListDialog.xaml の相互作用ロジック
    /// </summary>
    public partial class BackgroundModelListDialog : Window
    {
        public BackgroundModelListDialog()
        {
            InitializeComponent();
            DataContext = DIContainer.Container.Resolve<BackgroundModelListDialogViewModel>();
        }
    }
}
