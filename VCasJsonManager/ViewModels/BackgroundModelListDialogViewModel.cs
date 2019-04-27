//
// VCasJsonManager
// Copyright 2019 TOMA
// MIT License
//
using VCasJsonManager.Services;


namespace VCasJsonManager.ViewModels
{
    /// <summary>
    /// 背景モデル設定画面のViewModel
    /// </summary>
    public class BackgroundModelListDialogViewModel : ListEditDialogViewModelBase<int>
    {

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="collectionService"></param>
        public BackgroundModelListDialogViewModel(INico3dIdCollectionService collectionService) : base(collectionService)
        {
        }
    }
}
