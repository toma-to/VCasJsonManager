//
// VCasJsonManager
// Copyright 2019 TOMA
// MIT License
//
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using VCasJsonManager.Models;

namespace VCasJsonManager.Services
{
    public interface ICollectionManageService<T> : INotifyPropertyChanged, INotifyDataErrorInfo, IDisposable
    {
        ObservableCollection<T> Collection { get; }
        ConfigJson ConfigJson { get; }
        string InputValue { get; set; }

        void AddNewItem();
        void BrowsItem(T item);
        void RemoveAll();
        void RemoveAt(int index);
    }
}
