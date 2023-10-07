using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;
using UnityEngine.AI;
using Zenject;
using Random = UnityEngine.Random;

namespace FiniteStateMachine
{
    public class cGameplayState : cStateBase
    {
        [Inject] private cUIManager m_UIManager;
        cGameLogicStateMachine GameLogicStateMachine => m_StateMachine as cGameLogicStateMachine;
        
        public override void Enter()
        {
            base.Enter();
            m_UIManager.SetInteractable(false);
            GameLogicStateMachine.TransitionManager.PlayTransition(cTransitionManager.TransitionType.Rotating, () =>
            {
                m_UIManager.HidePage(Page.Start);
                m_UIManager.HidePage(Page.MainMenuSliderView);
                m_UIManager.ShowPage(Page.LevelSelect);
            }, () =>
            {
                m_UIManager.SetInteractable(true);
            });
        }

        public override void Exit()
        {
            base.Exit();
        }
    }
}