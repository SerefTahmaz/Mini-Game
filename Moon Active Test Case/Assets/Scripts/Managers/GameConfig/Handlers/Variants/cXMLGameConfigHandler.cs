using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using UnityEditor;
using UnityEngine;

public class cXMLGameConfigHandler : IGameConfigHandler
{
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
        var path = EditorUtility.SaveFilePanel(
            "Save config",
            "",
            "GameConfig" + ".xml",
            "xml");

        if (path.Length != 0)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(cGameConfiguration));
 
            using (FileStream stream = new FileStream(path, FileMode.Create))
            {
                serializer.Serialize(stream, new cGameConfiguration());
            }
            AssetDatabase.Refresh();
        }
    }
}