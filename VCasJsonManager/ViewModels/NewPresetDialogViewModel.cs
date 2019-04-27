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

        /* コマンド、プロパティの定義にはそれぞれ 
         * 
         *  lvcom   : ViewModelCommand
         *  lvcomn  : ViewModelCommand(CanExecute無)
         *  llcom   : ListenerCommand(パラメータ有のコマンド)
         *  llcomn  : ListenerCommand(パラメータ有のコマンド・CanExecute無)
         *  lprop   : 変更通知プロパティ(.NET4.5ではlpropn)
         *  
         * を使用してください。
         * 
         * Modelが十分にリッチであるならコマンドにこだわる必要はありません。
         * View側のコードビハインドを使用しないMVVMパターンの実装を行う場合でも、ViewModelにメソッドを定義し、
         * LivetCallMethodActionなどから直接メソッドを呼び出してください。
         * 
         * ViewModelのコマンドを呼び出せるLivetのすべてのビヘイビア・トリガー・アクションは
         * 同様に直接ViewModelのメソッドを呼び出し可能です。
         */

        /* ViewModelからViewを操作したい場合は、View側のコードビハインド無で処理を行いたい場合は
         * Messengerプロパティからメッセージ(各種InteractionMessage)を発信する事を検討してください。
         */

        /* Modelからの変更通知などの各種イベントを受け取る場合は、PropertyChangedEventListenerや
         * CollectionChangedEventListenerを使うと便利です。各種ListenerはViewModelに定義されている
         * CompositeDisposableプロパティ(LivetCompositeDisposable型)に格納しておく事でイベント解放を容易に行えます。
         * 
         * ReactiveExtensionsなどを併用する場合は、ReactiveExtensionsのCompositeDisposableを
         * ViewModelのCompositeDisposableプロパティに格納しておくのを推奨します。
         * 
         * LivetのWindowテンプレートではViewのウィンドウが閉じる際にDataContextDisposeActionが動作するようになっており、
         * ViewModelのDisposeが呼ばれCompositeDisposableプロパティに格納されたすべてのIDisposable型のインスタンスが解放されます。
         * 
         * ViewModelを使いまわしたい時などは、ViewからDataContextDisposeActionを取り除くか、発動のタイミングをずらす事で対応可能です。
         */

        /* UIDispatcherを操作する場合は、DispatcherHelperのメソッドを操作してください。
         * UIDispatcher自体はApp.xaml.csでインスタンスを確保してあります。
         * 
         * LivetのViewModelではプロパティ変更通知(RaisePropertyChanged)やDispatcherCollectionを使ったコレクション変更通知は
         * 自動的にUIDispatcher上での通知に変換されます。変更通知に際してUIDispatcherを操作する必要はありません。
         */

        public void Initialize()
        {
        }
    }
}
