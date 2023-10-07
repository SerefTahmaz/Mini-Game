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

    [MenuItem("GameConfig/Template Json Config")]
    public static void CreateTemplateConfig()
    {
        string savePlayerData = JsonUtility.ToJson(new cGameConfiguration());

        var path = EditorUtility.SaveFilePanel(
            "Save config",
            "",
            "GameConfig" + ".json",
            "json");

        if (path.Length != 0)
        {
            File.WriteAllText(path,savePlayerData);
            AssetDatabase.Refresh();
        }
    }
}