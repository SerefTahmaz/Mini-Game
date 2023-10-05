using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using UnityEngine;

public class cXmlExample : IGameConfig
{
    public cGameConfiguration GameConfiguration { get; set; }

    private string m_FilePath => Application.dataPath + "/ObjectData.xml";

    [ContextMenu("save")]
    public void Save()
    {
        XmlSerializer serializer = new XmlSerializer(typeof(cGameConfiguration));
 
        using (FileStream stream = new FileStream(m_FilePath, FileMode.Create))
        {
            serializer.Serialize(stream, GameConfiguration);
        }
    }
 
    [ContextMenu("load")]
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
        return null;
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

public interface IGameConfig
{
    public cGameConfiguration GameConfiguration { get; set; }
    public void Save();
    public void Load();
    public cGameConfiguration Load(TextAsset textAsset);
    public void DeleteSaveFile();
}