using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cUIManager : cSingleton<cUIManager>
{
    [SerializeField] private Page m_StartView;
    [SerializeField] private List<cView> m_Views;

    private void Awake()
    {
        ShowPage(m_StartView);
    }

    public void ShowPage(Page page)
    {
        m_Views[(int)page].Activate();
    }
    
    public void HidePage(Page page)
    {
        m_Views[(int)page].Deactivate();
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