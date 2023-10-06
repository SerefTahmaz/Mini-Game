using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class JsonConfigTestScript
{
    private cGameConfiguration m_CorrectConfig;
    private TextAsset m_JsonText;
    
    [SetUp]
    public void Init()
    {
        m_JsonText = Resources.Load<TextAsset>("GameConfigs/Json/Easy");
        m_CorrectConfig = JsonUtility.FromJson<cGameConfiguration>(m_JsonText.text);
    }
    
    // A Test behaves as an ordinary method
    [Test]
    public void JsonConfigLoadTestPasses()
    {
        var jsonGameConfig = new cJsonGameConfig();
        var testConfig = jsonGameConfig.Load(m_JsonText);

        Assert.AreEqual(testConfig,m_CorrectConfig);
    }
}
