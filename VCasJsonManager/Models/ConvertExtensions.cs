//
// VCasJsonManager
// Copyright 2019 TOMA
// MIT License
//
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace VCasJsonManager.Models
{
    /// <summary>
    /// モデル関連の変換処理を行う拡張メソッドの定義
    /// </summary>
    public static class ConvertExtensions
    {

        /// <summary>
        /// 文字列の配列をURIの配列に変換する。変換できない要素は、URI配列から取り除かれる。
        /// </summary>
        /// <param name="self">変換元の文字列の配列</param>
        /// <returns>URI配列</returns>
        public static IEnumerable<Uri> ToUriList(this IEnumerable<string> self)
        {
            return self.Select(e => e.ToUri()).Where(e => e != null);
        }

        /// <summary>
        /// 両面画像URL文字列の配列を、両面画像URLオブジェクトの配列に変換する。変換できない要素は、配列から取り除かれる。
        /// </summary>
        /// <param name="self">両面画像URL文字列配列</param>
        /// <returns>両面画像URLオブジェクト配列</returns>
        public static IEnumerable<ConfigJson.DoubleImageUrls> ToDoubleImageUrl(this IEnumerable<IEnumerable<string>> self)
        {
            return self.Select(e =>
            {
                if (!(e?.Any() ?? false))
                {
                    return null;
                }
                if (e.Count() != 2)
                {
                    Trace.WriteLine($"両面画像のURL数が不正({e.Count()}: {e.ElementAt(0)}");
                    return null;
                }
                var uris = e.Select(e1 => e1.ToUri());
                if (uris.Any(e1 => e1 == null))
                {
                    return null;
                }

                return new ConfigJson.DoubleImageUrls(uris.ElementAt(0), uris.ElementAt(1));
            })
            .Where(e => e != null);
        }
    }
}
