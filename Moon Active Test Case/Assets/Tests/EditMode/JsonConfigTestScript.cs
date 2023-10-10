using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using SimonSays.Managers.Config;
using UnityEngine;
using UnityEngine.TestTools;

namespace SimonSays.Tests
{
    /// <summary>
    /// <para>Unit test to verify configuration loading works</para>
    /// </summary>
    public class JsonConfigTestScript
    {
        private cGameConfiguration m_CorrectConfig;
        private TextAsset m_JsonText;
    
        [SetUp]
        public void Init()
        {
            m_JsonText = Resources.Load<TextAsset>("GameConfigs/Tests/Json/Test");
            m_CorrectConfig = JsonUtility.FromJson<cGameConfiguration>(m_JsonText.text);
        }
    
        [Test]
        public void JsonConfigLoadTestPass()
        {
            var jsonGameConfig = new cJsonGameConfigHandler();
            var testConfig = jsonGameConfig.FileToConfig(m_JsonText);

            Assert.AreEqual(m_CorrectConfig, testConfig);
        }
    }
}