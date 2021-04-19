using System;
using Frame;
using Game.ModelCommon.Presenter;
using UnityEngine;

namespace Game.ModelCommon
{
    public class CommonModel:SModel<CommonModel>, IModel, ISingleton
    {
        private ComPresenter comP = PresenterManager.Instance.GetPresenter<ComPresenter>();
 
        public override void OnInit()
        {
            base.OnInit();
            
        }

        public override void OnRegister()
        {
            base.OnRegister();
        }

        public override void OnUnregister()
        {
            base.OnUnregister();
        }

        public override TM GetPresenter<TM>()
        {
            return (TM)(object)comP;
        }
    }
}