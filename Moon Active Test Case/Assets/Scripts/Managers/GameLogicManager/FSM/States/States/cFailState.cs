using System.Linq;
using DG.Tweening;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace FiniteStateMachine
{
    public class cFailState : cStateBase
    {
        [Inject] private cUIManager m_UIManager;
        
        public override void Enter()
        {
            base.Enter();
            m_UIManager.HidePage(Page.Gameplay);
            m_UIManager.ShowPage(Page.FailView);
        }

        public override void Exit()
        {
            base.Exit();
        }
    }
}