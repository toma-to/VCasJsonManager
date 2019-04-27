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
using VCasJsonManager.Models.Settings;

namespace VCasJsonManagerTests.Stubs
{
    class AppSettingsStub : IAppSettings
    {
        public string AppDataPath { get; set; }

        public string ConfigJsonPath { get; set; }

        public string ModuleDirectory { get; set; }

        public string VirtualCastExePath { get; set; }

        public string ConfigJsonName => "config.json";

        public string VirtualCastExeName => "VirtualCast.exe";
    }
}
