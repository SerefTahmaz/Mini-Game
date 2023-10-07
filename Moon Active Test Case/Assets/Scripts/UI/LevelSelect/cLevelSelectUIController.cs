using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Zenject;

public class cLevelSelectUIController : cView
{
    [SerializeField] private List<cGameLevelSO> m_GameLevels;
    [SerializeField] private Transform m_LayoutTransform;
    [SerializeField] private UnityEvent m_OnSelected;
    [SerializeField] private Image m_BG;
    [SerializeField] private Image m_Icon;
    
    private List<cLevelSelectButton> m_InsButtons = new List<cLevelSelectButton>();
    private cGameManagerStateMachine m_GameManager;
    private LevelSelectButtonFactory m_LevelSelectButtonFactory;
   

    [Inject]
    public void Initialize(cGameManagerStateMachine gameManager, LevelSelectButtonFactory levelSelectButtonFactory) 
    {
        m_GameManager = gameManager;
        m_LevelSelectButtonFactory = levelSelectButtonFactory;
    }

    private void Awake()
    {
        for (var index = 0; index < m_GameLevels.Count; index++)
        {
            var VARIABLE = m_GameLevels[index];
            var ins = m_LevelSelectButtonFactory.Create();
            ins.transform.SetParent(m_LayoutTransform);
            ins.transform.ResetTransform();
            ins.Init(VARIABLE, this);

            m_InsButtons.Add(ins);
        }
    }

    public override void Activate()
    {
        base.Activate();

        foreach (var VARIABLE in m_InsButtons)
        {
            VARIABLE.ResetState();
        }
        OnEnter(m_GameLevels[1]);
    }

    public void Selected(cGameLevelSO gameLevelSo)
    {
        m_GameManager.SetLevel(gameLevelSo.m_ConfigFile);
        m_OnSelected.Invoke();
    }

    public void OnEnter(cGameLevelSO gameLevelSo)
    {
        m_BG.color = gameLevelSo.m_Color;
        m_Icon.sprite = gameLevelSo.m_Icon;
        // m_Icon.color = Color.Lerp(gameLevelSo.m_Color, Color.white, 0);
    }
}
