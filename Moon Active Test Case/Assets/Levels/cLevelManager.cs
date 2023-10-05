using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cLevelManager : cSingleton<cLevelManager>
{
    [SerializeField] private GameObject[] m_LevelPrefabs;
    [SerializeField] private GameObject m_TestLevel;
    private GameObject m_Level;

    public cLevel m_CurrentLevel;
    
    public void LoadCurrentLevel()
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

        m_Level = Instantiate(levelPrefab, transform);
        m_CurrentLevel = m_Level.GetComponent<cLevel>();
    }

    public GameObject GetCurrentLevel()
    {
        return m_Level;
    }

    public void RemoveLevel()
    {
        if ( m_Level != null )
        {
            Destroy( m_Level.gameObject );
        }
    }
}