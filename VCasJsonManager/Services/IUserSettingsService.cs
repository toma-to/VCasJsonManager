//
// VCasJsonManager
// Copyright 2019 TOMA
// MIT License
//
using System.Threading.Tasks;
using VCasJsonManager.Models.Settings;

namespace VCasJsonManager.Services
{
    /// <summary>
    /// ユーザー設定情報を管理するインターフェース
    /// </summary>
    public interface IUserSettingsService
    {
        IAppSettings AppSettings { get; }
        UserSettings UserSettings { get; }

        Task SaveAsync();
    }
}
