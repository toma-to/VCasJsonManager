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
    /// 画像設定画面のViewModel
    /// </summary>
    public class ImageListDialogViewModel : ListEditDialogViewModelBase<Uri>
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="collectionService"></param>
        public ImageListDialogViewModel(IUriCollectionService collectionService) : base(collectionService)
        {
        }
    }
}
