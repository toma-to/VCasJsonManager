//
// VCasJsonManager
// Copyright 2019 TOMA
// MIT License
//
using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using VCasJsonManager.Models.Settings;

namespace VCasJsonManager.Services
{
    /// <summary>
    /// ユーザー設定情報を管理するクラス
    /// </summary>
    public class UserSettingsService : IUserSettingsService
    {
        /// <summary>
        /// アプリケーション設定情報
        /// </summary>
        public IAppSettings AppSettings { get; }

        /// <summary>
        /// ユーザー設定情報
        /// </summary>
        public UserSettings UserSettings { get; }

        /// <summary>
        /// ユーザー設定ファイルパス
        /// </summary>
        private string FilePath => Path.Combine(AppSettings.AppDataPath, "setting.json");

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public UserSettingsService(IAppSettings appSettings)
        {
            AppSettings = appSettings;

            try
            {
                if (!File.Exists(FilePath))
                {
                    UserSettings = new UserSettings(AppSettings);
                    return;
                }
                using (var reader = new StreamReader(new FileStream(FilePath, FileMode.Open, FileAccess.Read), new UTF8Encoding(false)))
                {
                    var json = reader.ReadToEnd();
                    UserSettings = JsonConvert.DeserializeObject<UserSettings>(json);
                }
            }
            catch (Exception e) when (e is IOException || e is JsonException)
            {
                Trace.TraceInformation($"設定ファイルの読み込みに失敗しました。{e.Message}:{FilePath}");
                UserSettings = new UserSettings(AppSettings);
            }
        }

        /// <summary>
        /// ユーザー設定の保存
        /// </summary>
        /// <returns></returns>
        public async Task SaveAsync()
        {
            try
            {
                using (var writer = new StreamWriter(new FileStream(FilePath, FileMode.Create, FileAccess.Write), new UTF8Encoding(false)))
                {
                    var json = JsonConvert.SerializeObject(UserSettings);
                    await writer.WriteAsync(json);
                }
            }
            catch (Exception e)
            {
                Trace.TraceInformation($"設定ファイルの保存に失敗しました。{e.Message}:{FilePath}");
                throw;
            }

        }
    }
}
