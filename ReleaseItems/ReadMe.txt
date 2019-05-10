========== ========== ==========
 VCasJsonManager v1.2.0.4
 Copyright © 2019 TOMA 
========== ========== ==========

【 ソフト名 】VCasJsonManager
【ライセンス】MIT License
【 動作環境 】Windows10
【バージョン】1.2.0.4

---------- ----------
◇ 概要 ◇
バーチャルキャストのconfig.jsonを管理するツールです。
複数のconfig.jsonの設定をプリセットとして記憶し、任意のプリセットを呼び出して
置き換えることができます。

◇ 動作条件 ◇
Windows10
作成するconfig.jsonは、VirtualCast1.5.5bに対応しています。

◇ インストール ◇
お好きなフォルダに展開してください。
VCasJsonManagerフォルダー内のVCasJsonManager.exeを実行すると起動します。

◇ アンインストール ◇
同梱の「データフォルダ表示.bat」を実行すると、本ソフトウェアのデータ保存フォルダが表示されます。
アンインストール時はデータ保存フォルダを削除した後、本ソフトウェアのファイルを削除してください。
レジストリは使用していません。

◇ 使い方 ◇
※VirtualCastのconfig.jsonを作成済みの方は、事前にバックアップを取っておくことをお勧めします。
起動後は、右上の歯車アイコンをクリックし、設定画面にてVirtualCastのフォルダーを指定してください。
その他の操作については、同梱の「マニュアル.pdf」を参照してください。

◇ ライセンス ◇
本ソフトウェアはMIT Licenseにて公開しています。
ライセンスの詳細は、同梱の「LICENSE」ファイルを参照してください。
日本語訳については、https://ja.osdn.net/projects/opensource/wiki/licenses/MIT_license などを
参考にしてください。

◇ 更新履歴 ◇

v1.2.0.4 - 2019/5/10
「更新してVirtualCast起動」ボタンのToolTipがおかしい問題の修正。(Issue#10)

v1.2.0.3 - 2019/5/1
内部処理の軽微な修正。

v1.2.0.2 - 2019/4/30
バーチャルキャスト起動後にVCasJsonManagerを自動で終了する設定を追加。
アプリケーションの二重起動抑止処理を追加。
バージョン情報ダイアログを追加。

v1.1.0.1 - 2019/4/29
JSONファイルのインポート、エクスポート機能追加。

v1.0.0.0 - 2019/4/28
First Release

----------
◇ 連絡先 ◇
　GitHub：  https://github.com/toma-to/VCasJsonManager
            最新版は以下のURLよりダウンロード可能です。
            https://github.com/toma-to/VCasJsonManager/releases
            
　Discord:  兎摩#2704
  Discordサーバー： https://discord.gg/zf2T3fD
            （バグ報告、要望などは上記Discordサーバーで受け付けます。もちろんGitHubのissueでもOKです）


----------

◇ 使用ライブラリ ◇
本ソフトウェアでは以下のライブラリを使用しています。

Livet
 - https://github.com/ugaya40/Livet
 - [zlib/libpng license.]

Material Design In XAML Toolkit
 - https://github.com/MaterialDesignInXAML/MaterialDesignInXamlToolkit
 - [MIT License]

Json.NET
 - https://www.newtonsoft.com/json
 - [MIT License]

PropertyChanged.Fody
 - https://github.com/Fody/PropertyChanged
 - [MIT License]

Unity Container
 - https://github.com/unitycontainer/unity
 - [Apache License 2.0 (http://www.apache.org/licenses/LICENSE-2.0)]
