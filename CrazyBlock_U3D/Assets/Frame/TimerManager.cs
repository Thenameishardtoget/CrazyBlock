using System;
using System.Collections.Generic;
using UnityEngine;

namespace Frame
{
    public class TimerManager:Singleton<TimerManager>, ISingleton
    {
      
        public override void OnInit()
        {
            FrameMono.Instance.AddUpdateAction(Instance.Update);
        }
        private void Update(float delta)
        {
            for (int i = 0; i < _list.Count; i++)
            {
                TimeInfo info = _list[i];
                if(info._pause) continue;
                info._time -= delta * (info.ignoreEngineScale ? 1 : Time.timeScale);
                if (info._time < 0)
                {
                    if (info.loop == 0)
                    {
                        _instances.Add(info.instanceID);
                        _list.Remove(info);
                    }
                    else
                    {
                        info._time = info.time;
                        if (info.loop > 0) info.loop--;
                    }
                    
                    if(info.cb != null) info.cb.Invoke();
                }
            }
        }
        
        private List<TimeInfo> _list = new List<TimeInfo>();
        private List<ulong> _instances = new List<ulong>();
        private ulong _max = 0;
        public ulong AddTimer(float space, Action cb, int loop = 0, float delay = 0,
            bool ignoreEngineScale = false)
        {
            TimeInfo info = new TimeInfo();
            info.instanceID = GetIntanceID();
            info.loop = loop;
            info.delay = delay;
            info.time = space + delay;
            info.ignoreEngineScale = ignoreEngineScale;
            info.cb = cb;
            return info.instanceID;
        }

        public ulong AddTimer(float space, Action cb, bool ignoreEngineScale)
        {
            return AddTimer(space, cb, 0, 0, ignoreEngineScale);
        }

        public ulong AddTimer(float delay, float space, Action cb, bool ignoreEngineScale = false)
        {
            return AddTimer(space, cb, 0, delay, ignoreEngineScale);
        }

        public bool HasTimer(ulong id)
        {
            return _list.Find(ele => ele.instanceID == id) != null;
        }

        public void PauseTimer(ulong id)
        {
            TimeInfo info = _list.Find(ele => ele.instanceID == id);
            if (info != null) info._pause = true;
        }

        public void Resume(ulong id)
        {
            TimeInfo info = _list.Find(ele => ele.instanceID == id);
            if (info != null) info._pause = false;
        }

        public void StopTimer(ulong id)
        {
            TimeInfo info = _list.Find(ele => ele.instanceID == id);
            if (info != null) _list.Remove(info);
        }

        private ulong GetIntanceID()
        {
            ulong id;
            if (_instances.Count > 0)
            {
                id = _instances[0];
                _instances.RemoveAt(0);
            }
            else
            {
                id = _max++;
            }

            return id;
        }
        
        private class TimeInfo
        {
            public ulong instanceID;
            public int loop;
            public float delay;
            public float time;
            public Action cb;
            public bool ignoreEngineScale;
            
            public float _time;
            public bool _pause;
        }
    }
}