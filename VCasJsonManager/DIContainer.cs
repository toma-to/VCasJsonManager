//
// VCasJsonManager
// Copyright 2019 TOMA
// MIT License
//
using Unity;
using VCasJsonManager.Models.Settings;
using VCasJsonManager.Services;
using VCasJsonManager.Services.Impl;
using VCasJsonManager.ViewModels.ListDialog;

namespace VCasJsonManager
{
    /// <summary>
    /// DIコンテナ
    /// </summary>
    public static class DIContainer
    {
        /// <summary>
        /// コンテナオブジェクト
        /// </summary>
        public static IUnityContainer Container { get; }

        /// <summary>
        /// クラス初期化
        /// </summary>
        static DIContainer()
        {
            Container = new UnityContainer();

            // 依存関係の登録
            Container.RegisterType<IAppSettings, AppSettings>(TypeLifetime.Singleton);
            Container.RegisterType<IUserSettingsService, UserSettingsService>(TypeLifetime.Singleton);
            Container.RegisterType<IConfigJsonFileService, ConfigJsonFileService>(TypeLifetime.Singleton);
            Container.RegisterType<IConfigJsonService, ConfigJsonService>(TypeLifetime.Singleton);
            Container.RegisterType<IExecutionService, ExecutionService>(TypeLifetime.Singleton);
            Container.RegisterType<IUriConversionService, UriConversionService>(TypeLifetime.Singleton);

            Container.RegisterFactory<INico3dIdCollectionService>("ModelIdService",
                c => new Nico3dIdCollectionService(
                    c.Resolve<IExecutionService>(),
                    c.Resolve<IUriConversionService>(),
                    c.Resolve<IConfigJsonService>(),
                    conf => conf.CharacterModels),
                FactoryLifetime.PerResolve);
            Container.RegisterFactory<INico3dIdCollectionService>("BackgroudIdService",
                c => new Nico3dIdCollectionService(
                    c.Resolve<IExecutionService>(),
                    c.Resolve<IUriConversionService>(),
                    c.Resolve<IConfigJsonService>(),
                    conf => conf.BackgroundModels),
                FactoryLifetime.PerResolve);
            Container.RegisterFactory<IUriCollectionService>("BackgroundImageService",
                c => new UriCollectionService(
                    c.Resolve<IExecutionService>(),
                    c.Resolve<IUriConversionService>(),
                    c.Resolve<IConfigJsonService>(),
                    conf => conf.BackgroundUrls),
                FactoryLifetime.PerResolve);
            Container.RegisterFactory<IUriCollectionService>("WhiteboardService",
                c => new UriCollectionService(
                    c.Resolve<IExecutionService>(),
                    c.Resolve<IUriConversionService>(),
                    c.Resolve<IConfigJsonService>(),
                    conf => conf.WhiteboardUrls),
                FactoryLifetime.PerResolve);
            Container.RegisterFactory<IUriCollectionService>("CueCardService",
                c => new UriCollectionService(
                    c.Resolve<IExecutionService>(),
                    c.Resolve<IUriConversionService>(),
                    c.Resolve<IConfigJsonService>(),
                    conf => conf.CueCardUrls),
                FactoryLifetime.PerResolve);
            Container.RegisterFactory<IUriCollectionService>("ImageService",
                c => new UriCollectionService(
                    c.Resolve<IExecutionService>(),
                    c.Resolve<IUriConversionService>(),
                    c.Resolve<IConfigJsonService>(),
                    conf => conf.ImageUrls),
                FactoryLifetime.PerResolve);
            Container.RegisterFactory<IUriCollectionService>("HiddenImageService",
                c => new UriCollectionService(
                    c.Resolve<IExecutionService>(),
                    c.Resolve<IUriConversionService>(),
                    c.Resolve<IConfigJsonService>(),
                    conf => conf.HiddenImageUrls),
                FactoryLifetime.PerResolve);
            Container.RegisterFactory<ITextCollectionService>("CommentListService",
                c => new TextCollectionService(
                    c.Resolve<IExecutionService>(),
                    c.Resolve<IUriConversionService>(),
                    c.Resolve<IConfigJsonService>(),
                    conf => conf.BroadcasterComments),
                FactoryLifetime.PerResolve);
            Container.RegisterFactory<IDoubleImageCollectionService>("DoubleImageService",
                c => new DoubleImageCollectionService(
                    c.Resolve<IExecutionService>(),
                    c.Resolve<IUriConversionService>(),
                    c.Resolve<IConfigJsonService>(),
                    conf => conf.DoubleSidedImageUrls),
                FactoryLifetime.PerResolve);
            Container.RegisterFactory<IDoubleImageCollectionService>("HiddenDoubleImageService",
                c => new DoubleImageCollectionService(
                    c.Resolve<IExecutionService>(),
                    c.Resolve<IUriConversionService>(),
                    c.Resolve<IConfigJsonService>(),
                    conf => conf.HiddenDoubleSidedImageUrls),
                FactoryLifetime.PerResolve);
            Container.RegisterType<IMylistIdCollectionService, MylistIdCollectionService>(TypeLifetime.PerResolve);
            Container.RegisterType<INicovideoIdCollectionService, NicovideoIdCollectionService>(TypeLifetime.PerResolve);

            Container.RegisterFactory<CharacterModelListDialogViewModel>(
                c => new CharacterModelListDialogViewModel(c.Resolve<INico3dIdCollectionService>("ModelIdService")),
                FactoryLifetime.PerResolve);
            Container.RegisterFactory<BackgroundModelListDialogViewModel>(
                c => new BackgroundModelListDialogViewModel(c.Resolve<INico3dIdCollectionService>("BackgroudIdService")),
                FactoryLifetime.PerResolve);
            Container.RegisterFactory<BackgroundImageListDialogViewModel>(
                c => new BackgroundImageListDialogViewModel(c.Resolve<IUriCollectionService>("BackgroundImageService")),
                FactoryLifetime.PerResolve);
            Container.RegisterFactory<WhitboardListDialogViewModel>(
                c => new WhitboardListDialogViewModel(c.Resolve<IUriCollectionService>("WhiteboardService")),
                FactoryLifetime.PerResolve);
            Container.RegisterFactory<CueCardListDialogViewModel>(
                c => new CueCardListDialogViewModel(c.Resolve<IUriCollectionService>("CueCardService")),
                FactoryLifetime.PerResolve);
            Container.RegisterFactory<ImageListDialogViewModel>(
                c => new ImageListDialogViewModel(c.Resolve<IUriCollectionService>("ImageService")),
                FactoryLifetime.PerResolve);
            Container.RegisterFactory<HiddenImageListDialogViewModel>(
                c => new HiddenImageListDialogViewModel(c.Resolve<IUriCollectionService>("HiddenImageService")),
                FactoryLifetime.PerResolve);
            Container.RegisterFactory<CommentListDialogViewModel>(
                c => new CommentListDialogViewModel(c.Resolve<ITextCollectionService>("CommentListService")),
                FactoryLifetime.PerResolve);
            Container.RegisterFactory<DoubleImageListDialogViewModel>(
                c => new DoubleImageListDialogViewModel(c.Resolve<IDoubleImageCollectionService>("DoubleImageService")),
                FactoryLifetime.PerResolve);
            Container.RegisterFactory<HiddenDoubleListDialogViewModel>(
                c => new HiddenDoubleListDialogViewModel(c.Resolve<IDoubleImageCollectionService>("HiddenDoubleImageService")),
                FactoryLifetime.PerResolve);
        }
    }
}
