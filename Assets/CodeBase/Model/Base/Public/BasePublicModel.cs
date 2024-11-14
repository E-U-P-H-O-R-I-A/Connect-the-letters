using System.IO;
using CodeBase.Data;

namespace CodeBase.Model.Base.Public
{
    public abstract class BasePublicModel<T> : IPublicModel where T : IPublicScheme
    {
        public T Data { get; protected set; }
        
        public abstract string Folder { get; }

        public virtual string ReadJson() => 
            File.ReadAllText(Folder);

        public void Parse(string jsonData) => 
            Data = jsonData.ToDeserialized<T>();
    }
}