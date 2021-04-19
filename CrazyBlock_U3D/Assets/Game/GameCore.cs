using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using DG.Tweening;
using Game.ModelCommon;
using Game.ModelCommon.Presenter;
using UnityEngine;

public class GameCore
{
    private static Transform last;
    public static GameObject CreateBlock()
    {
        GameObject go = GameObject.Instantiate(Resources.Load<GameObject>("Cube"));
        return go;
    }

    public static void Move()
    {
        GameObject go = CreateBlock();
        
        if (last)
        {
            Vector3 start = new Vector3(20, last.position.y + go.transform.lossyScale.y + 5, 100);
            Vector3 end = new Vector3(-20, last.position.y + go.transform.lossyScale.y + 5, 100);
            go.transform.position = start;
            var tween = go.transform.DOMove(end, 5);
            tween.SetLoops(-1, LoopType.Yoyo);
            tween.SetEase(Ease.Linear);
            var rig = go.GetComponent<Rigidbody>();
            rig.useGravity = false;
        }
        else
        {
            go.transform.position = new Vector3(0, -100, 100);
            var rig = go.GetComponent<Rigidbody>();
            rig.freezeRotation = true;
            rig.useGravity = false;
            go.isStatic = true;
        }
        
        last = go.transform;
        var presenter = GameModel.Instance.GetPresenter<GamePresenter>();
        presenter.GameState = GameState.Move;
    }

    public static void Drop()
    {
        var presenter = GameModel.Instance.GetPresenter<GamePresenter>();
        presenter.GameState = GameState.Drop;
        last.DOKill();
        if(!last.gameObject.isStatic)last.GetComponent<Rigidbody>().useGravity = true;
    }

    public static void GameOver()
    {
        var presenter = GameModel.Instance.GetPresenter<GamePresenter>();
        presenter.GameState = GameState.Over;
    }
}
