using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Zenject;

public class cHomeButton : cButton
{
    [Inject] private cGameManagerStateMachine m_GameManager;
    [Inject] private cUIManager m_UIManager;
    
    public override void OnClick()
    {
        base.OnClick();
        m_UIManager.TransitionManager.PlayTransition(cTransitionManager.TransitionType.Lateral, () =>
        {
            m_GameManager.ChangeState(m_GameManager.MenuState);
        }, () =>
        {
            
        });
    }
}
