using System.Linq;
using DG.Tweening;
using SimonSays.Managers;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace FiniteStateMachine
{
    public class cFailState : cStateBase
    {
        [Inject] private cUIManager m_UIManager;
        [Inject] private ILevelManager m_LevelManager;

        public override void Enter()
        {
            base.Enter();
            m_UIManager.ShowPage(Page.FailView);
            m_UIManager.ShowPage(Page.LeaderBoardView);
        }

        public override void Exit()
        {
            base.Exit();
            m_UIManager.HidePage(Page.LeaderBoardView);
            m_UIManager.HidePage(Page.FailView);
            m_LevelManager.RemoveCurrentLevel();
        }
    }
}