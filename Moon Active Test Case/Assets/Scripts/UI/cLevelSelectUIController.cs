using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class cLevelSelectUIController : cView
{
    [SerializeField] private List<cGameLevelSO> m_GameLevels;
    [SerializeField] private cLevelSelectButton m_LevelSelectButton;
    [SerializeField] private Transform m_LayoutTransform;
    [SerializeField] private UnityEvent m_OnSelected;
    [SerializeField] private Image m_BG;
    [SerializeField] private Image m_Icon;

    private List<cLevelSelectButton> m_InsButtons = new List<cLevelSelectButton>();

    private void Awake()
    {
        for (var index = 0; index < m_GameLevels.Count; index++)
        {
            var VARIABLE = m_GameLevels[index];
            var ins = Instantiate(m_LevelSelectButton, m_LayoutTransform);
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
        m_InsButtons[1].OnEnter();
        m_InsButtons[1].OnExit();
    }

    public void Selected(cGameLevelSO gameLevelSo)
    {
        cGameLogicManager.Instance.SetLevel(gameLevelSo.m_ConfigFile);
        m_OnSelected.Invoke();
    }

    public void OnEnter(cGameLevelSO gameLevelSo)
    {
        m_BG.color = gameLevelSo.m_Color;
        m_Icon.sprite = gameLevelSo.m_Icon;
        // m_Icon.color = Color.Lerp(gameLevelSo.m_Color, Color.white, 0);
    }
}
