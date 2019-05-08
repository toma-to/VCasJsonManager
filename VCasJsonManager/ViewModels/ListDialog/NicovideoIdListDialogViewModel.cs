//
// VCasJsonManager
// Copyright 2019 TOMA
// MIT License
//
using VCasJsonManager.Services;

namespace VCasJsonManager.ViewModels
{
    /// <summary>
    /// 動画ID設定画面のViewModel
    /// </summary>
    public class NicovideoIdListDialogViewModel : ListEditDialogViewModelBase<string>
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="collectionService"></param>
        public NicovideoIdListDialogViewModel(INicovideoIdCollectionService collectionService) : base(collectionService)
        {
        }
    }
}
