//
// VCasJsonManager
// Copyright 2019 TOMA
// MIT License
//
using Newtonsoft.Json;
using PropertyChanged;
using System.Collections.ObjectModel;
using System.ComponentModel;

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
        [DependsOn(nameof(ConfigJsonFilePath))]
        public string ConfigJsonPath
        {
            get
            {
                if (string.IsNullOrEmpty(ConfigJsonFilePath))
                {
                    return AppSettings.ConfigJsonPath;
                }
                else
                {
                    return ConfigJsonFilePath;
                }
            }
        }

        /// <summary>
        /// VirtualCastの実行ファイルのパス
        /// </summary>
        [JsonIgnore]
        [DependsOn(nameof(RunVirtualCastPath))]
        public string VirtualCastExePath
        {
            get
            {
                if (string.IsNullOrEmpty(RunVirtualCastPath))
                {
                    return AppSettings.VirtualCastExePath;
                }
                else
                {
                    return RunVirtualCastPath;
                }
            }
        }

        /// <summary>
        /// VirtualCast起動パス
        /// </summary>
        public string RunVirtualCastPath { get; set; }

        /// <summary>
        /// config.jsonファイルのパス
        /// </summary>
        public string ConfigJsonFilePath { get; set; }

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
        /// VirtualCast起動後にアプリケーションを終了する設定
        /// </summary>
        public bool ExitWhenVirtulCastLaunched { get; set; } = false;

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
