using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class GameCore
{
    private static Transform last;
    public static void CreateBlock()
    {
        GameObject go = GameObject.Instantiate(Resources.Load<GameObject>("Cube"));
        if (last)
        {
            last.DOKill();
            if(!last.gameObject.isStatic)last.GetComponent<Rigidbody>().useGravity = true;
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
    }
}
