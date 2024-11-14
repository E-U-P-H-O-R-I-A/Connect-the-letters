using CodeBase.Data;
using CodeBase.Model.Base.Private;
using UnityEngine;

namespace CodeBase.Model.Private
{
    public abstract class BasePrivateModel<T> : IPrivateModel  where T : IPrivateScheme, new()
    {
        public abstract string Key { get; }
        public T Data { get; set; }
        
        public void SaveProgress() => 
            PlayerPrefs.GetString(Key, Data.ToJson());

        public void LoadProgress()
        {
            string savedData = PlayerPrefs.GetString(Key, null);
            Data = savedData != null && savedData != string.Empty ? savedData.ToDeserialized<T>() : new T();
        }
    }
}