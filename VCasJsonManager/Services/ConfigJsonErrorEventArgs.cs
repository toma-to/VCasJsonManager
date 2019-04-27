//
// VCasJsonManager
// Copyright 2019 TOMA
// MIT License
//
using System;

namespace VCasJsonManager.Services
{
    /// <summary>
    /// config.jsonの処理でエラーが発生したことを通知するイベントの引数
    /// </summary>
    public class ConfigJsonErrorEventArgs : EventArgs
    {
        /// <summary>
        /// エラー原因
        /// </summary>
        public enum Cause
        {
            /// <summary>
            /// config.jsonの読み込み失敗
            /// </summary>
            ReadConfigJsonError,

            /// <summary>
            /// config.jsonの書き込み失敗
            /// </summary>
            WriteConfigJsonError,

            /// <summary>
            /// プリセット読み込み失敗
            /// </summary>
            ReadPresetError,

            /// <summary>
            /// プリセット書き込み失敗
            /// </summary>
            WritePresetError,

            /// <summary>
            /// プリセットの新規作成失敗
            /// </summary>
            CreatePresetError,

            /// <summary>
            /// プリセット削除失敗
            /// </summary>
            DeletePresetError,
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
        public ConfigJsonErrorEventArgs(Cause cause, Exception exception, string path)
        {
            ErrorCause = cause;
            Exception = exception;
            Path = path;
        }
    }
}
