//
// VCasJsonManager
// Copyright 2019 TOMA
// MIT License
//
using System;

namespace VCasJsonManager.Services
{
    /// <summary>
    /// 外部アプリケーション起動エラーを通知するイベント引数
    /// </summary>
    public class ExecutionErrorEventArgs : EventArgs
    {
        /// <summary>
        /// エラー原因
        /// </summary>
        public enum Cause
        {
            /// <summary>
            /// VirtualCast.exeの起動
            /// </summary>
            VirtualCastExe,

            /// <summary>
            /// Webブラウザの起動
            /// </summary>
            WebBrowser,
        }

        /// <summary>
        /// エラー原因
        /// </summary>
        public Cause ErrorCause { get; }

        /// <summary>
        /// 例外が発生していた場合の例外情報
        /// </summary>
        public Exception Exception { get; }

        /// <summary>
        /// 例外に関係するファイルパス
        /// </summary>
        public string Path { get; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="cause">エラー原因</param>
        /// <param name="exception">例外</param>
        /// <param name="path">ファイルパス</param>
        public ExecutionErrorEventArgs(Cause cause, Exception exception, string path)
        {
            ErrorCause = cause;
            Exception = exception;
            Path = path;
        }
    }
}
