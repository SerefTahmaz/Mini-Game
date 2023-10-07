using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Dan.Main;
using UnityEditor;
using UnityEngine;

public static class cSaveDataHandler
{
    private static SaveData m_GameConfiguration = new SaveData();
    public static SaveData GameConfiguration
    {
        get
        {
            Load();
            return m_GameConfiguration;
        }
        set
        {
            m_GameConfiguration = value;
        }
    }

    private static bool m_Loaded = false;

    private static string m_SaveFilePath => Application.persistentDataPath + "/SavaData.json";

    private static void Load(){
        if(m_Loaded) return;
        
        if (File.Exists(m_SaveFilePath))
        {
            string loadPlayerData = File.ReadAllText(m_SaveFilePath);
            GameConfiguration = JsonUtility.FromJson<SaveData>(loadPlayerData);
  
            Debug.Log("Load game complete!");
            m_Loaded = true;
        }
        else
            Debug.Log("There is no save files to load!");
    }


    public static void Save()
    {
        string savePlayerData = JsonUtility.ToJson(GameConfiguration);
        File.WriteAllText(m_SaveFilePath, savePlayerData);

        Debug.Log("Save file created at: ");
    }


    [MenuItem("SaveData/DeleteSaveData")]
    public static void DeleteSaveFile()
    {
        if (File.Exists(m_SaveFilePath))
        {
            File.Delete(m_SaveFilePath);
            GameConfiguration = new SaveData();
  
            Debug.Log("Save file deleted!");
        }
        else
            Debug.Log("There is nothing to delete!");

        m_Loaded = false;
    }

    public static string PlayerName()
    {
        return GameConfiguration.PlayerName;
    }
}
