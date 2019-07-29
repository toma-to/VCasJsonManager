//
// VCasJsonManager
// Copyright 2019 TOMA
// MIT License
//
using Livet.EventListeners;
using Livet.Messaging.IO;
using System.Linq;
using PropertyChanged;
using VCasJsonManager.Models.Settings;
using VCasJsonManager.Services;

namespace VCasJsonManager.ViewModels
{
    /// <summary>
    /// 動作設定画面のViewModel
    /// </summary>
    public class PreferencesDialogViewModel : PropertyChangedMappingViewModelBase
    {
        /// <summary>
        /// UserSettingsService
        /// </summary>
        private IUserSettingsService UserSettingsService { get; }

        /// <summary>
        /// UserSettings
        /// </summary>
        private UserSettings UserSettings => UserSettingsService.UserSettings;

        /// <summary>
        /// VirtualCast起動パス
        /// </summary>
        [DoNotNotify]
        public string RunVcasPath { get => UserSettings.RunVirtualCastPath; set => UserSettings.RunVirtualCastPath = value; }

        /// <summary>
        /// config.jsonファイルパス
        /// </summary>
        [DoNotNotify]
        public string ConfigJsonPath { get => UserSettings.ConfigJsonFilePath; set => UserSettings.ConfigJsonFilePath = value; }

        /// <summary>
        /// バーチャルキャスト起動時に終了
        /// </summary>
        [DoNotNotify]
        public bool ExitWhenVCasLaunched
        {
            get => UserSettings.ExitWhenVirtulCastLaunched;
            set => UserSettings.ExitWhenVirtulCastLaunched = value;
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="userSettingsService"></param>
        public PreferencesDialogViewModel(IUserSettingsService userSettingsService)
        {
            UserSettingsService = userSettingsService;

            AddMapping(nameof(UserSettings.RunVirtualCastPath), nameof(RunVcasPath));
            AddMapping(nameof(UserSettings.ConfigJsonFilePath), nameof(ConfigJsonPath));
            AddMapping(nameof(UserSettings.ExitWhenVirtulCastLaunched), nameof(ExitWhenVCasLaunched));

            CompositeDisposable.Add(new PropertyChangedEventListener(UserSettings)
            {
                { (_, e) => MapPropertyChanged(e.PropertyName) },
            });
        }

        /// <summary>
        /// バーチャルキャストファイル選択
        /// </summary>
        /// <param name="message"></param>
        public void VCasExeSelected(OpeningFileSelectionMessage message)
        {
            if (!string.IsNullOrWhiteSpace(message.Response?.FirstOrDefault()))
            {
                RunVcasPath = message.Response.FirstOrDefault();
            }
        }

        /// <summary>
        /// config.jsonファイル選択
        /// </summary>
        /// <param name="message"></param>
        public void ConfigJsonSelected(OpeningFileSelectionMessage message)
        {
            if (!string.IsNullOrWhiteSpace(message.Response?.FirstOrDefault()))
            {
                ConfigJsonPath = message.Response.FirstOrDefault();
            }
        }

        /// <summary>
        /// 設定の保存
        /// </summary>
        public async void SaveSetting()
        {
            await UserSettingsService.SaveAsync();
        }
    }
}
