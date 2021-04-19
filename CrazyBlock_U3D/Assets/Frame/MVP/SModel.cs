using System.ComponentModel;
using UnityEngine;

namespace Frame
{
    public abstract class SModel<T> : Singleton<T> where T : IModel, ISingleton, new()
    {
        public override void OnInit()
        {
            ModelManager.Instance.RegisterModel(Instance);
        }

        public virtual void OnRegister()
        {
            Debug.Log("SModel<T>  OnRegister");
        }

        public virtual void OnUnregister()
        {
        }

        public abstract TM GetPresenter<TM>() where TM : IPresenter, new();
    }
}