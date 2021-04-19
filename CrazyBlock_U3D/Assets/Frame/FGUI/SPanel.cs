using System;
using System.Linq;
using FairyGUI;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace Frame.FGUI
{
    public abstract class SPanel:IPanel,IDisposable
    {
        public abstract string packageName { get; }
        public abstract string componentName { get; }

        protected GComponent ComRoot;
        
        private int _delayCnt;
        public virtual void Init()
        {
            UIPackage.AddPackage(packageName);
            ComRoot = UIPackage.CreateObject(packageName, componentName).asCom;
            _delayCnt = 0;
            if(ComRoot._transitions != null)
            foreach (var tran in ComRoot._transitions.Where(tran => tran.autoPlay))
            {
                _delayCnt++;
                tran.onComplete = EndPlay;
            }

            GRoot.inst.AddChild(ComRoot);
        }

        public virtual void Open()
        {
            
        }

        public virtual void Close()
        {
#pragma warning disable 612, 618
            PanelManager.Instance.OnPanelClose(this);
#pragma warning restore  612, 618
        }

        private void EndPlay()
        {
            _delayCnt--;
            if (_delayCnt <= 0)
            {
                Debug.Log("Auto Complete");
            }
        }

        public void Dispose()
        {
            ComRoot.Dispose();
        }
    }
}