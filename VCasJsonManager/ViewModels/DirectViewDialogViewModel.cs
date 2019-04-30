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
    /// ダイレクトビュー設定画面のViewModel
    /// </summary>
    public class DirectViewDialogViewModel : PropertyChangedMappingViewModelBase
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
        /// AllowDirectView
        /// </summary>
        [DoNotNotify]
        public bool AllowDirectView { get => ConfigJson.AllowDirectView; set => ConfigJson.AllowDirectView = value; }

        /// <summary>
        /// DirectViewMode
        /// </summary>
        [DoNotNotify]
        public bool DirectViewMode { get => ConfigJson.DirectViewMode; set => ConfigJson.DirectViewMode = value; }

        /// <summary>
        /// DirectViewTalk
        /// </summary>
        [DoNotNotify]
        public bool DirectViewTalk { get => ConfigJson.DirectViewTalk; set => ConfigJson.DirectViewTalk = value; }

        /// <summary>
        /// LookingGlass
        /// </summary>
        [DoNotNotify]
        public bool LookingGlass { get => ConfigJson.LookingGlass; set => ConfigJson.LookingGlass = value; }

        /// <summary>
        /// ConfigJsonのイベントリスナ
        /// </summary>
        private PropertyChangedEventListener ConfigJsonListner { get; set; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="configJsonService"></param>
        public DirectViewDialogViewModel(IConfigJsonService configJsonService)
        {
            ConfigJsonService = configJsonService;

            AddMapping(nameof(ConfigJson.AllowDirectView));
            AddMapping(nameof(ConfigJson.DirectViewMode));
            AddMapping(nameof(ConfigJson.DirectViewTalk));
            AddMapping(nameof(ConfigJson.LookingGlass));

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
