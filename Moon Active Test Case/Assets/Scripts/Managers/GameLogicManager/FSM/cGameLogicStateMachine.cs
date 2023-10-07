using System;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using FiniteStateMachine;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Animations;
using Zenject;
using Random = UnityEngine.Random;

public class cGameLogicStateMachine : cStateMachine
    {
        #region PritaveFields
        [SerializeField] private cStatesBlackBoard m_StatesBlackBoard;
        [SerializeField] private cLeaderBoardView m_LeaderBoardView;
        [SerializeField] private cTransitionManager m_TransitionManager;
        [Inject] private cObjectPooler m_ObjectPooler;
        [Inject] private cUIManager m_UIManager;
        [Inject] private ISoundManager m_SoundManager;
        [Inject] private ILevelManager m_LevelManager;
        
        private cGameManagerEventController m_GameEvents = new cGameManagerEventController();
        private cGameConfiguration m_CurrentGameConfig;
        private IGameConfig m_GameConfig;
        #endregion

        #region States
        public cStateBase Empty => m_StatesBlackBoard.m_Empty;
        public cStateBase GameplayState => m_StatesBlackBoard.m_GameplayState;
        public cStateBase FailState => m_StatesBlackBoard.m_FailState;
        public cStateBase MenuState => m_StatesBlackBoard.m_MenuState;
        #endregion

        #region Properties
        public cLeaderBoardView LeaderBoardView => m_LeaderBoardView;
        public cTransitionManager TransitionManager => m_TransitionManager;
        public cGameManagerEventController GameEvents => m_GameEvents;
        #endregion

        private void Awake()
        {
            m_GameConfig = new cJsonGameConfig();
            
            MenuState.InitializeState("Menu", this);
            GameplayState.InitializeState("Gameplay", this);
            FailState.InitializeState("Fail", this);
            Empty.InitializeState("Empty", this);
        }

        protected override cStateBase GetInitialState()
        {
            return MenuState;
        }
        
        public void OnSuccessTurn()
        {
            var go = m_ObjectPooler.Spawn("MoneyUI", cCurrencyBarScreen.Instance.transform).transform;
            go.localScale = Vector3.one * 1.25f;
            go.gameObject.GetComponent<cMoneyUI>().Fly(m_CurrentGameConfig.m_EachStepPointCount);
            m_SoundManager.SuccessSound();
            GameEvents.OnSuccessTurn.Invoke();
        }
        
        public void Replay()
        {
            m_UIManager.HidePage(Page.LeaderBoardView);
            m_UIManager.HidePage(Page.FailView);
            m_UIManager.ShowPage(Page.LevelSelect);
            m_LevelManager.RemoveCurrentLevel();
        }
        
        public void SetLevel(TextAsset textAsset)
        {
            GameEvents.OnTimeIsUpEvent = delegate {  };
            m_CurrentGameConfig = m_GameConfig.Load(textAsset);
            m_LevelManager.LoadCurrentLevel(m_CurrentGameConfig);
        
            m_UIManager.ShowPage(Page.Gameplay);
            m_UIManager.Fillbar.Init(m_CurrentGameConfig.m_GameTimeInSeconds);
        }
    }