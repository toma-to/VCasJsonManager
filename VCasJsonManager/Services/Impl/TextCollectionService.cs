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
    /// テキストのコレクションを管理するクラス
    /// </summary>
    public class TextCollectionService : CollectionManageService<string>, ITextCollectionService
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="executionService"></param>
        /// <param name="uriConversionService"></param>
        /// <param name="configJsonService"></param>
        /// <param name="selectFunc"></param>
        public TextCollectionService(
            IExecutionService executionService,
            IUriConversionService uriConversionService,
            IConfigJsonService configJsonService,
            Func<ConfigJson, ObservableCollection<string>> selectFunc)
            : base(executionService, uriConversionService, configJsonService, selectFunc)
        {
        }

        /// <summary>
        /// 項目追加時のバリデーションと、新規項目の作成
        /// </summary>
        /// <param name="item">作成された追加項目</param>
        /// <returns>バリデーションOKの場合true</returns>
        protected override bool ValidateAndCreateNewItem(out string item)
        {
            item = InputValue;
            if (string.IsNullOrEmpty(InputValue))
            {
                SetError(nameof(InputValue), Resources.ValidationNoInput);
                return false;
            }
            ClearError(nameof(InputValue));

            return true;
        }

        /// <summary>
        /// 選択項目のブラウズ(本クラスでは非サポート)
        /// </summary>
        /// <param name="item">選択項目</param>
        public override void BrowsItem(string item)
        {
            throw new NotSupportedException();
        }
    }
}
