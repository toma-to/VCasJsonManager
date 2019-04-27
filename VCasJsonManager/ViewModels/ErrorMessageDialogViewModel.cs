//
// VCasJsonManager
// Copyright 2019 TOMA
// MIT License
//
using Livet;
using PropertyChanged;

namespace VCasJsonManager.ViewModels
{
    /// <summary>
    /// エラーメッセージ表示ダイアログ用ViewModel
    /// </summary>
    public class ErrorMessageDialogViewModel : ViewModel
    {
        /// <summary>
        /// 詳細情報
        /// </summary>
        public string Detail { get; set; }

        /// <summary>
        /// エラーメッセージ
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 詳細情報有無
        /// </summary>
        [DependsOn(nameof(Detail))]
        public bool HasDetail => !string.IsNullOrEmpty(Detail);
    }
}
