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
    /// カンペ画像設定画面のViewModel
    /// </summary>
    class CueCardListDialogViewModel : ListEditDialogViewModelBase<Uri>
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="collectionService"></param>
        public CueCardListDialogViewModel(IUriCollectionService collectionService) : base(collectionService)
        {
        }
    }
}
