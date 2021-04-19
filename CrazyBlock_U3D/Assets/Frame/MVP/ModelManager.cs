using System.Collections.Generic;

namespace Frame
{
    public class ModelManager:Singleton<ModelManager>, ISingleton
    {
        private List<IModel> _modles = new List<IModel>();
        
        public void RegisterModel<T>(T model) where T :  IModel
        {
            if (!_modles.Contains(model))
            {
                _modles.Add(model);
                model.OnRegister();
            }
        }

        public void UnregisterModel<T>(T model) where T:IModel
        {
            if (_modles.Contains(model))
            {
                _modles.Remove(model);
                model.OnUnregister();
            }
        }
    }
}