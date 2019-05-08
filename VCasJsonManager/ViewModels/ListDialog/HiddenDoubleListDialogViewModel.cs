//
// VCasJsonManager
// Copyright 2019 TOMA
// MIT License
//
using VCasJsonManager.Services;

namespace VCasJsonManager.ViewModels
{
    /// <summary>
    /// 隠し画像(両面)設定画面のViewModel
    /// </summary>
    public class HiddenDoubleListDialogViewModel : DoubleSideListEditDialogViewModelBase
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="collectionService"></param>
        public HiddenDoubleListDialogViewModel(IDoubleImageCollectionService collectionService) : base(collectionService)
        {
        }
    }
}
