using System;
using System.Collections.Generic;
using UnityEngine;

namespace Frame
{
    public class LocalDataManager : Singleton<LocalDataManager>, ISingleton
    {

        private Dictionary<Type, IData> _datas = new Dictionary<Type, IData>();
        private List<IBridge> _bridges = new List<IBridge>();

        private bool hasSaveData = false;
        private float saveSpace = 0.3f;
        private float saveDis = -1f;
        public override void OnInit()
        {
            FrameMono.Instance.AddUpdateAction(Instance.Update);
        }

        private void Update(float deltaTime)
        {
            if (saveDis < 0)
            {
                saveDis = saveSpace;
                if (_bridges.Count > 0)
                {
                    _bridges.ForEach(bridge =>
                    {
                        if (bridge != null && bridge.IsDirty())
                        {
#pragma warning disable 612, 618
                            bridge.__Save();
#pragma warning restore  612, 618
                            hasSaveData = true;
                        }
                    });

                    if (hasSaveData) PlayerPrefs.Save();
                }
            }
            else
            {
                saveDis = saveDis - deltaTime;
            }
        }

        public void SaveLocalData<T>(T data) where T : IData, new()
        {
            if(data == null) return;
            string str = JsonUtility.ToJson(data);
            PlayerPrefs.SetString("Frame_" + typeof(T).ToString(), str);
        }
        
        public T GetLocalData<T>() where T : IData, new()
        {
            var type = typeof(T);
            if (_datas.ContainsKey(type)) return (T) _datas[type];

            string str = PlayerPrefs.GetString("Frame_" + type.ToString());
            _datas.Add(type, string.IsNullOrEmpty(str) ? new T() : JsonUtility.FromJson<T>(str));
            return (T) _datas[type];
        }

        public void AddBridge<T>(DataBridge<T> dataBridge) where T : IData, new()
        {
            _bridges.Add(dataBridge);
        }

        public void RemoveBridge<T>(DataBridge<T> dataBridge) where T : IData, new()
        {
            _bridges.Remove(dataBridge);
        }
    }
}
