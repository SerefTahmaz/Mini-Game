using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Zenject;

public class cHomeButton : cButton
{
    [Inject] private cGameLogicManager m_GameLogicManager;
    [Inject] private cUIManager m_UIManager;
    [Inject] private cLevelManager m_LevelManager;
    
    public override void OnClick()
    {
        base.OnClick();
        m_GameLogicManager.TransitionManager.PlayTransition(cTransitionManager.TransitionType.Lateral, () =>
        {
            m_UIManager.HidePage(Page.LeaderBoardView);
            m_UIManager.HidePage(Page.FailView);
            m_UIManager.ShowPage(Page.Start);
            m_UIManager.ShowPage(Page.MainMenuSliderView);
            m_LevelManager.RemoveLevel();
        }, () =>
        {
            
        });
    }
}
