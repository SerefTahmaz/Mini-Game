using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using NUnit.Framework;
using SimonSays.Managers.Config;
using UnityEngine;
using UnityEngine.TestTools;

namespace SimonSays.Tests
{
    /// <summary>
    /// <para>Unit test to verify configuration loading works</para>
    /// </summary>
    public class XMLConfigTestScript
    {
        private cGameConfiguration m_CorrectConfig;
        private TextAsset m_XMLText;
    
        [SetUp]
        public void Init()
        {
            m_XMLText = Resources.Load<TextAsset>("GameConfigs/Tests/XML/Test");
        
            var serializer = new XmlSerializer(typeof(cGameConfiguration));
            using(var reader = new System.IO.StringReader(m_XMLText.text))
            {
                m_CorrectConfig = (cGameConfiguration) serializer.Deserialize(reader);
            }
        }
    
        [Test]
        public void XMLConfigLoadTestPass()
        {
            var xmlGameConfig = new cXMLGameConfigHandler();
            var testConfig = xmlGameConfig.FileToConfig(m_XMLText);

            Assert.AreEqual(m_CorrectConfig,testConfig);
        }
    }
}