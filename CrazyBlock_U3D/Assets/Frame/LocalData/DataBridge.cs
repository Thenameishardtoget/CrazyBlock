using System;
using UnityEngine;

namespace Frame
{
    public class DataBridge<T> : IBridge, IDisposable where T : IData, new()
    {
        public T m_data;
        private bool isDirty = false;

        public DataBridge()
        {
            m_data = LocalDataManager.Instance.GetLocalData<T>();
            LocalDataManager.Instance.AddBridge(this);
        }

        ~DataBridge()
        {
            Dispose();
        }

        public void Dispose()
        {
            LocalDataManager.Instance.RemoveBridge(this);
            GC.SuppressFinalize(this);
        }

        public bool IsDirty()
        {
            return isDirty;
        }

        [Obsolete("This is a frame function, please replace by 'SetDirty'")]
        public void __Save()
        {
            LocalDataManager.Instance.SaveLocalData(m_data);
        }


        public void SetDirty(bool immediately = false)
        {
            if (immediately)
            {
                LocalDataManager.Instance.SaveLocalData(m_data);
                PlayerPrefs.Save();
                isDirty = false;
            }
            else
            {
                isDirty = true;
            }
        }
    }
}