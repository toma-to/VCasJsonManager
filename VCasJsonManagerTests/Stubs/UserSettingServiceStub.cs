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
using VCasJsonManager.Services;

namespace VCasJsonManagerTests.Stubs
{
    class UserSettingServiceStub : IUserSettingsService
    {
        public IAppSettings AppSettings { get; set; }

        public UserSettings UserSettings { get; set; }

        public UserSettingServiceStub()
        {
            AppSettings = new AppSettingsStub();
            UserSettings = new UserSettings(AppSettings);
        }

        public bool SaveAsyncCalled { get; set; }
        public async Task SaveAsync()
        {
            await Task.Yield();
            SaveAsyncCalled = true;
        }
    }
}
