using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using FairyGUI;
using Frame;
using Frame.FGUI;
using Game.ModelCommon;
using Game.ModelCommon.Presenter;
using UnityEngine;

public class Main : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Start");

        ComPresenter cc = CommonModel.Instance.GetPresenter<ComPresenter>();
        
        Debug.Log(UIPackage.AddPackage("Test").name);
        
        GameCore.Move();    
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            PanelManager.Instance.Open<TestPanel>();
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (GameModel.Instance.GamePresenter.GameState != GameState.Move)
            {
                GameCore.Move();    
            }
            else
            {
                GameCore.Drop();
            }
            
        }
    }
}
