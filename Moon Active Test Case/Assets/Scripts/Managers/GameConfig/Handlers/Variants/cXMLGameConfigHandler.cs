using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using UnityEditor;
using UnityEngine;

public class cXMLGameConfigHandler : IGameConfigHandler
{
    private static string m_FilePath => Application.dataPath + "/GameConfig.xml";
    
    public cGameConfiguration Load(TextAsset textAsset)
    {
        var serializer = new XmlSerializer(typeof(cGameConfiguration));
        using(var reader = new System.IO.StringReader(textAsset.text))
        {
            return (cGameConfiguration) serializer.Deserialize(reader);
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
}