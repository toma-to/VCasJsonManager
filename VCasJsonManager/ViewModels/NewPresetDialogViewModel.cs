//
// VCasJsonManager
// Copyright 2019 TOMA
// MIT License
//
using Livet;
using Livet.Messaging.Windows;
using PropertyChanged;
using System.ComponentModel;

namespace VCasJsonManager.ViewModels
{
    /// <summary>
    /// 新規プリセット保存ダイアログのViewModel
    /// </summary>
    public class NewPresetDialogViewModel : ViewModel, INotifyPropertyChanged
    {
        /// <summary>
        /// プリセット名
        /// </summary>
        public string PresetName { get; set; }

        /// <summary>
        /// OK呼び出し可否
        /// </summary>
        [DependsOn(nameof(PresetName))]
        public bool OkEnable => !string.IsNullOrEmpty(PresetName);

        /// <summary>
        /// ダイアログがOKで閉じられたかのフラグ
        /// </summary>
        public bool IsOk { get; private set; }

        /// <summary>
        /// OK処理
        /// </summary>
        public void Ok()
        {
            IsOk = true;
            Messenger.Raise(new WindowActionMessage(WindowAction.Close, "Close"));
        }

        /// <summary>
        /// キャンセル処理
        /// </summary>
        public void Cancel()
        {
            IsOk = false;
            Messenger.Raise(new WindowActionMessage(WindowAction.Close, "Close"));
        }
    }
}
