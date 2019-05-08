//
// VCasJsonManager
// Copyright 2019 TOMA
// MIT License
//
using System;
using VCasJsonManager.Services;

namespace VCasJsonManager.ViewModels
{
    /// <summary>
    /// 背景画像設定画面のViewModel
    /// </summary>
    public class BackgroundImageListDialogViewModel : ListEditDialogViewModelBase<Uri>
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="collectionService"></param>
        public BackgroundImageListDialogViewModel(IUriCollectionService collectionService) : base(collectionService)
        {
        }
    }
}
