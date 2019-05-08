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
    /// 隠し画像設定画面のViewModel
    /// </summary>
    public class HiddenImageListDialogViewModel : ListEditDialogViewModelBase<Uri>
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="collectionService"></param>
        public HiddenImageListDialogViewModel(IUriCollectionService collectionService) : base(collectionService)
        {
        }
    }
}
