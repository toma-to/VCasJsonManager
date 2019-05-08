//
// VCasJsonManager
// Copyright 2019 TOMA
// MIT License
//
using System;
using System.Collections.ObjectModel;

using VCasJsonManager.Models;
using VCasJsonManager.Properties;

namespace VCasJsonManager.Services
{
    /// <summary>
    /// 両面画像のコレクションを管理するクラス
    /// </summary>
    public class DoubleImageCollectionService : CollectionManageService<ConfigJson.DoubleImageUrls>, IDoubleImageCollectionService
    {
        /// <summary>
        /// 入力値(裏面)
        /// </summary>
        public string AnotherInputValue { get; set; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="executionService"></param>
        /// <param name="uriConversionService"></param>
        /// <param name="configJsonService"></param>
        /// <param name="selectFunc"></param>
        public DoubleImageCollectionService(
            IExecutionService executionService,
            IUriConversionService uriConversionService,
            IConfigJsonService configJsonService,
            Func<ConfigJson, ObservableCollection<ConfigJson.DoubleImageUrls>> selectFunc)
            : base(executionService, uriConversionService, configJsonService, selectFunc)
        {
        }

        /// <summary>
        /// 選択項目のブラウズ。この呼び出し形式は本クラスでは未サポート
        /// </summary>
        /// <param name="item">選択項目</param>
        public override void BrowsItem(ConfigJson.DoubleImageUrls item)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// 選択項目のブラウズ。
        /// </summary>
        /// <param name="item">選択項目</param>
        public void BrowsItem(Uri item)
        {
            ExecutionService.RunBrowser(item);
        }

        /// <summary>
        /// 項目追加時のバリデーションと、新規項目の作成
        /// </summary>
        /// <param name="item">作成された追加項目</param>
        /// <returns>バリデーションOKの場合true</returns>
        protected override bool ValidateAndCreateNewItem(out ConfigJson.DoubleImageUrls item)
        {
            ClearError(nameof(InputValue));
            ClearError(nameof(AnotherInputValue));

            if (string.IsNullOrEmpty(InputValue))
            {
                SetError(nameof(InputValue), Resources.ValidationNoInput);
            }
            if (string.IsNullOrEmpty(AnotherInputValue))
            {
                SetError(nameof(AnotherInputValue), Resources.ValidationNoInput);
            }
            if (HasErrors)
            {
                item = null;
                return false;
            }

            var front = UriConversionService.BuildUri(InputValue);
            var back = UriConversionService.BuildUri(AnotherInputValue);
            if (front == null)
            {
                SetError(nameof(InputValue), Resources.ValidationBadUri);
            }
            if (back == null)
            {
                SetError(nameof(AnotherInputValue), Resources.ValidationBadUri);
            }
            if (HasErrors)
            {
                item = null;
                return false;
            }

            item = new ConfigJson.DoubleImageUrls(front, back);
            return true;
        }
    }
}
