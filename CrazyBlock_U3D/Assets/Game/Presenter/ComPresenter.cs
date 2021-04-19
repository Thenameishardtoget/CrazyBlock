using Frame;
using Game.LocalData;
using UnityEngine;

namespace Game.ModelCommon.Presenter
{
    public class ComPresenter:SPresenter
    {
        private DataBridge<GameData> b = new DataBridge<GameData>();
        protected override void OnInit()
        {
        }

        public void Test()
        {
            Debug.Log(b.m_data.aaa);
            b.m_data.aaa = 1;
            b.SetDirty(true);
        }
    }
}