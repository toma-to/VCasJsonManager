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
    /// CharacterModelListDialog.xaml の相互作用ロジック
    /// </summary>
    public partial class CharacterModelListDialog : Window
    {
        public CharacterModelListDialog()
        {
            InitializeComponent();
            DataContext = DIContainer.Container.Resolve<CharacterModelListDialogViewModel>();
        }
    }
}
