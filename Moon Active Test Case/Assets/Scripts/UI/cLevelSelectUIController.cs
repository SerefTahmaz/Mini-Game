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

    private void Awake()
    {
        foreach (var VARIABLE in m_GameLevels)
        {
            var ins = Instantiate(m_LevelSelectButton,m_LayoutTransform);
            ins.Init(VARIABLE, this);
        }
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
