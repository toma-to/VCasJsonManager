//
// VCasJsonManager
// Copyright 2019 TOMA
// MIT License
//
using Livet;
using System;
using System.Windows;
using VCasJsonManager.ViewModels;
using VCasJsonManager.Views;

namespace VCasJsonManager
{
    /// <summary>
    /// App.xaml の相互作用ロジック
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        /// 二重起動抑止
        /// </summary>
        private SimplexApplication Simplex { get; set; }

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            DispatcherHelper.UIDispatcher = Dispatcher;
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);

            Simplex = new SimplexApplication();
            if (Simplex.CheckOtherInstance())
            {
                Simplex.Dispose();
                Environment.Exit(0);
            }

        }

        private void Application_Exit(object sender, ExitEventArgs e)
        {
            DIContainer.Container.Dispose();
            Simplex.Dispose();
        }

        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            var dlg = new ErrorMessageDialog();
            var vm = (ErrorMessageDialogViewModel)dlg.DataContext;

            vm.Message = VCasJsonManager.Properties.Resources.ErrorUnknownFatal;

            if (e.ExceptionObject is Exception exception)
            {
                vm.Detail = $"{exception.Message}\n{exception.GetType().Name}\n{exception.StackTrace}";
            }

            dlg.Owner = MainWindow;
            dlg.ShowDialog();

            Environment.Exit(1);
        }
    }
}
