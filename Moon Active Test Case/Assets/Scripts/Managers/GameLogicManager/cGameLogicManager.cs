using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

public class cOldGameLogicManager : MonoBehaviour
{
    [SerializeField] private cLeaderBoardView m_LeaderBoardView;
    [SerializeField] private cTransitionManager m_TransitionManager;
    [Inject] private cObjectPooler m_ObjectPooler;
    [Inject] private cUIManager m_UIManager;
    [Inject] private ISoundManager m_SoundManager;
    [Inject] private ILevelManager m_LevelManager;

    private IGameConfig GameConfig;
    private cGameConfiguration m_CurrentGameConfig;
    private cGameManagerEventController m_GameEvents = new cGameManagerEventController();

    public cLeaderBoardView LeaderBoardView => m_LeaderBoardView;
    public cTransitionManager TransitionManager => m_TransitionManager;
    public cGameManagerEventController GameEvents => m_GameEvents;

    private void Awake()
    {
        GameConfig = new cJsonGameConfig();
    }

    public void SetLevel(TextAsset textAsset)
    {
        GameEvents.OnTimeIsUpEvent = delegate {  };
        m_CurrentGameConfig = GameConfig.Load(textAsset);
        m_LevelManager.LoadCurrentLevel(m_CurrentGameConfig);
        
        m_UIManager.ShowPage(Page.Gameplay);
        m_UIManager.Fillbar.Init(m_CurrentGameConfig.m_GameTimeInSeconds);
    }

    public void OnSuccessTurn()
    {
        var go = m_ObjectPooler.Spawn("MoneyUI", cCurrencyBarScreen.Instance.transform).transform;
        go.localScale = Vector3.one * 1.25f;
        go.gameObject.GetComponent<cMoneyUI>().Fly(m_CurrentGameConfig.m_EachStepPointCount);
        m_SoundManager.SuccessSound();
        GameEvents.OnSuccessTurn.Invoke();
    }

    public void OnStartButton()
    {
        m_UIManager.SetInteractable(false);
        m_TransitionManager.PlayTransition(cTransitionManager.TransitionType.Rotating, () =>
        {
            m_UIManager.HidePage(Page.Start);
            m_UIManager.HidePage(Page.MainMenuSliderView);
            m_UIManager.ShowPage(Page.LevelSelect);
        }, () =>
        {
            m_UIManager.SetInteractable(true);
        });
    }

    public void OnFail()
    {
        m_UIManager.HidePage(Page.Gameplay);
        m_UIManager.ShowPage(Page.FailView);
    }

    public void Replay()
    {
        m_UIManager.HidePage(Page.LeaderBoardView);
        m_UIManager.HidePage(Page.FailView);
        m_UIManager.ShowPage(Page.LevelSelect);
        m_LevelManager.RemoveCurrentLevel();
    }
}