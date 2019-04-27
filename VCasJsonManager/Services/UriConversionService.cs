//
// VCasJsonManager
// Copyright 2019 TOMA
// MIT License
//
using System;
using System.Linq;
using System.Text.RegularExpressions;

using VCasJsonManager.Models.Settings;

namespace VCasJsonManager.Services
{
    /// <summary>
    /// URIの変換処理を行うクラス
    /// </summary>
    public class UriConversionService : IUriConversionService
    {
        /// <summary>
        /// UserSettings
        /// </summary>
        private UserSettings UserSettings { get; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="userSettingService"></param>
        public UriConversionService(IUserSettingsService userSettingService)
        {
            UserSettings = userSettingService.UserSettings;
        }

        /// <summary>
        /// ニコニ立体ID抽出用Regex
        /// </summary>
        private static Regex Nico3dRegex { get; } = new Regex(@"td(\d+)");

        /// <summary>
        /// ニコニ立体のIDの抽出
        /// </summary>
        /// <param name="url">ニコニ立体ID、またはニコニ立体URL</param>
        /// <returns>ニコニ立体ID。抽出失敗時はnull</returns>
        public int? ExtractNico3dId(string url)
        {
            return ExtractIntId(url, Nico3dRegex, 1);
        }

        /// <summary>
        /// ニコニ立体URLフォーマット
        /// </summary>
        private static string Nico3dFormat { get; } = "https://3d.nicovideo.jp/works/td{0}";

        /// <summary>
        /// ニコニ立体URLの構築
        /// </summary>
        /// <param name="id">ニコニ立体ID</param>
        /// <returns>ニコニ立体URL</returns>
        public string BuildNico3dUrl(int id)
        {
            return string.Format(Nico3dFormat, id);
        }

        /// <summary>
        /// ニコニコ動画ID抽出用Regex
        /// </summary>
        private static Regex NicoVideoRegex { get; } = new Regex(@"(sm\d+)");

        /// <summary>
        /// ニコニコ動画IDの抽出
        /// </summary>
        /// <param name="url">ニコニコ動画ID、またはニコニコ動画URL</param>
        /// <returns>ニコニコ動画ID。抽出失敗時はnull</returns>
        public string ExtractNicoVideoId(string url)
        {
            var match = NicoVideoRegex.Matches(url).OfType<Match>().Where(e => e.Groups.Count >= 2).FirstOrDefault();
            return match?.Groups[1].Value;
        }

        /// <summary>
        /// ニコニコ動画URLフォーマット
        /// </summary>
        private static string NicoVideoFormat { get; } = "https://www.nicovideo.jp/watch/{0}";

        /// <summary>
        /// ニコニコ動画URLの構築
        /// </summary>
        /// <param name="id">ニコニコ動画ID</param>
        /// <returns>ニコニコ動画URL</returns>
        public string BuildNicoViedoUrl(string id)
        {
            return string.Format(NicoVideoFormat, id);
        }

        /// <summary>
        /// マイリストID抽出用Regex
        /// </summary>
        private static Regex MylistRegex { get; } = new Regex(@"mylist/(#/){0,1}(\d+)");

        /// <summary>
        /// マイリストIDの抽出
        /// </summary>
        /// <param name="url">マイリストID、またはマイリストURL</param>
        /// <returns>マイリストID。抽出失敗時はnull</returns>
        public int? ExtractMylistId(string url)
        {
            return ExtractIntId(url, MylistRegex, 2);
        }

        /// <summary>
        /// マイリストURLフォーマット
        /// </summary>
        private static string MylistFormat { get; } = "https://www.nicovideo.jp/mylist/{0}";

        /// <summary>
        /// マイリストURLの構築
        /// </summary>
        /// <param name="id">マイリストID</param>
        /// <returns>マイリストURL</returns>
        public string BuildMylistUrl(int id)
        {
            return string.Format(MylistFormat, id);
        }

        /// <summary>
        /// ニコナレID抽出用Regex
        /// </summary>
        private static Regex NiconareRegex { get; } = new Regex(@"kn(\d+)");

        /// <summary>
        /// ニコナレIDの抽出
        /// </summary>
        /// <param name="url">ニコナレID、またはニコナレURL</param>
        /// <returns>ニコナレID。抽出失敗時はnull</returns>
        public int? ExtractNiconareId(string url)
        {
            return ExtractIntId(url, NiconareRegex, 1);
        }

        /// <summary>
        /// ニコナレURLフォーマット
        /// </summary>
        private static string NiconareFormat { get; } = "https://niconare.nicovideo.jp/watch/kn{0}";

        /// <summary>
        /// ニコナレURLの構築
        /// </summary>
        /// <param name="id">ニコナレID</param>
        /// <returns>ニコナレURL</returns>
        public string BuildNiconareUrl(int id)
        {
            return string.Format(NiconareFormat, id);
        }

        /// <summary>
        /// URL文字列のURIオブジェクトへの変換。同時に、GoogleドライブURL変換処理も行う
        /// </summary>
        /// <param name="input">URL文字列</param>
        /// <returns>URIオブジェクト。変換失敗時はnull</returns>
        public Uri BuildUri(string input)
        {
            if (UserSettings.ConvertGoogleDriveUri)
            {
                // GoogleドライブURLの共有URLを画像直接指定のURLに変換
                input = input.Replace("https://drive.google.com/open?id=", "https://drive.google.com/uc?export=view&id=");
            }

            return input.ToUri();
        }

        /// <summary>
        /// 数値IDの抽出
        /// </summary>
        /// <param name="input">入力値</param>
        /// <param name="regex">抽出用正規表現</param>
        /// <param name="index">正規表現での抽出箇所のインデックス</param>
        /// <returns>ID。抽出失敗時はnull</returns>
        private static int? ExtractIntId(string input, Regex regex, int index)
        {
            if (int.TryParse(input, out var wk))
            {
                return wk;
            }

            var match = regex.Matches(input).OfType<Match>().Where(e => e.Groups.Count > index).FirstOrDefault();
            if (match == null)
            {
                return null;
            }

            if (int.TryParse(match.Groups[index].Value, out var wk2))
            {
                return wk2;
            }

            return null;
        }

    }
}
