//
// VCasJsonManager
// Copyright 2019 TOMA
// MIT License
//
namespace VCasJsonManager.Models.Settings
{
    /// <summary>
    /// アプリケーション設定情報のインターフェース
    /// </summary>
    public interface IAppSettings
    {
        string AppDataPath { get; }
        string ConfigJsonPath { get; }
        string ModuleDirectory { get; }
        string VirtualCastExePath { get; }
        string ConfigJsonName { get; }
        string VirtualCastExeName { get; }
    }
}
