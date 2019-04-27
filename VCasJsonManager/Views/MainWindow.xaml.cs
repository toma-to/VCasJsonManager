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
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = DIContainer.Container.Resolve<MainWindowViewModel>();
        }
    }
}
