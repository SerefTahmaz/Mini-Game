using System;
using System.Collections;
using System.Linq;
using DG.Tweening;
using UnityEngine;
using UnityEngine.AI;
using Zenject;
using Random = UnityEngine.Random;

namespace FiniteStateMachine
{
    public class cMenuState : cStateBase
    {
        [SerializeField] private GameObject m_MenuBackground;
        [Inject] private cUIManager m_UIManager;

        public override void Enter()
        {
            base.Enter();
            m_UIManager.ShowPage(Page.Start);
            m_UIManager.ShowPage(Page.MainMenuSliderView);
            m_MenuBackground.SetActive(true);
        }

        public override void Exit()
        {
            base.Exit();
            m_UIManager.HidePage(Page.Start);
            m_UIManager.HidePage(Page.MainMenuSliderView);
            m_MenuBackground.SetActive(false);
        }
    }
}