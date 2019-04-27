//
// VCasJsonManager
// Copyright 2019 TOMA
// MIT License
//
using System.Windows.Controls;
using Unity;
using VCasJsonManager.ViewModels;

namespace VCasJsonManager.Views
{

    /// <summary>
    /// PresetControlView.xaml の相互作用ロジック
    /// </summary>
    public partial class PresetControlView : UserControl
    {
        public PresetControlView()
        {
            InitializeComponent();
            DataContext = DIContainer.Container.Resolve<PresetControlViewModel>();
        }
    }
}
