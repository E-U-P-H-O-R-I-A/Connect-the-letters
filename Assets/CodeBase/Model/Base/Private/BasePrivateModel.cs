using CodeBase.Data;
using CodeBase.Model.Base.Private;
using UnityEngine;

namespace CodeBase.Model.Private
{
    public abstract class BasePrivateModel<T> : IPrivateModel  where T : IPrivateScheme
    {
        public abstract string Key { get; }
        private T Data { get; set; }
        
        public void SaveProgress() => 
            PlayerPrefs.GetString(Key, Data.ToJson());

        public void LoadProgress() => 
            Data = PlayerPrefs.GetString(Key).ToDeserialized<T>();
    }
}