using CodeBase.Model.Base.Private;

namespace CodeBase.Services.PrivateModelProvider
{
    public interface IPrivateModelProvider
    {
        public T Get<T>() where T : class, IPrivateModel;
    }
}