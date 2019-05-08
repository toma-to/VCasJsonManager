//
// VCasJsonManager
// Copyright 2019 TOMA
// MIT License
//
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;

namespace VCasJsonManager.Services.Impl
{
    /// <summary>
    /// 外部アプリケーションを起動するクラス
    /// </summary>
    public class ExecutionService : IExecutionService
    {
        /// <summary>
        /// 外部アプリケーション起動のエラーを通知するイベント
        /// </summary>
        public event EventHandler<ExecutionErrorEventArgs> ExecutionError;

        /// <summary>
        /// VirtualCastの起動完了を通知するイベント
        /// </summary>
        public event EventHandler VirtualCastLaunched;

        /// <summary>
        /// ユーザー設定情報サービス
        /// </summary>
        private IUserSettingsService UserSettingsService { get; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="userSettingService">ユーザー設定情報サービス</param>
        public ExecutionService(IUserSettingsService userSettingService)
        {
            UserSettingsService = userSettingService;
        }

        /// <summary>
        /// VirtualCastの起動
        /// </summary>
        public void RunVirtualCast()
        {
            try
            {
                Process.Start(UserSettingsService.UserSettings.VirtualCastExePath);
                VirtualCastLaunched?.Invoke(this, EventArgs.Empty);
            }
            catch (Exception e) when (e is Win32Exception || e is IOException)
            {
                Trace.TraceInformation($"バーチャルキャスト起動失敗。{e.Message}:{UserSettingsService.UserSettings.VirtualCastExePath}");
                ExecutionError?.Invoke(this, new ExecutionErrorEventArgs(ExecutionErrorEventArgs.Cause.VirtualCastExe, e, UserSettingsService.UserSettings.VirtualCastExePath));
            }
        }

        /// <summary>
        /// ブラウザの起動
        /// </summary>
        /// <param name="uri">表示するURL</param>
        public void RunBrowser(string uri)
        {
            try
            {
                Process.Start(uri);
            }
            catch (Exception e) when (e is Win32Exception || e is IOException)
            {
                Trace.TraceInformation($"ブラウザ起動失敗。{e.Message}:{uri}");
                ExecutionError?.Invoke(this, new ExecutionErrorEventArgs(ExecutionErrorEventArgs.Cause.WebBrowser, e, uri));
            }
        }

        /// <summary>
        /// ブラウザの起動
        /// </summary>
        /// <param name="uri">表示するURL</param>
        public void RunBrowser(Uri uri)
        {
            RunBrowser(uri.ToString());
        }

    }
}
