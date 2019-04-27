//
// VCasJsonManager
// Copyright 2019 TOMA
// MIT License
//
using Livet.Commands;
using PropertyChanged;
using System;
using System.Windows.Input;
using VCasJsonManager.Models;
using VCasJsonManager.Services;

namespace VCasJsonManager.ViewModels
{
    /// <summary>
    /// 両面画像リスト設定画面のViewModelの基本クラス
    /// </summary>
    public abstract class DoubleSideListEditDialogViewModelBase : ListEditDialogViewModelBase<ConfigJson.DoubleImageUrls>
    {
        /// <summary>
        /// DoubleImageCollectionService
        /// </summary>
        private IDoubleImageCollectionService DoubleImageCollectionService { get; }

        /// <summary>
        /// 裏面用入力値
        /// </summary>
        [DoNotNotify]
        public string AnotherInputValue
        {
            get => DoubleImageCollectionService.AnotherInputValue;
            set => DoubleImageCollectionService.AnotherInputValue = value;
        }

        /// <summary>
        /// 追加可否
        /// </summary>
        public bool CanAdd => !string.IsNullOrEmpty(InputValue) && !string.IsNullOrEmpty(AnotherInputValue);

        /// <summary>
        /// アイテム表示コマンド
        /// </summary>
        public ICommand BrowsUriCommand { get; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="collectionService"></param>
        public DoubleSideListEditDialogViewModelBase(IDoubleImageCollectionService collectionService) : base(collectionService)
        {
            DoubleImageCollectionService = collectionService;

            AddMapping(nameof(IDoubleImageCollectionService.AnotherInputValue), new[] { nameof(AnotherInputValue), nameof(CanAdd) });
            AddMapping(nameof(IDoubleImageCollectionService.InputValue), nameof(CanAdd));

            BrowsUriCommand = new ListenerCommand<Uri>(e => BrowsItem(e));
        }

        /// <summary>
        /// アイテムのブラウズ
        /// </summary>
        /// <param name="item"></param>
        public virtual void BrowsItem(Uri item)
        {
            DoubleImageCollectionService.BrowsItem(item);
        }

        /// <summary>
        /// アイテムの追加
        /// </summary>
        public override void AddItem()
        {
            base.AddItem();
            AnotherInputValue = string.Empty;
        }
    }
}
