//
// VCasJsonManager
// Copyright 2019 TOMA
// MIT License
//
using Newtonsoft.Json;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using VCasJsonManager.Models;
using VCasJsonManager.Models.Settings;

namespace VCasJsonManager.Services
{
    /// <summary>
    /// config.json情報を管理するクラス
    /// </summary>
    public class ConfigJsonService : INotifyPropertyChanged, IConfigJsonService
    {
        /// <summary>
        /// PropertyChangedイベントハンドラ
        /// </summary>
#pragma warning disable 0067
        public event PropertyChangedEventHandler PropertyChanged;
#pragma warning restore 0067

        /// <summary>
        /// エラー発生イベント
        /// </summary>
        public event EventHandler<ConfigJsonErrorEventArgs> ErrorOccurred;

        /// <summary>
        /// ユーザー設定情報サービス
        /// </summary>
        private IUserSettingsService UserSettingsService { get; }

        /// <summary>
        /// ユーザー設定情報
        /// </summary>
        private UserSettings UserSettings { get; }

        /// <summary>
        /// ファイルアクセスサービス
        /// </summary>
        private IConfigJsonFileService FileService { get; }

        /// <summary>
        /// config.json情報
        /// </summary>
        public ConfigJson ConfigJson { get; private set; }

        /// <summary>
        /// 処理中フラグ
        /// </summary>
        public bool IsBusy { get; private set; }

        /// <summary>
        /// 現在読み込まれているプリセット
        /// </summary>
        public PresetInfo CurrentPreset { get; private set; }

        /// <summary>
        /// プリセット名からファイル名に使用できない文字を除去するための正規表現
        /// </summary>
        private static Regex PresetNameToFileNameRegex { get; }

