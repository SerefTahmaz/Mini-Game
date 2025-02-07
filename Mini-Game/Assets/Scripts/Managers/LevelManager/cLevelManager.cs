using System;
using System.Collections;
using System.Collections.Generic;
using SimonSays.Managers.Config;
using SimonSays.Managers.GameManager;
using SimonSays.Managers.SaveManager;
using UnityEngine;
using Zenject;

namespace SimonSays.Managers
{
    public class cLevelManager : MonoBehaviour, ILevelManager
    {
        [SerializeField] private cLevel[] m_LevelPrefabs;
        [SerializeField] private cLevel m_TestLevel;
        private cLevel m_Level;
        private IInstantiator m_Instantiator;
        private cGameManagerStateMachine m_GameManager;
        private ISaveManager m_SaveManager;

        public cLevel Level => m_Level;

        [Inject]
        public void Initialize(IInstantiator instantiator, cGameManagerStateMachine gameManager, ISaveManager saveManager) {
            m_Instantiator = instantiator;
            m_GameManager = gameManager;
            m_SaveManager = saveManager;
        }
    
        public void LoadCurrentLevel(cGameConfiguration gameConfiguration)
        {
            if ( Level != null )
            {
                Destroy( Level.gameObject );
            }

            var currentLevel = m_SaveManager.SaveData.m_CurrentLevel;
            GameObject levelPrefab = m_LevelPrefabs[currentLevel % m_LevelPrefabs.Length].gameObject;
        
#if UNITY_EDITOR
            if ( m_TestLevel != null )
            {
                levelPrefab = m_TestLevel.gameObject;
            }
#endif

            m_Level = m_Instantiator.InstantiatePrefabForComponent<cLevel>(levelPrefab, transform);
            Level.InitLevel(gameConfiguration);
        }

        public void RemoveCurrentLevel()
        {
            if ( Level != null )
            {
                m_GameManager.GameEvents.OnGameStartBeforeLevelDestroy.Invoke();
                Destroy( Level.gameObject );
            }
        }
    }
}