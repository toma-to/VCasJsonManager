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
using VCasJsonManager.Models;
using VCasJsonManager.Services;

namespace VCasJsonManagerTests.Stubs
{
    class ConfigJsonFileServiceStub : IConfigJsonFileService
    {
        public bool FileExist { get; set; }
        public string IsFileExistPath { get; set; }
        public Exception Exception { get; set; }

        public bool IsFileExist(string path)
        {
            IsFileExistPath = path;
            return FileExist;
        }

        public ConfigJson ConfigJson { get; set; }

        public string ReadAsyncPath { get; set; }
        public async Task<ConfigJson> ReadAsync(string path)
        {
            if (Exception != null)
            {
                throw Exception;
            }

            await Task.Yield();
            ReadAsyncPath = path;
            return ConfigJson;
        }

        public String WriteAsyncPath { get; set; }
        public bool MergeUnknown { get; set; }
        public async Task WriteAsync(string path, ConfigJson configJson, bool mergeUnknown)
        {
            if (Exception != null)
            {
                throw Exception;
            }

            await Task.Yield();
            WriteAsyncPath = path;
            ConfigJson = configJson;
            MergeUnknown = mergeUnknown;
        }

        public string DeletePath { get; set; }
        public void DeleteFile(string path)
        {
            if (Exception != null)
            {
                throw Exception;
            }

            DeletePath = path;
        }
    }
}
