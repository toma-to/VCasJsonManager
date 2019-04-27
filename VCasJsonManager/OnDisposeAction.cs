//
// VCasJsonManager
// Copyright 2019 TOMA
// MIT License
//
using System;

namespace VCasJsonManager
{
    /// <summary>
    /// Dispose時にアクションを実行するクラス
    /// </summary>
    public class OnDisposeAction : IDisposable
    {
        /// <summary>
        /// Dispose時に実行するアクション
        /// </summary>
        private Action OnDispose { get; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="onDispose">Dispose時に実行するアクション</param>
        public OnDisposeAction(Action onDispose)
        {
            OnDispose = onDispose;
        }

        /// <summary>
        /// Dispose
        /// </summary>
        public void Dispose()
        {
            OnDispose();
        }
    }
}
