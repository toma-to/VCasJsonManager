//
// VCasJsonManager
// Copyright 2019 TOMA
// MIT License
//
using System;
using System.ComponentModel;
using System.Threading.Tasks;
using VCasJsonManager.Models;
using VCasJsonManager.Models.Settings;

namespace VCasJsonManager.Services
{
    /// <summary>
    /// config.json情報を管理するインターフェース
    /// </summary>
    public interface IConfigJsonService : INotifyPropertyChanged
    {
        ConfigJson ConfigJson { get; }
        PresetInfo CurrentPreset { get; }
        bool IsBusy { get; }

        event EventHandler<ConfigJsonErrorEventArgs> ErrorOccurred;

        Task DeletePresetAsync(string id);
        Task LoadPresetAsync(string id);
        Task ReadConfigJsonAsync();
        Task SaveNewPresetAsync(string presetName);
        Task SavePresetAsync(string id);
        Task WriteToConfigJsonAsync();
    }
}
