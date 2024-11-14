using System.Collections.Generic;
using System.Linq;
using CodeBase.Model.Base.Private;
using CodeBase.Services.PrivateModelProvider;
using Zenject;

namespace CodeBase.Services.PublicModelProvider
{
    public class PrivateModelProvider : IPrivateModelProvider
    {
        private List<IPrivateModel> _dataModels;

        [Inject]
        public void Construct(List<IPrivateModel> models) => 
            _dataModels = models;

        public T Get<T>() where T : class, IPrivateModel => 
            _dataModels.Find(x => x is T) as T;

        public List<IPrivateModel> GetAllModels() => 
            _dataModels.ToList();
    }
}