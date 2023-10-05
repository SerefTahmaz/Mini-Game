using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using UnityEditor;
using UnityEngine;

public class cXmlGameConfig : IGameConfig
{
    private cGameConfiguration m_GameConfiguration = new cGameConfiguration();
    public cGameConfiguration GameConfiguration
    {
        get => m_GameConfiguration;
        set => m_GameConfiguration = value;
    }

    private static string m_FilePath => Application.dataPath + "/ObjectData.xml";

    public void Save()
    {
        XmlSerializer serializer = new XmlSerializer(typeof(cGameConfiguration));
 
        using (FileStream stream = new FileStream(m_FilePath, FileMode.Create))
        {
            serializer.Serialize(stream, GameConfiguration);
        }
    }
 
    public void Load()
    {
        XmlSerializer serializer = new XmlSerializer(typeof(cGameConfiguration));
 
        using (FileStream stream = new FileStream(m_FilePath, FileMode.Open))
        {
            GameConfiguration = serializer.Deserialize(stream) as cGameConfiguration;
 
            Debug.Log(GameConfiguration);
        }
    }

    public cGameConfiguration Load(TextAsset textAsset)
    {
        var serializer = new XmlSerializer(typeof(cGameConfiguration));
        using(var reader = new System.IO.StringReader(textAsset.text))
        {
            return serializer.Deserialize(reader) as cGameConfiguration;
        }
    }
    
     [MenuItem("GameConfig/Template XML Config")]
    public static void CreateTemplateConfig()
    {
        XmlSerializer serializer = new XmlSerializer(typeof(cGameConfiguration));
 
        using (FileStream stream = new FileStream(m_FilePath, FileMode.Create))
        {
            serializer.Serialize(stream, new cGameConfiguration());
        }
  
        Debug.Log("Save file created at: " + m_FilePath);
        AssetDatabase.Refresh();
    }


    public void DeleteSaveFile()
    {
        if (File.Exists(m_FilePath))
        {
            File.Delete(m_FilePath);
  
            Debug.Log("Save file deleted!");
        }
        else
            Debug.Log("There is nothing to delete!");
    }
}