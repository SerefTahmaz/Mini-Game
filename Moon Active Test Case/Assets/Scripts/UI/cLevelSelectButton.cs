using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class cLevelSelectButton : cButton
{
    [SerializeField] private TMP_Text m_LevelNameText;
    [SerializeField] private Image m_BG;

    private cGameLevelSO m_GameLevelSo;
    private cLevelSelectUIController LevelSelectUIController;

    public void Init(cGameLevelSO gameLevelSo, cLevelSelectUIController levelSelectUIController)
    {
        LevelSelectUIController = levelSelectUIController;
        m_GameLevelSo = gameLevelSo;
        m_LevelNameText.text = m_GameLevelSo.m_LevelName;
        m_BG.color = gameLevelSo.m_Color;

    }
    public override void OnClick()
    {
        base.OnClick();
        LevelSelectUIController.Selected(m_GameLevelSo);
    }

    public override void OnEnter()
    {
        base.OnEnter();
        LevelSelectUIController.OnEnter(m_GameLevelSo);
    }
}