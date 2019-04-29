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

using VCasJsonManager.Services;
namespace VCasJsonManagerTests.Stubs
{
    class ExecutionServiceStub : IExecutionService
    {
#pragma warning disable 0067
        public event EventHandler<ExecutionErrorEventArgs> ExecutionError;
        public event EventHandler VirtualCastLaunched;
#pragma warning restore 0067

        public string RunBrowserUrl;
        public void RunBrowser(string uri)
        {
            RunBrowserUrl = uri;
        }

        public void RunBrowser(Uri uri)
        {
            RunBrowser(uri.ToString());
        }

        public void RunVirtualCast()
        {
            throw new NotImplementedException();
        }
    }
}
