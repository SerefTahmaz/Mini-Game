using FiniteStateMachine;
using SimonSays.Gameplay.UI;
using SimonSays.Managers;
using SimonSays.Managers.Config;
using UnityEngine;
using Zenject;

namespace SimonSays.Managers.GameManager
{
    /// <summary>
    /// Manager state machine for dealing with general game states
    /// </summary>
    public class cGameManagerStateMachine : cStateMachine
    {
        #region PritaveFields
        [SerializeField] private cStatesBlackboard m_StatesBlackboard;
        [Inject] private IObjectPooler m_ObjectPooler;
        [Inject] private cUIManager m_UIManager;
        [Inject] private ISoundManager m_SoundManager;
        [Inject] private ILevelManager m_LevelManager;
        
        private cGameManagerEventController m_GameEvents = new cGameManagerEventController();
        private cGameConfiguration m_CurrentGameConfig;
        private IGameConfigHandler m_GameConfigHandler;
        #endregion

        #region States
        public cStateBase Empty => m_StatesBlackboard.m_Empty;
        public cStateBase GameplayState => m_StatesBlackboard.m_GameplayState;
        public cStateBase FailState => m_StatesBlackboard.m_FailState;
        public cStateBase MenuState => m_StatesBlackboard.m_MenuState;
        #endregion

        #region Properties
        public cGameManagerEventController GameEvents => m_GameEvents;
        #endregion

        private void Awake()
        {
            m_GameConfigHandler = new cJsonGameConfigHandler();

            Empty.InitializeState("Empty", this);
            MenuState.InitializeState("Menu", this);
            GameplayState.InitializeState("Gameplay", this);
            FailState.InitializeState("Fail", this);
        }

        protected override cStateBase GetInitialState()
        {
            return MenuState;
        }
        
        public void OnSuccessTurn()
        {
            var go = m_ObjectPooler.Spawn("MoneyUI", m_UIManager.CurrencyManager.transform).transform;
            go.localScale = Vector3.one * 1.25f;
            go.gameObject.GetComponent<cMoneyUI>().Fly(m_CurrentGameConfig.m_EachStepPointCount);
            m_SoundManager.SuccessSound();
            GameEvents.OnSuccessTurn.Invoke();
        }
        
        public void Replay()
        {
            ChangeState(GameplayState);
        }
        
        /// <summary>
        /// <para>Initialize level spawning according to given config</para>
        /// </summary>
        /// <param name="textAsset">game configuration text</param>
        public void SetLevel(TextAsset textAsset)
        {
            GameEvents.OnTimeIsUpEvent = delegate {  };
            m_CurrentGameConfig = m_GameConfigHandler.FileToConfig(textAsset);
            m_LevelManager.LoadCurrentLevel(m_CurrentGameConfig);
        
            m_UIManager.ShowPage(Page.Gameplay);
            m_UIManager.Fillbar.Init(m_CurrentGameConfig.m_GameTimeInSeconds);
        }
    }
}