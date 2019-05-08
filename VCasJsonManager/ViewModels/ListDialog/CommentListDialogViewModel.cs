//
// VCasJsonManager
// Copyright 2019 TOMA
// MIT License
//
using VCasJsonManager.Services;

namespace VCasJsonManager.ViewModels.ListDialog
{
    /// <summary>
    /// 運営コメント設定画面のViewModel
    /// </summary>
    public class CommentListDialogViewModel : ListEditDialogViewModelBase<string>
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="collectionService"></param>
        public CommentListDialogViewModel(ITextCollectionService collectionService) : base(collectionService)
        {
        }
    }
}
