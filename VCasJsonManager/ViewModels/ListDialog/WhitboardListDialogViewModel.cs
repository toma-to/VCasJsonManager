//
// VCasJsonManager
// Copyright 2019 TOMA
// MIT License
//
using System;
using VCasJsonManager.Services;

namespace VCasJsonManager.ViewModels.ListDialog
{
    /// <summary>
    /// ホワイトボード画像設定画面のViewModel
    /// </summary>
    public class WhitboardListDialogViewModel : ListEditDialogViewModelBase<Uri>
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="collectionService"></param>
        public WhitboardListDialogViewModel(IUriCollectionService collectionService) : base(collectionService)
        {
        }
    }
}
