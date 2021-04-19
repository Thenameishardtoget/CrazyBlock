using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Tools : Editor
{
    [MenuItem("Tools/清除所有存档")]
    private static void DeletePlayPrefs()
    {
        PlayerPrefs.DeleteAll();
    }

}
