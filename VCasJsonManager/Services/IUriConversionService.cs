//
// VCasJsonManager
// Copyright 2019 TOMA
// MIT License
//
using System;

namespace VCasJsonManager.Services
{
    /// <summary>
    /// URIの変換処理を行うインターフェース
    /// </summary>
    public interface IUriConversionService
    {
        string BuildMylistUrl(int id);
        string BuildNico3dUrl(int id);
        string BuildNiconareUrl(int id);
        string BuildNicoViedoUrl(string id);
        Uri BuildUri(string input);
        int? ExtractMylistId(string url);
        int? ExtractNico3dId(string url);
        int? ExtractNiconareId(string url);
        string ExtractNicoVideoId(string url);
    }
}
