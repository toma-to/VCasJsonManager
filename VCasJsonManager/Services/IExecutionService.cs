//
// VCasJsonManager
// Copyright 2019 TOMA
// MIT License
//
using System;

namespace VCasJsonManager.Services
{
    /// <summary>
    /// 外部アプリケーションを起動するインターフェース
    /// </summary>
    public interface IExecutionService
    {
        event EventHandler<ExecutionErrorEventArgs> ExecutionError;
        event EventHandler VirtualCastLaunched;

        void RunBrowser(string uri);
        void RunBrowser(Uri uri);
        void RunVirtualCast();
    }
}
