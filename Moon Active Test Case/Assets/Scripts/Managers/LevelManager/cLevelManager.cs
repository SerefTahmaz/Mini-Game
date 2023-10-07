using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class cLevelManager : MonoBehaviour, ILevelManager
{
    [SerializeField] private GameObject[] m_LevelPrefabs;
    [SerializeField] private GameObject m_TestLevel;
    private GameObject m_Level;
    private IInstantiator m_Instantiator;
    private cGameManagerStateMachine m_GameManager;

    public cLevel m_CurrentLevel;
    
    [Inject]
    public void Initialize(IInstantiator instantiator, cGameManagerStateMachine gameManager) {
        m_Instantiator = instantiator;
        m_GameManager = gameManager;
    }
    
    public void LoadCurrentLevel(cGameConfiguration gameConfiguration)
    {
        if ( m_Level != null )
        {
            Destroy( m_Level.gameObject );
        }
        
        GameObject levelPrefab = m_LevelPrefabs[0% m_LevelPrefabs.Length].gameObject;
        
#if UNITY_EDITOR
        if ( m_TestLevel != null )
        {
            levelPrefab = m_TestLevel.gameObject;
        }
#endif

        m_Level = m_Instantiator.InstantiatePrefab(levelPrefab, transform);
        m_CurrentLevel = m_Level.GetComponent<cLevel>();
        m_CurrentLevel.InitLevel(gameConfiguration);
    }

    public void RemoveCurrentLevel()
    {
        if ( m_Level != null )
        {
            m_GameManager.GameEvents.OnGameStartBeforeLevelDestroy.Invoke();
            m_GameManager.GameEvents.OnGameStartBeforeLevelDestroy= delegate {  };
            Destroy( m_Level.gameObject );
        }
    }
}