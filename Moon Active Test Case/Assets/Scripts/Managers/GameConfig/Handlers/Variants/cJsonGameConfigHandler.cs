using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

/// <summary>
///   <para>Handles converting between JSON file and game configuration</para>
/// </summary>
public class cJsonGameConfigHandler : IGameConfigHandler
{
    public cGameConfiguration FileToConfig(TextAsset asset)
    {
        return JsonUtility.FromJson<cGameConfiguration>(asset.text);
    }

    public void ConfigToFile(string path, cGameConfiguration gameConfiguration)
    {
        string savePlayerData = JsonUtility.ToJson(gameConfiguration);
        if (path.Length != 0)
        {
            File.WriteAllText(path,savePlayerData);
            AssetDatabase.Refresh();
        }
    }
}