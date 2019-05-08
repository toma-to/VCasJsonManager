//
// VCasJsonManager
// Copyright 2019 TOMA
// MIT License
//
using System;
using System.Collections.ObjectModel;
using VCasJsonManager.Models;
using VCasJsonManager.Properties;

namespace VCasJsonManager.Services.Impl
{
    /// <summary>
    /// ニコニ立体のIDのコレクションを管理するクラス
    /// </summary>
    public class Nico3dIdCollectionService : CollectionManageService<int>, INico3dIdCollectionService
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="uriConversionService"></param>
        /// <param name="configJsonService"></param>
        /// <param name="selectFunc"></param>
        public Nico3dIdCollectionService(
            IExecutionService executionService,
            IUriConversionService uriConversionService,
            IConfigJsonService configJsonService,
            Func<ConfigJson, ObservableCollection<int>> selectFunc)
            : base(executionService, uriConversionService, configJsonService, selectFunc)
        {
        }

        /// <summary>
        /// 項目追加時のバリデーションと、新規項目の作成
        /// </summary>
        /// <param name="item">作成された追加項目</param>
        /// <returns>バリデーションOKの場合true</returns>
        protected override bool ValidateAndCreateNewItem(out int item)
        {
            item = 0;
            if (string.IsNullOrEmpty(InputValue))
            {
                SetError(nameof(InputValue), Resources.ValidationNoInput);
                return false;
            }

            var val = UriConversionService.ExtractNico3dId(InputValue);
            if (val is int wk)
            {
                item = wk;
                ClearError(nameof(InputValue));
                return true;
            }
            else
            {
                SetError(nameof(InputValue), Resources.ValidationBadNico3dId);
                return false;
            }
        }

        /// <summary>
        /// 選択項目のブラウズ
        /// </summary>
        /// <param name="item">選択項目</param>
        public override void BrowsItem(int item)
        {
            var url = UriConversionService.BuildNico3dUrl(item);
            ExecutionService.RunBrowser(url);
        }
    }
}
