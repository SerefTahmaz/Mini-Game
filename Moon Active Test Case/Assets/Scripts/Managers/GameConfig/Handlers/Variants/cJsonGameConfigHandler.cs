using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class cJsonGameConfigHandler : IGameConfigHandler
{
    public cGameConfiguration Load(TextAsset asset)
    {
        return JsonUtility.FromJson<cGameConfiguration>(asset.text);
    }

    public void CreateConfig(string path, cGameConfiguration gameConfiguration)
    {
        string savePlayerData = JsonUtility.ToJson(gameConfiguration);
        if (path.Length != 0)
        {
            File.WriteAllText(path,savePlayerData);
            AssetDatabase.Refresh();
        }
    }
}