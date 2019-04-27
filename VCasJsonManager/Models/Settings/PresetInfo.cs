//
// VCasJsonManager
// Copyright 2019 TOMA
// MIT License
//
using System.ComponentModel;

namespace VCasJsonManager.Models.Settings
{
    /// <summary>
    /// プリセット情報を保持するクラス
    /// </summary>
    public class PresetInfo : INotifyPropertyChanged
    {
        /// <summary>
        /// PropertyChangedイベントハンドラ
        /// </summary>
#pragma warning disable 0067
        public event PropertyChangedEventHandler PropertyChanged;
#pragma warning restore 0067

        /// <summary>
        /// プリセットのID
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// プリセット情報名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// プリセットファイル名
        /// </summary>
        public string FileName { get; set; }

    }
}
