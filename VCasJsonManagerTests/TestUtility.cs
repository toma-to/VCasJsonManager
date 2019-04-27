//
// VCasJsonManager
// Copyright 2019 TOMA
// MIT License
//
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;

namespace VCasJsonManagerTests
{
    static class TestUtility
    {
        public static async Task CreateTestDataFileAsync(string path, string data, Encoding encoding = null)
        {
            using (var writer = new StreamWriter(new FileStream(path, FileMode.Create, FileAccess.Write), encoding ?? new UTF8Encoding(false)))
            {
                await writer.WriteAsync(data);
            }
        }

        public static async Task<string> ReadResultDataFileAsync(string path, Encoding encoding = null)
        {
            using (var reader = new StreamReader(new FileStream(path, FileMode.Open, FileAccess.Read), encoding ?? new UTF8Encoding(false)))
            {
                return await reader.ReadToEndAsync();
            }
        }

        /// <summary>
        /// 空白文字列の除去
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static string RemoveSpace(this string self)
        {
            return Regex.Replace(self, @"\s", "");
        }
    }
}
