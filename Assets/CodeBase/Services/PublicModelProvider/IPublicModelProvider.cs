using CodeBase.Model;
using CodeBase.Model.Base.Public;

namespace CodeBase.Services.PublicModelProvider
{
    public interface IPublicModelProvider
    {
        public T Get<T>() where T : class, IPublicModel;
    }
}