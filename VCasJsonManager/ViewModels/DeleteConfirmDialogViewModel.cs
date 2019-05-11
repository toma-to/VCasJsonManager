//
// VCasJsonManager
// Copyright 2019 TOMA
// MIT License
//

using Livet;
using Livet.Messaging.Windows;

namespace VCasJsonManager.ViewModels
{
    /// <summary>
    /// 削除確認ダイアログのViewModel
    /// </summary>
    public class DeleteConfirmDialogViewModel : ViewModel
    {
        /// <summary>
        /// ダイアログの確認結果
        /// </summary>
        public bool Confirmed { get; set; } = false;

        /// <summary>
        /// OK処理
        /// </summary>
        public void Ok()
        {
            Confirmed = true;
            Messenger.Raise(new WindowActionMessage(WindowAction.Close, "Close"));
        }

        /// <summary>
        /// キャンセル処理
        /// </summary>
        public void Cancel()
        {
            Confirmed = false;
            Messenger.Raise(new WindowActionMessage(WindowAction.Close, "Close"));
        }
    }
}
