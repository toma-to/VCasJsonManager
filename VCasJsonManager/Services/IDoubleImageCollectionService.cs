//
// VCasJsonManager
// Copyright 2019 TOMA
// MIT License
//
using System;
using VCasJsonManager.Models;

namespace VCasJsonManager.Services
{
    public interface IDoubleImageCollectionService : ICollectionManageService<ConfigJson.DoubleImageUrls>
    {
        string AnotherInputValue { get; set; }

        void BrowsItem(Uri item);
    }
}
