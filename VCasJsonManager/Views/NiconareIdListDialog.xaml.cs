﻿//
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
    /// NiconareIdListDialog.xaml の相互作用ロジック
    /// </summary>
    public partial class NiconareIdListDialog : Window
    {
        public NiconareIdListDialog()
        {
            InitializeComponent();
            DataContext = DIContainer.Container.Resolve<NiconareIdListDialogViewModel>();
        }
    }
}
