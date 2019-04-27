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
    /// NicovideoIdListDialog.xaml の相互作用ロジック
    /// </summary>
    public partial class NicovideoIdListDialog : Window
    {
        public NicovideoIdListDialog()
        {
            InitializeComponent();
            DataContext = DIContainer.Container.Resolve<NicovideoIdListDialogViewModel>();
        }
    }
}
