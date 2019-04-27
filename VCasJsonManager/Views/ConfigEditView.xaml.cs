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
    /// ConfigEditView.xaml の相互作用ロジック
    /// </summary>
    public partial class ConfigEditView : UserControl
    {
        public ConfigEditView()
        {
            InitializeComponent();
            DataContext = DIContainer.Container.Resolve<ConfigEditViewModel>();
        }
    }
}
