//
// VCasJsonManager
// Copyright 2019 TOMA
// MIT License
//
using System;
using System.Diagnostics;
using System.IO;

namespace VCasJsonManager
{
    /// <summary>
    /// 文字列に関する拡張メソッド定義
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// 文字列をURIに変換する。変換できない場合はnullを返す。
        /// </summary>
        /// <param name="self">変換元の文字列</param>
        /// <returns>URI</returns>
        public static Uri ToUri(this string self)
        {
            if (Uri.TryCreate(self, UriKind.Absolute, out var result))
            {
                return result;
            }
            else
            {
                Trace.WriteLine($"不正なURL:{self}");
                return null;
            }
        }

        /// <summary>
        /// 文字列が絶対パスを示しているかを判定する
        /// </summary>
        /// <param name="self">判定対象</param>
        /// <returns>絶対パスの場合true</returns>
        public static bool IsAbsolutePath(this string self)
        {
            try
            {
                return (!string.IsNullOrEmpty(self) && Path.IsPathRooted(self));
            }
            catch (ArgumentException e)
            {
                Trace.WriteLine($"不正なファイルパス:{self}:{e.Message}");
                return false;
            }
        }
    }
}