        /// <summary>
        /// クラス初期化
        /// </summary>
        static ConfigJsonService()
        {
            var invalidFileCharas = string.Join(string.Empty, Path.GetInvalidFileNameChars());
            var pattern = $"[{Regex.Escape(invalidFileCharas)}]";
            PresetNameToFileNameRegex = new Regex(pattern);
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="userSettingService">ユーザー設定情報サービス</param>
        /// <param name="fileService">ファイルサービス</param>
        public ConfigJsonService(IUserSettingsService userSettingService, IConfigJsonFileService fileService)
        {
            UserSettingsService = userSettingService;
            UserSettings = userSettingService.UserSettings;
            FileService = fileService;
            ConfigJson = new ConfigJson();
            IsBusy = false;
        }

        /// <summary>
        /// VirtualCastのconfig.jsonを読み込む
        /// </summary>
        /// <returns></returns>
        public async Task ReadConfigJsonAsync()
        {
            try
            {
                if (FileService.IsFileExist(UserSettings.ConfigJsonPath))
                {
                    IsBusy = true;
                    using (new OnDisposeAction(() => IsBusy = false))
                    {
                        ConfigJson = await FileService.ReadAsync(UserSettings.ConfigJsonPath);
                        CurrentPreset = null;
                    }
                }
            }
            catch (Exception e) when (e is IOException || e is JsonException)
            {
                Trace.TraceInformation($"config.json読み込み失敗。{UserSettings.ConfigJsonPath}:{e.Message}");
                ErrorOccurred?.Invoke(this, new ConfigJsonErrorEventArgs(ConfigJsonErrorEventArgs.Cause.ReadConfigJsonError, e, UserSettings.ConfigJsonPath));
            }
        }

        /// <summary>
        /// VirtualCastのconfg.jsonに書き込む
        /// </summary>
        /// <returns></returns>
        public async Task WriteToConfigJsonAsync()
        {
            try
            {
                IsBusy = true;
                using (new OnDisposeAction(() => IsBusy = false))
                {
                    await FileService.WriteAsync(UserSettings.ConfigJsonPath, ConfigJson, UserSettings.MergeUnknownJsonProperty);
                }
            }
            catch (Exception e) when (e is IOException || e is JsonException)
            {
                Trace.TraceInformation($"config.json書き込み失敗。{UserSettings.ConfigJsonPath}:{e.Message}");
                ErrorOccurred?.Invoke(this, new ConfigJsonErrorEventArgs(ConfigJsonErrorEventArgs.Cause.WriteConfigJsonError, e, UserSettings.ConfigJsonPath));
            }
        }

        /// <summary>
        /// 新規のプリセット情報として保存
        /// </summary>
        /// <param name="presetName"></param>
        /// <returns></returns>
        public async Task SaveNewPresetAsync(string presetName)
        {
            IsBusy = true;
            using (new OnDisposeAction(() => IsBusy = false))
            {
                var id = Guid.NewGuid().ToString("D");
                var fileName = $"{PresetNameToFileNameRegex.Replace(presetName, "")}_{id}.json";
                var filePath = Path.Combine(UserSettingsService.AppSettings.AppDataPath, fileName);
                try
                {

                    await FileService.WriteAsync(filePath, ConfigJson, UserSettings.MergeUnknownJsonProperty);

                    var preset = new PresetInfo()
                    {
                        Id = id,
                        Name = presetName,
                        FileName = fileName,
                    };
                    UserSettings.PresetInfos.Add(preset);

                    CurrentPreset = preset;
                    await UserSettingsService.SaveAsync();

                    ConfigJson.IsChanged = false;
                }
                catch (Exception e) when (e is IOException || e is JsonException)
                {
                    Trace.TraceInformation($"プリセット{presetName}作成失敗。{filePath}:{e.Message}");
                    ErrorOccurred?.Invoke(this, new ConfigJsonErrorEventArgs(ConfigJsonErrorEventArgs.Cause.CreatePresetError, e, filePath));
                }
            }
        }

        /// <summary>
        /// プリセット情報の上書き保存
        /// </summary>
        /// <param name="id">保存するプリセットID</param>
        /// <returns></returns>
        public async Task SavePresetAsync(string id)
        {
            IsBusy = true;
            using (new OnDisposeAction(() => IsBusy = false))
            {
                var preset = UserSettings.PresetInfos.Where(e => e.Id == id).FirstOrDefault();
                if (preset == null)
                {
                    Trace.TraceInformation($"プリセットID不正：{id}");
                    ErrorOccurred?.Invoke(this,
                        new ConfigJsonErrorEventArgs(ConfigJsonErrorEventArgs.Cause.WritePresetError, new FileNotFoundException("プリセットが見つかりません。"), id));
                    return;
                }
                var path = Path.Combine(UserSettingsService.AppSettings.AppDataPath, preset.FileName);

                try
                {
                    await FileService.WriteAsync(path, ConfigJson, UserSettings.MergeUnknownJsonProperty);
                    ConfigJson.IsChanged = false;
                }
                catch (Exception e) when (e is IOException || e is JsonException)
                {
                    Trace.TraceInformation($"プリセット[{preset.Id}]作成失敗。{path}:{e.Message}");
                    ErrorOccurred?.Invoke(this, new ConfigJsonErrorEventArgs(ConfigJsonErrorEventArgs.Cause.WritePresetError, e, path));
                }

            }
        }

        /// <summary>
        /// プリセット情報の読み込み
        /// </summary>
        /// <param name="id">読み込むプリセットID</param>
        /// <returns></returns>
        public async Task LoadPresetAsync(string id)
        {
            IsBusy = true;
            using (new OnDisposeAction(() => IsBusy = false))
            {
                var preset = UserSettings.PresetInfos.Where(e => e.Id == id).FirstOrDefault();
                if (preset == null)
                {
                    Trace.TraceInformation($"プリセットID不正：{id}");
                    ErrorOccurred?.Invoke(this,
                        new ConfigJsonErrorEventArgs(ConfigJsonErrorEventArgs.Cause.ReadPresetError, new FileNotFoundException("プリセットが見つかりません。"), id));
                    return;
                }
                var path = Path.Combine(UserSettingsService.AppSettings.AppDataPath, preset.FileName);

                try
                {
                    ConfigJson = await FileService.ReadAsync(path);
                    CurrentPreset = preset;
                }
                catch (Exception e) when (e is IOException || e is JsonException)
                {
                    Trace.TraceInformation($"プリセット[{preset.Id}]読み込み失敗。{path}:{e.Message}");
                    ErrorOccurred?.Invoke(this, new ConfigJsonErrorEventArgs(ConfigJsonErrorEventArgs.Cause.ReadPresetError, e, path));
                    ConfigJson = new ConfigJson();
                    CurrentPreset = preset;
                }

            }
        }

        /// <summary>
        /// プリセット情報の削除
        /// </summary>
        /// <param name="id"></param>
        public async Task DeletePresetAsync(string id)
        {
            var preset = UserSettings.PresetInfos.Where(e => e.Id == id).FirstOrDefault();
            if (preset == null)
            {
                return;
            }
            var path = Path.Combine(UserSettingsService.AppSettings.AppDataPath, preset.FileName);

            try
            {
                FileService.DeleteFile(path);
                UserSettings.PresetInfos.Remove(preset);
                await UserSettingsService.SaveAsync();
                if (CurrentPreset.Id == preset.Id)
                {
                    CurrentPreset = null;
                }
            }
            catch (IOException e)
            {
                Trace.TraceInformation($"プリセット[{preset.Id}]削除失敗。{path}:{e.Message}");
                ErrorOccurred?.Invoke(this, new ConfigJsonErrorEventArgs(ConfigJsonErrorEventArgs.Cause.DeletePresetError, e, path));
            }

        }

        /// <summary>
        /// JSONファイルのインポート
        /// </summary>
        /// <param name="path">インポートするファイルのパス</param>
        /// <returns></returns>
        public async Task ImportJsonAsync(string path)
        {
            IsBusy = true;
            using (new OnDisposeAction(() => IsBusy = false))
            {
                try
                {
                    ConfigJson = await FileService.ReadAsync(path);
                    CurrentPreset = null;
                }
                catch (Exception e) when (e is IOException || e is ArgumentException || e is UnauthorizedAccessException || e is SecurityException)
                {
                    Trace.TraceInformation($"インポートファイル読み込み失敗。{path}:{e.Message}");
                    ErrorOccurred?.Invoke(this, new ConfigJsonErrorEventArgs(ConfigJsonErrorEventArgs.Cause.ImportJsonOpenError, e, path));
                }
                catch (JsonException e)
                {
                    Trace.TraceInformation($"インポートファイルフォーマット不正。{path}:{e.Message}");
                    ErrorOccurred?.Invoke(this, new ConfigJsonErrorEventArgs(ConfigJsonErrorEventArgs.Cause.ImportJsonBadFormat, e, path));
                }
            }
        }

        /// <summary>
        /// JSONファイルへのエクスポート
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public async Task ExportJsonAsync(string path)
        {
            IsBusy = true;
            using (new OnDisposeAction(() => IsBusy = false))
            {
                try
                {
                    await FileService.WriteAsync(path, ConfigJson, UserSettings.MergeUnknownJsonProperty);
                }
                catch (Exception e)
                    when (e is IOException || e is JsonException || e is ArgumentException || e is UnauthorizedAccessException || e is SecurityException)
                {
                    Trace.TraceInformation($"エクスポート失敗。{path}:{e.Message}");
                    ErrorOccurred?.Invoke(this, new ConfigJsonErrorEventArgs(ConfigJsonErrorEventArgs.Cause.ExoprtJsonError, e, path));
                }
            }
        }
    }

}
