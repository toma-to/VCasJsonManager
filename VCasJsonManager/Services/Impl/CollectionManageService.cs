//
// VCasJsonManager
// Copyright 2019 TOMA
// MIT License
//
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using VCasJsonManager.Models;

namespace VCasJsonManager.Services.Impl
{
    /// <summary>
    /// ConfigJsonオブジェクト内のコレクションを管理するための抽象基本クラス
    /// </summary>
    public abstract class CollectionManageService<T> : ICollectionManageService<T>
    {
        /// <summary>
        /// PropertyChangedイベントハンドラ
        /// </summary>
#pragma warning disable 0067
        public event PropertyChangedEventHandler PropertyChanged;
#pragma warning restore 0067

        /// <summary>
        /// エラー変更通知
        /// </summary>
        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        /// <summary>
        /// このクラスで管理するコレクション
        /// </summary>
        public ObservableCollection<T> Collection => SelectCollectionFunc(ConfigJson);

        /// <summary>
        /// ConfigJson
        /// </summary>
        public ConfigJson ConfigJson { get; }

        /// <summary>
        /// 入力値
        /// </summary>
        public string InputValue { get; set; }

        /// <summary>
        /// ConfigJsonオブジェクトから管理対象のコレクションを取得するファンクション
        /// </summary>
        protected Func<ConfigJson, ObservableCollection<T>> SelectCollectionFunc { get; }

        /// <summary>
        /// URL変換サービス
        /// </summary>
        protected IUriConversionService UriConversionService { get; }

        /// <summary>
        /// ExecutionService
        /// </summary>
        protected IExecutionService ExecutionService { get; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="configJsonService"></param>
        public CollectionManageService(
            IExecutionService executionService,
            IUriConversionService uriConversionService,
            IConfigJsonService configJsonService,
            Func<ConfigJson, ObservableCollection<T>> selectFunc)
        {
            ExecutionService = executionService;
            UriConversionService = uriConversionService;
            ConfigJson = configJsonService.ConfigJson;
            SelectCollectionFunc = selectFunc;
            Collection.CollectionChanged += Collection_CollectionChanged;
        }

        /// <summary>
        /// Collectionの変更通知処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Collection_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            ConfigJson.IsChanged = true;
        }

        /// <summary>
        /// 入力値をもとに項目を追加
        /// </summary>
        public void AddNewItem()
        {
            if (ValidateAndCreateNewItem(out var item))
            {
                Collection.Add(item);
            }
        }

        /// <summary>
        /// 指定したインデックスの項目の削除
        /// </summary>
        /// <param name="index"></param>
        public void RemoveAt(int index)
        {
            Collection.RemoveAt(index);
        }

        /// <summary>
        /// 全項目の削除
        /// </summary>
        public void RemoveAll()
        {
            Collection.Clear();
        }

        /// <summary>
        /// 選択項目のブラウズ
        /// </summary>
        /// <param name="item">選択項目</param>
        public abstract void BrowsItem(T item);

        /// <summary>
        /// 項目追加時のバリデーションと、新規項目の作成
        /// </summary>
        /// <param name="item">作成された追加項目</param>
        /// <returns>バリデーションOKの場合true</returns>
        protected abstract bool ValidateAndCreateNewItem(out T item);

        /// <summary>
        /// エラー有無
        /// </summary>
        public bool HasErrors => ErrorDictionary.Count > 0;

        /// <summary>
        /// エラーメッセージのディクショナリ
        /// </summary>
        private Dictionary<string, string> ErrorDictionary { get; } = new Dictionary<string, string>();

        /// <summary>
        /// エラー情報の取得
        /// </summary>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public IEnumerable GetErrors(string propertyName)
        {
            if (ErrorDictionary.TryGetValue(propertyName, out var error))
            {
                return new[] { error };
            }
            return null;
        }

        /// <summary>
        /// エラー情報の設定
        /// </summary>
        /// <param name="propertyName">エラーの発生したプロパティ名</param>
        /// <param name="message">エラーメッセージ</param>
        protected void SetError(string propertyName, string message)
        {
            ErrorDictionary[propertyName] = message;
            RaiseErrorChanged(propertyName);
        }

        /// <summary>
        /// エラー情報のクリア
        /// </summary>
        /// <param name="propertyName">エラー情報をクリアするプロパティ名</param>
        protected void ClearError(string propertyName)
        {
            if (ErrorDictionary.Remove(propertyName))
            {
                RaiseErrorChanged(propertyName);
            }
        }

        /// <summary>
        /// エラー通知
        /// </summary>
        /// <param name="propertyName">エラー情報が更新されたプロパティ名</param>
        protected void RaiseErrorChanged(string propertyName)
        {
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
        }

        /// <summary>
        /// ディスポーズ済みフラグ
        /// </summary>
        private bool disposed = false;

        /// <summary>
        /// Dispose
        /// </summary>
        public void Dispose()
        {
            if (disposed)
            {
                return;
            }

            disposed = true;
            Collection.CollectionChanged -= Collection_CollectionChanged;
        }
    }
}
