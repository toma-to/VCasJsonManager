//
// VCasJsonManager
// Copyright 2019 TOMA
// MIT License
//
using System;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Ipc;
using System.Threading;
using System.Windows;

namespace VCasJsonManager
{
    /// <summary>
    /// アプリケーションの二重起動抑止処理
    /// </summary>
    public class SimplexApplication : MarshalByRefObject, IDisposable
    {
        /// <summary>
        /// プロセス間通信用の識別子
        /// </summary>
        private static string Id { get; } = "4FC63747-BE7D-45E0-9F8C-766E5F86C71D";

        /// <summary>
        /// ハンドラ名
        /// </summary>
        private static string HandlerName { get; } = "Handler";

        /// <summary>
        /// IPCチャネル
        /// </summary>
        private IChannel Channel { get; set; }

        /// <summary>
        /// Mutex
        /// </summary>
        private Mutex Mutex { get; set; }

        /// <summary>
        /// 別のインスタンスが起動済みであるかのチェック
        /// </summary>
        /// <returns>別のインスタンスが起動済みの場合true</returns>
        public bool CheckOtherInstance()
        {
            Mutex = new Mutex(false, $"{Id}_Mutex", out var isNew);
            if (isNew)
            {
                // 最初のインスタンスの場合の処理
                Channel = new IpcServerChannel(Id);
                ChannelServices.RegisterChannel(Channel, true);
                RemotingServices.Marshal(this, HandlerName, typeof(SimplexApplication));

                return false;
            }
            else
            {
                // 別インスタンス起動済みの場合の処理
                Channel = new IpcClientChannel();
                ChannelServices.RegisterChannel(Channel, true);
                (Activator.GetObject(typeof(SimplexApplication), $"ipc://{Id}/{HandlerName}") as SimplexApplication).HandleActivation();
                return true;
            }
        }

        /// <summary>
        /// アクティブ化通知処理
        /// </summary>
        public void HandleActivation()
        {
            App.Current?.Dispatcher?.Invoke(() =>
            {
                var window = App.Current?.MainWindow;
                if (window != null)
                {
                    window.WindowState = WindowState.Normal;
                    window.Activate();
                }
            });
        }

        public override object InitializeLifetimeService() => null;

        #region IDisposable Support
        private bool disposedValue = false; // 重複する呼び出しを検出するには

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    Mutex?.Dispose();
                }

                if (Channel != null)
                {
                    ChannelServices.UnregisterChannel(Channel);
                }

                disposedValue = true;
            }
        }

        ~SimplexApplication()
        {
            // このコードを変更しないでください。クリーンアップ コードを上の Dispose(bool disposing) に記述します。
            Dispose(false);
        }

        public void Dispose()
        {
            // このコードを変更しないでください。クリーンアップ コードを上の Dispose(bool disposing) に記述します。
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
