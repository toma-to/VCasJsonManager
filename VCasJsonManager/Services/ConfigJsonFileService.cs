//
// VCasJsonManager
// Copyright 2019 TOMA
// MIT License
//
using System.IO;
using System.Text;
using System.Threading.Tasks;
using VCasJsonManager.Models;

namespace VCasJsonManager.Services
{
    /// <summary>
    /// config.jsonファイルのread/writeを行うクラス
    /// </summary>
    public class ConfigJsonFileService : IConfigJsonFileService
    {
        /// <summary>
        /// config.jsonファイルを読み込む
        /// </summary>
        /// <param name="path">config.jsonファイルのパス</param>
        /// <returns>読み込んだ情報</returns>
        /// <exception cref="Newtonsoft.Json.JsonException"></exception>
        public async Task<ConfigJson> ReadAsync(string path)
        {
            using (var reader = new StreamReader(new FileStream(path, FileMode.Open, FileAccess.Read), new UTF8Encoding(false)))
            {
                var json = await reader.ReadToEndAsync();
                return json.ParseConfigJson();
            }
        }

        /// <summary>
        /// config.jsonファイルを保存する
        /// </summary>
        /// <param name="path">保存するパス</param>
        /// <param name="configJson">保存する情報</param>
        /// <param name="mergeUnknown">不明なプロパティをマージするかのフラグ</param>
        /// <returns></returns>
        public async Task WriteAsync(string path, ConfigJson configJson, bool mergeUnknown)
        {
            using (var writer = new StreamWriter(new FileStream(path, FileMode.Create, FileAccess.Write), new UTF8Encoding(false)))
            {
                var jsonString = configJson.SerializeToJson(mergeUnknown);
                await writer.WriteAsync(jsonString);
            }
        }

        /// <summary>
        /// 指定したパスのファイルが存在するかを取得する。
        /// </summary>
        /// <param name="path">パス</param>
        /// <returns>存在する場合true</returns>
        public bool IsFileExist(string path)
        {
            return File.Exists(path);
        }

        /// <summary>
        /// 指定したファイルを削除する。
        /// </summary>
        /// <param name="path">削除するファイルのパス</param>
        public void DeleteFile(string path)
        {
            File.Delete(path);
        }
    }
}
