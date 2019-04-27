//
// VCasJsonManager
// Copyright 2019 TOMA
// MIT License
//
using Livet;
using Livet.EventListeners;
using Livet.Messaging;
using System.Collections.ObjectModel;
using VCasJsonManager.Models;
using VCasJsonManager.Models.Settings;
using VCasJsonManager.Services;

namespace VCasJsonManager.ViewModels
{
    /// <summary>
    /// プリセット管理画面のViewModel
    /// </summary>
    public class PresetControlViewModel : ViewModel
    {

        /// <summary>
        /// ConfigJsonService
        /// </summary>
        private IConfigJsonService ConfigJsonService { get; }

        /// <summary>
        /// UserSettingService
        /// </summary>
        private IUserSettingsService UserSettingsService { get; }

        /// <summary>
        /// ExecutionService
        /// </summary>
        private IExecutionService ExecutionService { get; }

        /// <summary>
        /// プリセット情報
        /// </summary>
        public ObservableCollection<PresetInfo> Presets => UserSettingsService.UserSettings.PresetInfos;

        /// <summary>
        /// 現在選択中のプリセット
        /// </summary>
        public PresetInfo CurrentPreset => ConfigJsonService.CurrentPreset;

        /// <summary>
        /// リストで選択されたプリセット
        /// </summary>
        public PresetInfo SelectedPreset { get; set; }

        /// <summary>
        /// 変更有無
        /// </summary>
        public bool IsChanged => ConfigJsonService.ConfigJson.IsChanged;

        /// <summary>
        /// プリセット保存可否
        /// </summary>
        public bool SaveEnable => CurrentPreset != null && IsChanged;

        /// <summary>
        /// プリセット削除可否
        /// </summary>
        public bool DeleteEnable => CurrentPreset != null;

        /// <summary>
        /// ConfigJsonのイベントリスナ
        /// </summary>
        private PropertyChangedEventListener ConfigJsonEventListener { get; set; } = null;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="configJsonService"></param>
        /// <param name="userSettingsService"></param>
        /// <param name="executionService"></param>
        public PresetControlViewModel(IConfigJsonService configJsonService, IUserSettingsService userSettingsService, IExecutionService executionService)
        {
            ConfigJsonService = configJsonService;
            UserSettingsService = userSettingsService;
            ExecutionService = executionService;

            // モデルのプロパティ更新イベントリスナ登録
            CompositeDisposable.Add(new PropertyChangedEventListener(UserSettingsService.UserSettings)
            {
                { nameof(UserSettings.PresetInfos), (_, __) => RaisePropertyChanged(nameof(Presets)) },
            });
            CompositeDisposable.Add(new PropertyChangedEventListener(ConfigJsonService)
            {
                {
                    nameof(IConfigJsonService.CurrentPreset),
                    (_, __) =>
                    {
                        RaisePropertyChanged(nameof(CurrentPreset));
                        RaisePropertyChanged(nameof(SaveEnable));
                        RaisePropertyChanged(nameof(DeleteEnable));
                    }
                },
                {
                    nameof(IConfigJsonService.ConfigJson), (_, __) => SetConfigJsonEventListenr()
                }
            });
            SetConfigJsonEventListenr();
        }

        /// <summary>
        /// ConfigJsonへのイベントリスナ登録
        /// </summary>
        private void SetConfigJsonEventListenr()
        {
            if (ConfigJsonEventListener != null)
            {
                CompositeDisposable.Remove(ConfigJsonEventListener);
            }

            ConfigJsonEventListener = new PropertyChangedEventListener(ConfigJsonService.ConfigJson)
            {
                {
                    nameof(ConfigJson.IsChanged),
                    (_, __) =>
                    {
                        RaisePropertyChanged(nameof(IsChanged));
                        RaisePropertyChanged(nameof(SaveEnable));
                    }
                },
            };
            CompositeDisposable.Add(ConfigJsonEventListener);
        }

        /// <summary>
        /// 新規プリセット保存
        /// </summary>
        public async void SaveAs()
        {
            var dlgVm = new NewPresetDialogViewModel();
            Messenger.Raise(new TransitionMessage(dlgVm, "NewPresetDialog"));

            if (dlgVm.IsOk)
            {
                await ConfigJsonService.SaveNewPresetAsync(dlgVm.PresetName);
            }
        }

        /// <summary>
        /// プリセットの上書き保存
        /// </summary>
        public async void Save()
        {
            if (CurrentPreset == null)
            {
                return;
            }

            await ConfigJsonService.SavePresetAsync(CurrentPreset.Id);
        }

        /// <summary>
        /// プリセットの削除
        /// </summary>
        public async void Delete()
        {
            if (CurrentPreset == null)
            {
                return;
            }
            await ConfigJsonService.DeletePresetAsync(CurrentPreset.Id);
        }

        /// <summary>
        /// プリセットのロード
        /// </summary>
        public async void LoadPreset()
        {
            if (SelectedPreset == null)
            {
                return;
            }

            await ConfigJsonService.LoadPresetAsync(SelectedPreset.Id);

            SelectedPreset = null;
        }

        /// <summary>
        /// config.json読み込み
        /// </summary>
        public async void ReadConfigJson()
        {
            await ConfigJsonService.ReadConfigJsonAsync();
        }

        /// <summary>
        /// config.json更新
        /// </summary>
        public async void UpdateConfigJson()
        {
            await ConfigJsonService.WriteToConfigJsonAsync();
        }

        /// <summary>
        /// config.jsonを更新してバーチャルキャスト起動
        /// </summary>
        public async void RunVirtualCast()
        {
            await ConfigJsonService.WriteToConfigJsonAsync();
            ExecutionService.RunVirtualCast();
        }

        /// <summary>
        /// 初期化処理
        /// </summary>
        public async void Initialize()
        {
            await ConfigJsonService.ReadConfigJsonAsync();
        }

    }
}
