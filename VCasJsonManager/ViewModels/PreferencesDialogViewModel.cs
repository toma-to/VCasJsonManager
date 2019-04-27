//
// VCasJsonManager
// Copyright 2019 TOMA
// MIT License
//
using Livet.EventListeners;
using Livet.Messaging.IO;
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
        /// VirtualCastフォルダーパス
        /// </summary>
        [DoNotNotify]
        public string VCasFolderPath { get => UserSettings.VirtualCastFolderPath; set => UserSettings.VirtualCastFolderPath = value; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="userSettingsService"></param>
        public PreferencesDialogViewModel(IUserSettingsService userSettingsService)
        {
            UserSettingsService = userSettingsService;

            AddMapping(nameof(UserSettings.VirtualCastFolderPath), nameof(VCasFolderPath));

            CompositeDisposable.Add(new PropertyChangedEventListener(UserSettings)
            {
                { (_, e) => MapPropertyChanged(e.PropertyName) },
            });
        }

        /// <summary>
        /// バーチャルキャストフォルダ選択
        /// </summary>
        /// <param name="message"></param>
        public void VCasFolderSelected(FolderSelectionMessage message)
        {
            if (!string.IsNullOrWhiteSpace(message.Response))
            {
                VCasFolderPath = message.Response;
            }
        }

        /// <summary>
        /// 設定の保存
        /// </summary>
        public async void SaveSetting()
        {
            await UserSettingsService.SaveAsync();
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}
