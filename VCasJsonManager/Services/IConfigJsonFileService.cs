//
// VCasJsonManager
// Copyright 2019 TOMA
// MIT License
//
using System.Threading.Tasks;
using VCasJsonManager.Models;

namespace VCasJsonManager.Services
{
    /// <summary>
    /// config.jsonファイルのread/writeを行うインターフェース
    /// </summary>
    public interface IConfigJsonFileService
    {
        bool IsFileExist(string path);
        Task<ConfigJson> ReadAsync(string path);
        Task WriteAsync(string path, ConfigJson configJson, bool mergeUnknown);
        void DeleteFile(string path);
    }
}
