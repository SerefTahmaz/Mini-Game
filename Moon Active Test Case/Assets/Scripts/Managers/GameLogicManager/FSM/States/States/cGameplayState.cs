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
        cGameManagerStateMachine GameManagerStateMachine => m_StateMachine as cGameManagerStateMachine;
        
        public override void Enter()
        {
            base.Enter();
            m_UIManager.ShowPage(Page.LevelSelect);
        }

        public override void Exit()
        {
            base.Exit();
            m_UIManager.HidePage(Page.LevelSelect);
            m_UIManager.HidePage(Page.Gameplay);
        }
    }
}