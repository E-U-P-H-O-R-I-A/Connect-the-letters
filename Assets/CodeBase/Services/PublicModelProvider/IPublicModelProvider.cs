using CodeBase.Model;

namespace CodeBase.Services.PublicModelProvider
{
    public interface IPublicModelProvider
    {
        public T Get<T>() where T : class, IPublicModel;
    }
}