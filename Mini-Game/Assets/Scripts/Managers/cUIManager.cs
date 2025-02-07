using System;
using System.Collections;
using System.Collections.Generic;
using SimonSays.Gameplay.UI;
using SimonSays.UI;
using SimonSays.UI.Currency;
using SimonSays.UI.Leaderboard;
using UnityEngine;

namespace SimonSays.Managers
{
    public class cUIManager : MonoBehaviour
    {
        [SerializeField] private Page m_StartView;
        [SerializeField] private List<cView> m_Views;
        [SerializeField] private cFillbarController m_FillbarController;
        [SerializeField] private CanvasGroup m_CanvasGroup;
        [SerializeField] private cLeaderBoardView m_LeaderBoardView;
        [SerializeField] private cTransitionManager m_TransitionManager;
        [SerializeField] private cCurrencyBarScreen m_CurrencyManager;

        public cFillbarController Fillbar => m_FillbarController;
        public cLeaderBoardView LeaderBoardView => m_LeaderBoardView;
        public cTransitionManager TransitionManager => m_TransitionManager;

        public cCurrencyBarScreen CurrencyManager => m_CurrencyManager;

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

        public void SetInteractable(bool state)
        {
            m_CanvasGroup.interactable = state;
            m_CanvasGroup.blocksRaycasts = state;
        }
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