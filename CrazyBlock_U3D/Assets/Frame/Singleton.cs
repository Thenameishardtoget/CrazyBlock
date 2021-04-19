using System.Collections;
using System.Collections.Generic;
using Frame;
using UnityEngine;

public abstract class Singleton<T> where T : ISingleton, new()
{
    public static T Instance;

    static Singleton()
    {
        Instance = new T();
        Instance.OnInit();
    }
    
    public virtual void OnInit(){}
}