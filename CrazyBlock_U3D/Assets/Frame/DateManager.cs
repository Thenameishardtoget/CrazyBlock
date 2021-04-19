using System;
using System.Collections.Generic;
using UnityEngine;

namespace Frame
{
    public class DateManager:Singleton<DateManager>, ISingleton
    {
        private long _curTime;
        private float _delta;

        private long _nextDay;
        private long _nextMonth;
        private long _nextYear;

        private List<Action<int>> _onNewDay = new List<Action<int>>();
        private List<Action<int>> _onNewMonth = new List<Action<int>>();
        private List<Action<int>> _onNewYear = new List<Action<int>>();
        private List<Action> _tickCB = new List<Action>();

        /// <summary>
        /// 时间1970
        /// </summary>
        public DateTime date1970 { get; private set; }
        
        /// <summary>
        /// 当前时间戳（s）
        /// </summary>
        public long curTime => _curTime;

        /// <summary>
        /// 当前日期
        /// </summary>
        public DateTime Now => date1970.AddSeconds(curTime);
        
        /// <summary>
        /// 下一天剩余秒数
        /// </summary>
        public long nextDay => _nextDay;
        /// <summary>
        /// 下一月剩余秒数
        /// </summary>
        public long nextMonth => _nextMonth;
        
        /// <summary>
        /// 下一年剩余秒数
        /// </summary>
        public long nextYear => _nextYear;

        public override void OnInit()
        {
            date1970 = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            SetNetTime((long)(DateTime.Now - date1970).TotalSeconds);
            
            FrameMono.Instance.AddUpdateAction(Instance.Update);
        }

        public void SetNetTime(long time)
        {
            if (time > 999999999999) time /= 1000;
            _curTime = time;
            DateTime cur = date1970.AddSeconds(_curTime);
            DateTime today = new DateTime(cur.Year, cur.Month, cur.Day);
            
            _nextDay = (long) (today.AddDays(1) - cur).TotalSeconds;
            _nextMonth = (long) (today.AddMonths(1) - cur).TotalSeconds;
            _nextYear = (long) (today.AddYears(1) - cur).TotalSeconds;
        }

        private void Update(float delta)
        {
            _delta = _delta + delta;
            if (_delta > 1)
            {
                _delta = _delta - 1;
                _curTime++;
                _nextDay--;
                _nextMonth--;
                _nextYear--;
                
                if (_tickCB.Count > 0) _tickCB.ForEach(cb => cb.Invoke());
                
                if (_nextDay == 0)
                {
                    DateTime cur = date1970.AddSeconds(_curTime);
                    DateTime today = new DateTime(cur.Year, cur.Month, cur.Day);
                    _nextDay = (long) (today.AddDays(1) - cur).TotalSeconds;
                    if(_onNewDay.Count > 0) _onNewDay.ForEach(cb=>cb.Invoke(cur.Day));
                }

                if (_nextMonth == 0)
                {
                    DateTime cur = date1970.AddSeconds(_curTime);
                    DateTime today = new DateTime(cur.Year, cur.Month, cur.Day);
                    _nextMonth = (long) (today.AddMonths(1) - cur).TotalSeconds;
                    if(_onNewMonth.Count > 0) _onNewMonth.ForEach(cb=>cb.Invoke(cur.Month));
                }

                if (_nextYear == 0)
                {
                    DateTime cur = date1970.AddSeconds(_curTime);
                    DateTime today = new DateTime(cur.Year, cur.Month, cur.Day);
                    _nextYear = (long) (today.AddYears(1) - cur).TotalSeconds;
                    if(_onNewYear.Count> 0) _onNewYear.ForEach(cb=>cb.Invoke(cur.Year));
                }
            }
        }

        public void AddTickCB(Action cb)
        {
            _tickCB.Add(cb);
        }

        public void RemoveTickCB(Action cb)
        {
            _tickCB.Remove(cb);
        }

        public void AddDayCB(Action<int> cb)
        {
            _onNewDay.Add(cb);
        }

        public void RemoveDayCB(Action<int> cb)
        {
            _onNewDay.Remove(cb);
        }

        public void AddMonthCB(Action<int> cb)
        {
            _onNewMonth.Add(cb);
        }

        public void RemoveMonthCB(Action<int> cb)
        {
            _onNewMonth.Remove(cb);
        }

        public void AddYearCB(Action<int> cb)
        {
            _onNewYear.Add(cb);
        }

        public void RemoveYearCB(Action<int> cb)
        {
            _onNewYear.Remove(cb);
        }
    }
}