//
// VCasJsonManager
// Copyright 2019 TOMA
// MIT License
//
using Livet;
using Livet.EventListeners;
using Livet.Messaging;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using VCasJsonManager.Properties;
using VCasJsonManager.Services;

namespace VCasJsonManager.ViewModels
{
    /// <summary>
    /// メインウィンドウのViewModel
    /// </summary>
    public class MainWindowViewModel : ViewModel, INotifyPropertyChanged
    {
        /// <summary>
        /// ConfigJsonService
        /// </summary>
        private IConfigJsonService ConfigJsonService { get; }

        /// <summary>
        /// ExecutionService
        /// </summary>
        private IExecutionService ExecutionService { get; }

        /// <summary>
        /// 処理中フラグ
        /// </summary>
        public bool IsBusy => ConfigJsonService.IsBusy;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="configJsonService"></param>
        /// <param name="executionService"></param>
        public MainWindowViewModel(IConfigJsonService configJsonService, IExecutionService executionService)
        {
            ConfigJsonService = configJsonService;
            ExecutionService = executionService;

            CompositeDisposable.Add(new PropertyChangedEventListener(ConfigJsonService)
            {
                { nameof(ConfigJsonService.IsBusy), (_, __) => RaisePropertyChanged(nameof(IsBusy)) },
            });

            CompositeDisposable.Add(new EventListener<EventHandler<ConfigJsonErrorEventArgs>>(
                h => ConfigJsonService.ErrorOccurred += h,
                h => ConfigJsonService.ErrorOccurred -= h,
                (_, e) =>
                {
                    if (!ConfigJsonErrorMessageMap.TryGetValue(e.ErrorCause, out var message))
                    {
                        message = Resources.ErrorUnknown;
                    }
                    ShowErrorMessage(message, e.Exception, e.Path);
                }
                ));

            CompositeDisposable.Add(new EventListener<EventHandler<ExecutionErrorEventArgs>>(
                h => ExecutionService.ExecutionError += h,
                h => ExecutionService.ExecutionError -= h,
                (_, e) =>
                {
                    if (!ExecutionErrorMessageMap.TryGetValue(e.ErrorCause, out var message))
                    {
                        message = Resources.ErrorUnknown;
                    }
                    ShowErrorMessage(message, e.Exception, e.Path);
                }
                ));
        }

        /// <summary>
        /// エラーメッセージの表示
        /// </summary>
        /// <param name="message"></param>
        /// <param name="exception"></param>
        /// <param name="path"></param>
        private void ShowErrorMessage(string message, Exception exception, string path)
        {
            string detail = null;
            if (exception != null)
            {
                detail = $"{exception.Message}\n{path}\n{exception.GetType().Name}\n{exception.StackTrace}";
            }

            var vm = new ErrorMessageDialogViewModel()
            {
                Message = message,
                Detail = detail,
            };

            Messenger.Raise(new TransitionMessage(vm, "ErrorDialog"));

        }

        /// <summary>
        /// ConfigJsonServiceのエラーメッセージマップ
        /// </summary>
        private static IReadOnlyDictionary<ConfigJsonErrorEventArgs.Cause, string> ConfigJsonErrorMessageMap { get; }
            = new Dictionary<ConfigJsonErrorEventArgs.Cause, string>()
            {
                { ConfigJsonErrorEventArgs.Cause.ReadConfigJsonError,  Resources.ErrorConfigJsonRead },
                { ConfigJsonErrorEventArgs.Cause.WriteConfigJsonError,  Resources.ErrorConfigJsonWrite },
                { ConfigJsonErrorEventArgs.Cause.ReadPresetError,  Resources.ErrorPresetRead },
                { ConfigJsonErrorEventArgs.Cause.WritePresetError,  Resources.ErrorPresetWrite },
                { ConfigJsonErrorEventArgs.Cause.CreatePresetError,  Resources.ErrorPresetWrite },
                { ConfigJsonErrorEventArgs.Cause.DeletePresetError,  Resources.ErrorPresetDelete },
            };

        /// <summary>
        /// ExecutionSeriveのエラーメッセージマップ
        /// </summary>
        private static IReadOnlyDictionary<ExecutionErrorEventArgs.Cause, string> ExecutionErrorMessageMap { get; }
            = new Dictionary<ExecutionErrorEventArgs.Cause, string>()
            {
                { ExecutionErrorEventArgs.Cause.VirtualCastExe, Resources.ErrorRunVirtualCast },
                { ExecutionErrorEventArgs.Cause.WebBrowser, Resources.ErrorRunBrowser },
            };
    }
}
