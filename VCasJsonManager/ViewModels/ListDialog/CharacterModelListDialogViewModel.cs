//
// VCasJsonManager
// Copyright 2019 TOMA
// MIT License
//
using VCasJsonManager.Services;

namespace VCasJsonManager.ViewModels.ListDialog
{
    /// <summary>
    /// キャラクターモデル設定画面のViewModel
    /// </summary>
    public class CharacterModelListDialogViewModel : ListEditDialogViewModelBase<int>
    {

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="collectionService"></param>
        public CharacterModelListDialogViewModel(INico3dIdCollectionService collectionService) : base(collectionService)
        {
        }
    }
}
