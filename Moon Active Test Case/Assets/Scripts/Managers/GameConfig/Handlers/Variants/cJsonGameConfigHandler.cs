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
    private static string saveFilePath => Application.dataPath + "/GameConfigData.json";

    public cGameConfiguration Load(TextAsset asset)
    {
        return JsonUtility.FromJson<cGameConfiguration>(asset.text);
    }

    [MenuItem("GameConfig/Template Json Config")]
    public static void CreateTemplateConfig()
    {
        string savePlayerData = JsonUtility.ToJson(new cGameConfiguration());
        File.WriteAllText(saveFilePath, savePlayerData);
  
        Debug.Log("Save file created at: " + saveFilePath);
        AssetDatabase.Refresh();
    }
}