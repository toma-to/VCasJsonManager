//
// VCasJsonManager
// Copyright 2019 TOMA
// MIT License
//
using VCasJsonManager.Properties;

namespace VCasJsonManager.Services
{
    /// <summary>
    /// 動画IDのコレクションを管理するクラス
    /// </summary>
    public class NicovideoIdCollectionService : CollectionManageService<string>, INicovideoIdCollectionService
    {

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="executionService"></param>
        /// <param name="uriConversionService"></param>
        /// <param name="configJsonService"></param>
        public NicovideoIdCollectionService(
            IExecutionService executionService,
            IUriConversionService uriConversionService,
            IConfigJsonService configJsonService)
            : base(executionService, uriConversionService, configJsonService, conf => conf.NicovideoIds)
        {
        }

        /// <summary>
        /// 選択項目のブラウズ
        /// </summary>
        /// <param name="item">選択項目</param>
        public override void BrowsItem(string item)
        {
            var url = UriConversionService.BuildNicoViedoUrl(item);
            ExecutionService.RunBrowser(url);
        }

        /// <summary>
        /// 項目追加時のバリデーションと、新規項目の作成
        /// </summary>
        /// <param name="item">作成された追加項目</param>
        /// <returns>バリデーションOKの場合true</returns>
        protected override bool ValidateAndCreateNewItem(out string item)
        {
            item = null;
            if (string.IsNullOrEmpty(InputValue))
            {
                SetError(nameof(InputValue), Resources.ValidationNoInput);
                return false;
            }

            var val = UriConversionService.ExtractNicoVideoId(InputValue);
            if (val == null)
            {
                SetError(nameof(InputValue), Resources.ValidationBadNicovideoId);
                return false;
            }

            item = val;
            ClearError(nameof(InputValue));
            return true;
        }
    }
}
