using UnityEngine;

namespace Frame
{
    public abstract class SPresenter : IPresenter
    {
        internal SPresenter()
        {
            DateManager.Instance.AddTickCB(OnTick);
            DateManager.Instance.AddDayCB(OnNewDay);
            DateManager.Instance.AddMonthCB(OnNewMonth);
            DateManager.Instance.AddYearCB(OnNewYear);
            OnInit();
        }

        protected virtual void OnInit(){}
        protected virtual void OnTick(){}
        protected virtual void OnNewDay(int day){}
        protected virtual  void OnNewMonth(int month){}
        protected virtual void OnNewYear(int obj) { }
    
    }
}