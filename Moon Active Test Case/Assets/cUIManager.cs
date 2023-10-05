using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cUIManager : cSingleton<cUIManager>
{
    [SerializeField] private Page m_StartPage;
    [SerializeField] private List<cPage> m_Pages;

    private void Awake()
    {
        ShowPage(m_StartPage);
    }

    public void ShowPage(Page page)
    {
        m_Pages[(int)page].Activate();
    }
    
    public void HidePage(Page page)
    {
        m_Pages[(int)page].Deactivate();
    }
}

public enum Page
{
    Gameplay,
    Start,
    LevelSelect,
    FailView,
    LeaderBoardView,
    StoreView,
    MainMenuSliderView
}