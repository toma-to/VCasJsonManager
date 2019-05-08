//
// VCasJsonManager
// Copyright 2019 TOMA
// MIT License
//
using VCasJsonManager.Properties;

namespace VCasJsonManager.Services
{
    /// <summary>
    /// マイリストIDのコレクションを管理するクラス
    /// </summary>
    public class MylistIdCollectionService : CollectionManageService<int>, IMylistIdCollectionService
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="executionService"></param>
        /// <param name="uriConversionService"></param>
        /// <param name="configJsonService"></param>
        public MylistIdCollectionService(
            IExecutionService executionService,
            IUriConversionService uriConversionService,
            IConfigJsonService configJsonService)
            : base(executionService, uriConversionService, configJsonService, conf => conf.MylistIds)
        {
        }

        /// <summary>
        /// 選択項目のブラウズ
        /// </summary>
        /// <param name="item">選択項目</param>
        public override void BrowsItem(int item)
        {
            var url = UriConversionService.BuildMylistUrl(item);
            ExecutionService.RunBrowser(url);
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

            var val = UriConversionService.ExtractMylistId(InputValue);
            if (val is int wk)
            {
                item = wk;
                ClearError(nameof(InputValue));
                return true;
            }
            else
            {
                SetError(nameof(InputValue), Resources.ValidationBadMylistId);
                return false;
            }
        }
    }
}
