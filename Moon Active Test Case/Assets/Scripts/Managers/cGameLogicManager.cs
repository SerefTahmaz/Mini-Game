using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class cGameLogicManager : cSingleton<cGameLogicManager>
{
    [SerializeField] private cLeaderBoardView m_LeaderBoardView;
    [SerializeField] private cTransitionManager m_TransitionManager;

    private IGameConfig GameConfig;
    private cGameConfiguration m_CurrentGameConfig;

    public Action m_OnSelected = delegate { };

    public cLeaderBoardView LeaderBoardView => m_LeaderBoardView;

    public cTransitionManager TransitionManager => m_TransitionManager;

    private void Awake()
    {
        GameConfig = new cJsonGameConfig();
    }

    public void SetLevel(TextAsset textAsset)
    {
        m_CurrentGameConfig = GameConfig.Load(textAsset);
        m_OnSelected = delegate { };
        cLevelManager.Instance.LoadCurrentLevel();
        cLevelManager.Instance.m_CurrentLevel.InitLevel(m_CurrentGameConfig);
    }

    public void OnSuccessTurn()
    {
        var go = cObjectPooler.Instance.Spawn("MoneyUI", cCurrencyBarScreen.Instance.transform).transform;
        go.localScale = Vector3.one * 1.25f;
        go.gameObject.GetComponent<cMoneyUI>().Fly();
    }

    public void OnStartButton()
    {
        m_TransitionManager.PlayTransition(cTransitionManager.TransitionType.Lateral);
        DOVirtual.DelayedCall(.5f, () =>
        {
            cUIManager.Instance.HidePage(Page.Start);
            cUIManager.Instance.HidePage(Page.MainMenuSliderView);
            cUIManager.Instance.ShowPage(Page.LevelSelect);
        });
    }

    public void OnFail()
    {
        cUIManager.Instance.HidePage(Page.Gameplay);
        cUIManager.Instance.ShowPage(Page.FailView);
    }

    public void Replay()
    {
        cUIManager.Instance.HidePage(Page.LeaderBoardView);
        cUIManager.Instance.HidePage(Page.FailView);
        cUIManager.Instance.ShowPage(Page.LevelSelect);
    }
}


#if UNITY_EDITOR
[CustomEditor(typeof(cGameLogicManager))]
public class cGameLogicManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if (GUILayout.Button("Click"))
        {
            // (target as cGameLogicManager).SetLevel();
        }
    }
}
#endif