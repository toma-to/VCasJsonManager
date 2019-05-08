//
// VCasJsonManager
// Copyright 2019 TOMA
// MIT License
//
using VCasJsonManager.Services;

namespace VCasJsonManager.ViewModels.ListDialog
{
    /// <summary>
    /// niconare ID設定画面のViewModel
    /// </summary>
    public class NiconareIdListDialogViewModel : ListEditDialogViewModelBase<int>
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="collectionService"></param>
        public NiconareIdListDialogViewModel(INiconareIdCollectionService collectionService) : base(collectionService)
        {
        }
    }
}
