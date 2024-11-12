using System.Collections.Generic;
using System.IO;
using CodeBase.Data;
using CodeBase.Model.Public;

namespace CodeBase.Model
{
    public abstract class BasePublicModel<T> : IPublicModel where T : IPublicScheme
    {
        public T Data { get; private set; }
        public abstract string Folder { get; }

        public virtual string ReadJson() => 
            File.ReadAllText(Folder);

        public void Parse(string jsonData) => 
            Data = jsonData.ToDeserialized<T>();
    }
}