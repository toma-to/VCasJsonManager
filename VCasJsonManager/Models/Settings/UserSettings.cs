//
// VCasJsonManager
// Copyright 2019 TOMA
// MIT License
//
using Newtonsoft.Json;
using PropertyChanged;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;

namespace VCasJsonManager.Models.Settings
{
    /// <summary>
    /// ユーザー設定を保持するクラス
    /// </summary>
    public class UserSettings : INotifyPropertyChanged
    {
        /// <summary>
        /// PropertyChangedイベントハンドラ
        /// </summary>
#pragma warning disable 0067
        public event PropertyChangedEventHandler PropertyChanged;
#pragma warning restore 0067

        /// <summary>
        /// アプリケーション設定
        /// </summary>
        [JsonIgnore]
        private IAppSettings AppSettings { get; }

        /// <summary>
        /// config.jsonのパス
        /// </summary>
        [JsonIgnore]
        [DependsOn(nameof(VirtualCastFolderPath))]
        public string ConfigJsonPath
        {
            get
            {
                if (string.IsNullOrEmpty(VirtualCastFolderPath))
                {
                    return AppSettings.ConfigJsonPath;
                }
                else
                {
                    return Path.Combine(VirtualCastFolderPath, AppSettings.ConfigJsonName);
                }
            }
        }

        /// <summary>
        /// VirtualCastの実行ファイルのパス
        /// </summary>
        [JsonIgnore]
        [DependsOn(nameof(VirtualCastFolderPath))]
        public string VirtualCastExePath
        {
            get
            {
                if (string.IsNullOrEmpty(VirtualCastFolderPath))
                {
                    return AppSettings.VirtualCastExePath;
                }
                else
                {
                    return Path.Combine(VirtualCastFolderPath, AppSettings.VirtualCastExeName);
                }
            }
        }

        /// <summary>
        /// VirtualCastフォルダーのパス
        /// </summary>
        public string VirtualCastFolderPath { get; set; }

        /// <summary>
        /// プリセット情報の配列
        /// </summary>
        public ObservableCollection<PresetInfo> PresetInfos { get; set; } = new ObservableCollection<PresetInfo>();

        /// <summary>
        /// Google Driveの画像URLの自動変換設定
        /// </summary>
        public bool ConvertGoogleDriveUri { get; set; } = true;

        /// <summary>
        /// 不明なJSONプロパティの保持設定
        /// </summary>
        public bool MergeUnknownJsonProperty { get; set; } = true;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="appSettings">アプリケーション設定情報</param>
        public UserSettings(IAppSettings appSettings)
        {
            AppSettings = appSettings;
        }

        /// <summary>
        /// コンストラクタ(JSONのデシリアライズ用)
        /// </summary>
        public UserSettings()
        {
            AppSettings = new AppSettings();
        }
    }
}
