using System.Collections.Generic;
using CodeBase.Model;
using Zenject;

namespace CodeBase.Services.PublicModelProvider
{
    public class PublicModelProvider : IPublicModelProvider
    {
        private List<IPublicModel> _dataModels;

        [Inject]
        public void Construct(List<IPublicModel> models) => 
            _dataModels = models;

        public T Get<T>() where T : class, IPublicModel => 
            _dataModels.Find(x => x is T) as T;
    }
}