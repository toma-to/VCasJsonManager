//
// VCasJsonManager
// Copyright 2019 TOMA
// MIT License
//
using Livet.Commands;
using Livet.EventListeners;
using Livet.Messaging;
using PropertyChanged;
using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using VCasJsonManager.Services;

namespace VCasJsonManager.ViewModels.ListDialog
{
    /// <summary>
    /// リストの編集用ダイアログのViewModel基本クラス
    /// </summary>
    public class ListEditDialogViewModelBase<T> : PropertyChangedMappingViewModelBase, INotifyDataErrorInfo
    {
        /// <summary>
        /// エラー有無
        /// </summary>
        public bool HasErrors => CollectionService.HasErrors;

        /// <summary>
        /// バリデーションエラー通知イベント
        /// </summary>
        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged
        {
            add
            {
                CollectionService.ErrorsChanged += value;
            }

            remove
            {
                CollectionService.ErrorsChanged -= value;
            }
        }

        /// <summary>
        /// CollectionService
        /// </summary>
        private ICollectionManageService<T> CollectionService { get; }

        /// <summary>
        /// リスト
        /// </summary>
        public ObservableCollection<T> Collection => CollectionService.Collection;

        /// <summary>
        /// アイテム表示コマンド
        /// </summary>
        public ICommand BrowsItemCommand { get; }

        /// <summary>
        /// アイテム削除コマンド
        /// </summary>
        public ICommand DeleteItemCommand { get; }

        /// <summary>
        /// 入力項目
        /// </summary>
        [DoNotNotify]
        public string InputValue
        {
            get => CollectionService.InputValue;
            set => CollectionService.InputValue = value;
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="collectionService"></param>
        public ListEditDialogViewModelBase(ICollectionManageService<T> collectionService)
        {
            CollectionService = collectionService;

            AddMapping(nameof(INico3dIdCollectionService.InputValue), nameof(InputValue));
            AddMapping(nameof(INico3dIdCollectionService.Collection), nameof(Collection));

            CompositeDisposable.Add(new PropertyChangedEventListener(CollectionService)
            {
                { (_, e) => MapPropertyChanged(e.PropertyName) }
            });

            BrowsItemCommand = new ListenerCommand<T>(item => BrowsItem(item));
            DeleteItemCommand = new ListenerCommand<int>(index => DeleteItem(index));
        }

        /// <summary>
        /// アイテムの追加
        /// </summary>
        public virtual void AddItem()
        {
            CollectionService.AddNewItem();
            InputValue = string.Empty;
        }

        /// <summary>
        /// アイテムのブラウズ
        /// </summary>
        /// <param name="item"></param>
        public virtual void BrowsItem(T item)
        {
            CollectionService.BrowsItem(item);
        }

        /// <summary>
        /// アイテムの削除
        /// </summary>
        /// <param name="index"></param>
        public virtual void DeleteItem(int index)
        {
            CollectionService.RemoveAt(index);
        }

        /// <summary>
        /// バリデーションエラーの取得
        /// </summary>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public IEnumerable GetErrors(string propertyName)
        {
            return CollectionService.GetErrors(propertyName);
        }

        /// <summary>
        /// 全アイテムの削除
        /// </summary>
        public virtual void DeleteAll()
        {
            var dlgVm = new DeleteConfirmDialogViewModel();
            Messenger.Raise(new TransitionMessage(dlgVm, "DeleteConfirmDialog"));

            if (dlgVm.Confirmed)
            {
                CollectionService.RemoveAll();
            }
        }

    }
}
