using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrameMono : MonoBehaviour
{
    private static FrameMono _instance;

    public static FrameMono Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject go = new GameObject("FrameMono");
                _instance = go.AddComponent<FrameMono>();
            }

            return _instance;
        }
    }

    private List<Action<float>> _updateActions = new List<Action<float>>();
    private bool hasNull;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_updateActions.Count > 0)
        {
            hasNull = false;
            for (int i = 0; i < _updateActions.Count; i++)
            {
                if (_updateActions[i] != null) _updateActions[i].Invoke(Time.deltaTime);
                else hasNull = true;
            }

            if (hasNull)
            {
                for (int i = _updateActions.Count - 1; i >= 0; i--)
                {
                    if(_updateActions[i] == null) _updateActions.RemoveAt(i);
                }
            }
        }
    }

    public void AddUpdateAction(Action<float> action)
    {
        if(!_updateActions.Contains(action))_updateActions.Add(action);
    }
}
