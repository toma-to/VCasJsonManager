//
// VCasJsonManager
// Copyright 2019 TOMA
// MIT License
//
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VCasJsonManagerTests
{
    static class JsonStrings
    {
        public const string TemplateString = @"
{
    ""niconico"": {
        ""character_models"": [
            1,11
        ],
        ""character_models.description"": ""VRM形式のニコニ立体モデルのリスト (tdXXXXX)"",
        ""background_models"": [
            2
        ],
        ""background_models.description"": ""GLB形式のニコニ立体モデルのリスト (tdXXXXX)"",
        ""mylist_ids"": [
            3
        ],
        ""niconare_ids"": [
            4
        ],
        ""niconare_ids.description"": ""ニコナレからスライドの引用 (knXXXXX)"",
        ""broadcaster_comments"": [
            ""コメント""
        ],
        ""broadcaster_comments.description"": ""運営コメント"",
        ""ng_score_threshold"": -50,
        ""ng_score_threshold.description"": ""NGスコアのしきい値""
    },
    ""persistent_object"": {
        ""image_urls"": [""http://image.foo/""],
        ""image_urls.description"": ""初期表示を行う永続化画像のURLリスト"",
 
        ""double_sided_image_urls"": [
            [""http://dsfront.bar/"",""http://dsback.bar/""]
        ],
        ""double_sided_image_urls.description"": ""初期表示を行う永続化画像(両面)のURLリスト"",
 
        ""hidden_image_urls"": [""http://hidden.foo/""],
        ""hidden_image_urls.description"": ""視聴者に見せない永続化画像のURLリスト"",
 
        ""hidden_double_sided_image_urls"": [
            [""http://hdsfront.bar/"",""http://hdsback.bar/""]
        ],
        ""hidden_double_sided_image_urls.description"": ""視聴者に見せない永続化画像(両面)のURLリスト"",
 
        ""nicovideo_ids"": [""sm6""],
        ""nicovideo_ids.description"": ""初期表示を行うニコニコ動画の動画番号のリスト""
    },
    ""background"": {
        ""panorama"": {
        ""source_urls"": [""http://bg.foo/""],
        ""source_urls.description"": ""Equirectangular形式のパノラマ画像のURLリスト""
    }
    },
    ""item"": {
        ""whiteboard"": {
            ""source_urls"": [""http://white.foo/""],
            ""source_urls.description"": ""レーザーポインタでホワイトボードに切り替えするときの画像のURLリスト""
        },
        ""cue_card"": {
            ""source_urls"": [""http://cue.foo/""],
            ""source_urls.description"": ""レーザーポインタでカンペに切り替えするときの画像のURLリスト""
        },
        ""hide_camera_from_viewers"": false,
        ""hide_camera_from_viewers.description"": ""カメラオブジェクトの(視聴者への)表示/非表示"",
 
        ""enable_displaycapture_chromakey"": false,
        ""enable_displaycapture_chromakey.description"": ""ディスプレイにクロマキーを適用する"",
 
        ""enable_nicovideo_chromakey"": false,
        ""enable_nicovideo_chromakey.description"": ""ニコニコ動画プレイヤーにクロマキーを適用する"",

        ""capture_format"": ""png""
    },
    ""humanoid"": {
        ""use_fast_spring_bone"": false,
    },
    ""studio"": {
        ""allow_direct_view"": false,
        ""allow_direct_view.description"": ""ダイレクトビューモードでのスタジオ入室を許可するかどうか""
    },
    ""mode"": ""direct-view"",
    ""direct_view_talk"": false,
    ""enable_looking_glass"": false,
    ""enable_vivesranipal_eye"": false,
    ""enable_vivesranipal_blink"": false,
    ""vivesranipal_eye_adjust_x"": null,
    ""vivesranipal_eye_adjust_y"": null,
    ""embedded_script"": {
        ""websocket_console_port"": 8080,
        ""vr_debug"": false,
        ""moonsharp_debugger_port"":8888
    }
}";
        public const string TrueString = @"
{

    ""item"": {
        ""hide_camera_from_viewers"": true,
        ""hide_camera_from_viewers.description"": ""カメラオブジェクトの(視聴者への)表示/非表示"",
 
        ""enable_displaycapture_chromakey"": true,
        ""enable_displaycapture_chromakey.description"": ""ディスプレイにクロマキーを適用する"",
 
        ""enable_nicovideo_chromakey"": true,
        ""enable_nicovideo_chromakey.description"": ""ニコニコ動画プレイヤーにクロマキーを適用する""
    },
    ""humanoid"": {
        ""use_fast_spring_bone"": true,
    },
    ""studio"": {
        ""allow_direct_view"": true,
        ""allow_direct_view.description"": ""ダイレクトビューモードでのスタジオ入室を許可するかどうか""
    },
    ""direct_view_talk"": true,
    ""enable_looking_glass"": true,
    ""enable_vivesranipal_eye"": true,
    ""enable_vivesranipal_blink"": true,
    ""vivesranipal_eye_adjust_x"": 2.0,
    ""vivesranipal_eye_adjust_y"": 1.0,
    ""embedded_script"": {
        ""vr_debug"": true
    }
}";
        public const string BadString = @"
{

    ""item"": {
        ""hide_camera_from_viewers"": true,
        ""hide_camera_from_viewers.description"": ""カメラオブジェクトの(視聴者への)表示/非表示"",
 
        ""enable_displaycapture_chromakey"": true,
        ""enable_displaycapture_chromakey.description"": ""ディスプレイにクロマキーを適用する"",
 
        ""enable_nicovideo_chromakey"": true,
        ""enable_nicovideo_chromakey.description"": ""ニコニコ動画プレイヤーにクロマキーを適用する""
    },
    ""humanoid"": {
        ""use_fast_spring_bone"": true,
    },
    ""studio"": {
        ""allow_direct_view"": true,
        ""allow_direct_view.description"": ""ダイレクトビューモードでのスタジオ入室を許可するかどうか""
    },
    ""direct_view_talk"": true,
    ""enable_looking_glass"": true,
    ""embedded_script"": {
        ""vr_debug"": true
    }}
}";

        public const string ExpectedString = @"
{
    ""niconico"": {
        ""character_models"": [
            1,11
        ],
        ""background_models"": [
            2
        ],
        ""niconare_ids"": [
            4
        ],
        ""mylist_ids"": [
            3
        ],
        ""broadcaster_comments"": [
            ""コメント""
        ],
        ""ng_score_threshold"": -50
    },
    ""background"": {
        ""panorama"": {
        ""source_urls"": [""http://bg.foo/""]
    }
    },
    ""persistent_object"": {
        ""image_urls"": [""http://image.foo/""],
 
        ""double_sided_image_urls"": [
            [""http://dsfront.bar/"",""http://dsback.bar/""]
        ],
 
        ""hidden_image_urls"": [""http://hidden.foo/""],
 
        ""hidden_double_sided_image_urls"": [
            [""http://hdsfront.bar/"",""http://hdsback.bar/""]
        ],
 
        ""nicovideo_ids"": [""sm6""]
    },
    ""item"": {
        ""whiteboard"": {
            ""source_urls"": [""http://white.foo/""]
        },
        ""cue_card"": {
            ""source_urls"": [""http://cue.foo/""]
        },
        ""hide_camera_from_viewers"": false,
 
        ""enable_displaycapture_chromakey"": false,
 
        ""enable_nicovideo_chromakey"": false,

        ""capture_format"": ""PNG""
    },
    ""studio"": {
        ""allow_direct_view"": false
    },
    ""humanoid"": {
        ""use_fast_spring_bone"": false
    },
    ""mode"": ""direct-view"",
    ""direct_view_talk"": false,
    ""enable_looking_glass"": false,
    ""enable_vivesranipal_eye"": false,
    ""enable_vivesranipal_blink"": false,
    ""vivesranipal_eye_adjust_x"": null,
    ""vivesranipal_eye_adjust_y"": null,
    ""embedded_script"": {
        ""websocket_console_port"": 8080,
        ""vr_debug"": false,
        ""moonsharp_debugger_port"":8888
    }
}";
        public const string MergeString = @"
{
    ""niconico"": {
        ""character_models"": [
            1,11
        ],
        ""background_models"": [
            2
        ],
        ""foo"":""unknown""
    }
}";
        public const string MergeExpectedString = @"
{
    ""niconico"": {
        ""character_models"": [
            222,333
        ],
        ""background_models"": [
            
        ],
        ""foo"":""unknown"",
        ""niconare_ids"": [
        ],
        ""mylist_ids"": [
        ],
        ""broadcaster_comments"": [
        ],
        ""ng_score_threshold"":null
    },
    ""background"": {
        ""panorama"": {
        ""source_urls"": []
    }
    },
    ""persistent_object"": {
        ""image_urls"": [],
 
        ""double_sided_image_urls"": [
        ],
 
        ""hidden_image_urls"": [],
 
        ""hidden_double_sided_image_urls"": [
        ],
 
        ""nicovideo_ids"": []
    },
    ""item"": {
        ""whiteboard"": {
            ""source_urls"": []
        },
        ""cue_card"": {
            ""source_urls"": []
        },
        ""hide_camera_from_viewers"": false,
 
        ""enable_displaycapture_chromakey"": false,
 
        ""enable_nicovideo_chromakey"": false,

        ""capture_format"": null
    },
    ""studio"": {
        ""allow_direct_view"": false
    },
    ""humanoid"": {
        ""use_fast_spring_bone"": false
    },
    ""mode"":null,
    ""direct_view_talk"": false,
    ""enable_looking_glass"": false,
    ""enable_vivesranipal_eye"": false,
    ""enable_vivesranipal_blink"": false,
    ""vivesranipal_eye_adjust_x"": null,
    ""vivesranipal_eye_adjust_y"": null,
    ""embedded_script"": {
        ""websocket_console_port"": null,
        ""vr_debug"": false,
        ""moonsharp_debugger_port"":null
    }
}";    }
}
