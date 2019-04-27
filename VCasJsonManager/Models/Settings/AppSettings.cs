//
// VCasJsonManager
// Copyright 2019 TOMA
// MIT License
//
using System;
using System.Configuration;
using System.IO;
using System.Reflection;

namespace VCasJsonManager.Models.Settings
{
    /// <summary>
    /// アプリケーション設定情報を読み込むクラス
    /// </summary>
    public class AppSettings : IAppSettings
    {
        /// <summary>
        /// モジュールの存在するディレクトリ
        /// </summary>
        public string ModuleDirectory { get; }

        /// <summary>
        /// config.jsonのパス
        /// </summary>
        public string ConfigJsonPath { get; }

        /// <summary>
        /// VirtualCastの実行ファイルのパス
        /// </summary>
        public string VirtualCastExePath { get; }

        /// <summary>
        /// アプリケーションデータディレクトリのパス
        /// </summary>
        public string AppDataPath { get; }

        /// <summary>
        /// config.jsonのファイル名
        /// </summary>
        public string ConfigJsonName { get; } = "config.json";

        /// <summary>
        /// VirtualCast.exeのファイル名
        /// </summary>
        public string VirtualCastExeName { get; } = "VirtualCast.exe";

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public AppSettings()
        {
            ModuleDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            ConfigJsonPath = ConfigurationManager.AppSettings["ConfigJsonPath"];
            if (!ConfigJsonPath.IsAbsolutePath())
            {
                ConfigJsonPath = Path.GetFullPath(Path.Combine(ModuleDirectory, "..", ConfigJsonName));
            }

            VirtualCastExePath = ConfigurationManager.AppSettings["VirtualCastExePath"];
            if (!VirtualCastExePath.IsAbsolutePath())
            {
                VirtualCastExePath = Path.GetFullPath(Path.Combine(ModuleDirectory, "..", VirtualCastExeName));
            }

            AppDataPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "VCasJsonManager");
            Directory.CreateDirectory(AppDataPath);
        }

    }
}
