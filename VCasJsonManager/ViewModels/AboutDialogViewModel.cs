//
// VCasJsonManager
// Copyright 2019 TOMA
// MIT License
//
using System;
using Livet;
using VCasJsonManager.Properties;
using System.Reflection;
using VCasJsonManager.Services;
using Unity;

namespace VCasJsonManager.ViewModels
{
    /// <summary>
    /// バージョン情報ダイアログのViewModel
    /// </summary>
    public class AboutDialogViewModel : ViewModel
    {
        /// <summary>
        /// ExecutionService
        /// </summary>
        private IExecutionService ExecutionService { get; }

        /// <summary>
        /// バージョン情報文字列
        /// </summary>
        public string VersionString { get; }

        /// <summary>
        /// アプリケーション名
        /// </summary>
        public string AppName { get; }

        /// <summary>
        /// 著作権表示
        /// </summary>
        public string Copyright { get; }

        /// <summary>
        /// アプリの概要
        /// </summary>
        public string Description { get; }

        /// <summary>
        /// アプリケーションのリリースページのURL
        /// </summary>
        public string ReleasePageUrl => Resources.ReleasePageUrl;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="executionService"></param>
        public AboutDialogViewModel(IExecutionService executionService)
        {
            ExecutionService = executionService;

            // アセンブリ情報取得
            var assm = Assembly.GetExecutingAssembly();
            T GetAssemblyAtrribute<T>() where T : Attribute
                => Attribute.GetCustomAttribute(assm, typeof(T)) as T;

            VersionString = assm.GetName().Version.ToString();
            AppName = GetAssemblyAtrribute<AssemblyTitleAttribute>()?.Title;
            Copyright = GetAssemblyAtrribute<AssemblyCopyrightAttribute>()?.Copyright;
            Description = GetAssemblyAtrribute<AssemblyDescriptionAttribute>()?.Description;
        }

        /// <summary>
        /// リリースページの表示
        /// </summary>
        public void BrowseReleasePage()
        {
            ExecutionService.RunBrowser(ReleasePageUrl);
        }

        // TODO: xaml編集終わったら消す
        public AboutDialogViewModel() : this(DIContainer.Container.Resolve<IExecutionService>()) { }
    }
}
