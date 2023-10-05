using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class cLevelSelectButton : MonoBehaviour
{
    [SerializeField] private TMP_Text m_LevelNameText;

    private cGameLevelSO m_GameLevelSo;
    private cLevelSelectUIController LevelSelectUIController;

    public void Init(cGameLevelSO gameLevelSo, cLevelSelectUIController levelSelectUIController)
    {
        LevelSelectUIController = levelSelectUIController;
        m_GameLevelSo = gameLevelSo;
        m_LevelNameText.text = m_GameLevelSo.m_LevelName;

    }
    public void OnClick()
    {
        LevelSelectUIController.Selected(m_GameLevelSo);
    }
}