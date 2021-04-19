using System;
using System.Collections.Generic;

namespace Frame
{
    public class PresenterManager:Singleton<PresenterManager>, ISingleton
    {
        private Dictionary<Type, IPresenter> _presenters = new Dictionary<Type, IPresenter>();

        public Dictionary<Type, IPresenter> Presenters => _presenters;
        
        public T GetPresenter<T>() where T : IPresenter, new()
        {
            var type = typeof(T);
            if (!_presenters.ContainsKey(type)) _presenters.Add(type, new T());
            return (T)_presenters[type];
        }
    }
}