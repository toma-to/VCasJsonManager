//
// VCasJsonManager
// Copyright 2019 TOMA
// MIT License
//
using Livet;
using System.Collections.Generic;
using System.Linq;

namespace VCasJsonManager.ViewModels
{
    /// <summary>
    /// ModelからのPropertyChangedをマッピングして通知するためのViewModelの基本クラス
    /// </summary>
    public abstract class PropertyChangedMappingViewModelBase : ViewModel
    {
        /// <summary>
        /// プロパティ名のマッピング
        /// </summary>
        private Dictionary<string, IEnumerable<string>> Mapping { get; } = new Dictionary<string, IEnumerable<string>>();

        /// <summary>
        /// マッピングの追加
        /// </summary>
        /// <param name="modelProp">モデルのプロパティ名</param>
        /// <param name="vmProp">ViewModelのプロパティ名。省略時はモデルのプロパティ名と同一</param>
        protected void AddMapping(string modelProp, string vmProp = null)
        {
            AddMapping(modelProp, new[] { vmProp ?? modelProp });
        }

        /// <summary>
        /// マッピングの追加
        /// </summary>
        /// <param name="modelProp">モデルのプロパティ名</param>
        /// <param name="vmProps">ViewModelのプロパティ名</param>
        protected void AddMapping(string modelProp, IEnumerable<string> vmProps)
        {
            if (!Mapping.TryGetValue(modelProp, out var list))
            {
                list = new List<string>();
            }
            Mapping[modelProp] = list.Concat(vmProps);
        }

        /// <summary>
        /// PropertyChangedのマッピング
        /// </summary>
        /// <param name="modelProp"></param>
        protected void MapPropertyChanged(string modelProp)
        {
            if (Mapping.TryGetValue(modelProp, out var list))
            {
                foreach (var prop in list)
                {
                    RaisePropertyChanged(prop);
                }
            }
        }

        /// <summary>
        /// 全てのプロパティのProperyChangedの通知
        /// </summary>
        /// <param name="ignoreProp">通知しないViewModelのプロパティ名</param>
        protected void RaiseAllPropertyChanged(params string[] ignoreProp)
        {
            var list = Mapping.SelectMany(e => e.Value).Distinct().Where(e => !ignoreProp.Contains(e));
            foreach (var prop in list)
            {
                RaisePropertyChanged(prop);
            }
        }
    }
}
