using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

[Serializable]
public class cGameConfiguration
{
    public int m_ButtonCount=4;
    public int m_EachStepPointCount=1;
    public int m_GameTimeInSeconds=50;
    public bool m_RepeatMode=true;
    public float m_GameSpeed = 1;

    public override string ToString()
    {
        return $"button Count: {m_ButtonCount}," +
               $"Each Step Point Count: {m_EachStepPointCount}," +
               $"Game Time In Seconds Count: {m_GameTimeInSeconds}," +
               $"Repeat Mode: {m_RepeatMode}," +
               $"Game Speed: {m_GameSpeed},";
    }
}

public class jsonGameData : IGameConfig
{
    private cGameConfiguration m_GameConfiguration = new cGameConfiguration();
    public cGameConfiguration GameConfiguration
    {
        get => m_GameConfiguration;
        set => m_GameConfiguration = value;
    }
    private bool m_Loaded = false;

    private string saveFilePath => Application.dataPath + "/GameConfigData.json";

    public void Load(){
        if(m_Loaded) return;
        
        if (File.Exists(saveFilePath))
        {
            string loadPlayerData = File.ReadAllText(saveFilePath);
            GameConfiguration = JsonUtility.FromJson<cGameConfiguration>(loadPlayerData);
  
            Debug.Log("Load game complete!");
            m_Loaded = true;
        }
        else
            Debug.Log("There is no save files to load!");
    }
    
    public cGameConfiguration Load(TextAsset asset)
    {
        return JsonUtility.FromJson<cGameConfiguration>(asset.text);
    }


    public void Save()
    {
        string savePlayerData = JsonUtility.ToJson(GameConfiguration);
        File.WriteAllText(saveFilePath, savePlayerData);
  
        Debug.Log("Save file created at: ");
    }
    
    
    public void DeleteSaveFile()
    {
        if (File.Exists(saveFilePath))
        {
            File.Delete(saveFilePath);
  
            Debug.Log("Save file deleted!");
        }
        else
            Debug.Log("There is nothing to delete!");
    }
}