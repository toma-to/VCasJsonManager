//
// VCasJsonManager
// Copyright 2019 TOMA
// MIT License
//
using VCasJsonManager.Services;

namespace VCasJsonManager.ViewModels.ListDialog
{
    /// <summary>
    /// 永続化画像(両面)設定画面のViewModel
    /// </summary>
    public class DoubleImageListDialogViewModel : DoubleSideListEditDialogViewModelBase
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="collectionService"></param>
        public DoubleImageListDialogViewModel(IDoubleImageCollectionService collectionService) : base(collectionService)
        {
        }
    }
}
