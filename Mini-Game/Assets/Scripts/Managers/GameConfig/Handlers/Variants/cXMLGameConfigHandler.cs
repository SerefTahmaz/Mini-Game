using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using UnityEditor;
using UnityEngine;

namespace SimonSays.Managers.Config
{
    /// <summary>
    ///   <para>Handles converting between XML file and game configuration</para>
    /// </summary>
    public class cXMLGameConfigHandler : IGameConfigHandler
    {
        public cGameConfiguration FileToConfig(TextAsset textAsset)
        {
            var serializer = new XmlSerializer(typeof(cGameConfiguration));
            using(var reader = new System.IO.StringReader(textAsset.text))
            {
                return (cGameConfiguration) serializer.Deserialize(reader);
            }
        }
    
        public void ConfigToFile(string path,cGameConfiguration gameConfiguration)
        {
            if (path.Length != 0)
            {
                XmlSerializer serializer = new XmlSerializer(typeof(cGameConfiguration));
 
                using (FileStream stream = new FileStream(path, FileMode.Create))
                {
                    serializer.Serialize(stream, gameConfiguration);
                }
                AssetDatabase.Refresh();
            }
        }
    }
}