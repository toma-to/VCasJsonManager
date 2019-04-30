//
// VCasJsonManager
// Copyright 2019 TOMA
// MIT License
//
using Livet.EventListeners;
using PropertyChanged;
using System;
using VCasJsonManager.Models;
using VCasJsonManager.Services;

namespace VCasJsonManager.ViewModels
{
    /// <summary>
    /// デバッグ設定画面のViewModel
    /// </summary>
    public class VrDebugDialogViewModel : PropertyChangedMappingViewModelBase
    {
        /// <summary>
        /// ConfigJsonService
        /// </summary>
        private IConfigJsonService ConfigJsonService { get; }

        /// <summary>
        /// ConfigJson
        /// </summary>
        private ConfigJson ConfigJson => ConfigJsonService.ConfigJson;

        /// <summary>
        /// ScriptVrDebug
        /// </summary>
        [DoNotNotify]
        public bool ScriptVrDebug { get => ConfigJson.ScriptVrDebug; set => ConfigJson.ScriptVrDebug = value; }

        /// <summary>
        /// ScriptWebSocketConsolePort
        /// </summary>
        [DoNotNotify]
        public string ScriptWebSocketConsolePort
        {
            get => ConfigJson.ScriptWebSocketConsolePort?.ToString();
            set
            {
                if (int.TryParse(value, out var wk))
                {
                    ConfigJson.ScriptWebSocketConsolePort = wk;
                }
                else
                {
                    ConfigJson.ScriptWebSocketConsolePort = null;
                }
            }
        }

        /// <summary>
        /// ScriptMoonsharpDebuggerPort
        /// </summary>
        [DoNotNotify]
        public string ScriptMoonsharpDebuggerPort
        {
            get => ConfigJson.ScriptMoonsharpDebuggerPort?.ToString();
            set
            {
                if (int.TryParse(value, out var wk))
                {
                    ConfigJson.ScriptMoonsharpDebuggerPort = wk;
                }
                else
                {
                    ConfigJson.ScriptMoonsharpDebuggerPort = null;
                }
            }
        }

        /// <summary>
        /// ConfigJsonのイベントリスナ
        /// </summary>
        private PropertyChangedEventListener ConfigJsonListner { get; set; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="configJsonService"></param>
        public VrDebugDialogViewModel(IConfigJsonService configJsonService)
        {
            ConfigJsonService = configJsonService;

            AddMapping(nameof(ConfigJson.ScriptVrDebug));
            AddMapping(nameof(ConfigJson.ScriptWebSocketConsolePort));
            AddMapping(nameof(ConfigJson.ScriptMoonsharpDebuggerPort));

            Action addConfigJsonListner = () =>
            {
                ConfigJsonListner = new PropertyChangedEventListener(ConfigJson)
                    {
                        { (_, e) => MapPropertyChanged(e.PropertyName) },
                    };
                CompositeDisposable.Add(ConfigJsonListner);
            };

            CompositeDisposable.Add(new PropertyChangedEventListener(ConfigJsonService)
            {
                { nameof(IConfigJsonService.ConfigJson), (_, __) =>
                    {
                        RaiseAllPropertyChanged();
                        CompositeDisposable.Remove(ConfigJsonListner);
                        ConfigJsonListner.Dispose();
                        addConfigJsonListner();
                    }
                },
            });
            addConfigJsonListner();
        }
    }
}
