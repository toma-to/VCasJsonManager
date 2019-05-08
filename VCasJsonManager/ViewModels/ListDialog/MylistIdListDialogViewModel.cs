//
// VCasJsonManager
// Copyright 2019 TOMA
// MIT License
//
using VCasJsonManager.Services;

namespace VCasJsonManager.ViewModels
{
    /// <summary>
    /// マイリストID設定画面のViewModel
    /// </summary>
    public class MylistIdListDialogViewModel : ListEditDialogViewModelBase<int>
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="collectionService"></param>
        public MylistIdListDialogViewModel(IMylistIdCollectionService collectionService) : base(collectionService)
        {
        }
    }
}
