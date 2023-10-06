using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class XMLConfigTestScript
{
    private cGameConfiguration m_CorrectConfig;
    private TextAsset m_XMLText;
    
    [SetUp]
    public void Init()
    {
        m_XMLText = Resources.Load<TextAsset>("GameConfigs/XML/Easy");
        
        var serializer = new XmlSerializer(typeof(cGameConfiguration));
        using(var reader = new System.IO.StringReader(m_XMLText.text))
        {
            m_CorrectConfig = (cGameConfiguration) serializer.Deserialize(reader);
        }
    }
    
    // A Test behaves as an ordinary method
    [Test]
    public void XMLConfigLoadTestPasses()
    {
        var xmlGameConfig = new cXmlGameConfig();
        var testConfig = xmlGameConfig.Load(m_XMLText);

        Assert.AreEqual(testConfig,m_CorrectConfig);
    }
}
