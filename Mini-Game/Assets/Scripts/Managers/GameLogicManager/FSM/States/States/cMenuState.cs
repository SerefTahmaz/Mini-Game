using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using SimonSays.Managers;
using UnityEngine;
using UnityEngine.AI;
using Zenject;
using Random = UnityEngine.Random;

namespace FiniteStateMachine
{
    public class cMenuState : cStateBase
    {
        [SerializeField] private List<GameObject> m_MenuBackground;
        [Inject] private cUIManager m_UIManager;
        [Inject] private ISoundManager m_SoundManager;

        public override void Enter()
        {
            base.Enter();
            m_UIManager.ShowPage(Page.Start);
            m_UIManager.ShowPage(Page.MainMenuSliderView);
            m_MenuBackground.ForEach((o => o.SetActive(true)));
            m_SoundManager.PlayAmbient();
        }

        public override void Exit()
        {
            base.Exit();
            m_UIManager.HidePage(Page.Start);
            m_UIManager.HidePage(Page.MainMenuSliderView);
            m_MenuBackground.ForEach((o => o.SetActive(false)));
            m_SoundManager.PauseAmbient();
        }
    }
}