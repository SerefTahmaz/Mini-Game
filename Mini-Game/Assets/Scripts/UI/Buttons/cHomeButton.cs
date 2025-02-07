using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using SimonSays.Managers;
using SimonSays.Managers.GameManager;
using UnityEngine;
using Zenject;

namespace SimonSays.UI
{
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
}