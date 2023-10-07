using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using Zenject;

public class cStartButtonController : cButton
{
    [SerializeField] private Transform m_Pivot;
    [SerializeField, Min(0.001f)] private float m_PulseDelay;
    [Inject] private cGameManagerStateMachine m_GameManager;
    [Inject] private cUIManager m_UIManager;

    private void Awake()
    {
        ButtonPulseAnim().Forget();
    }

    private async UniTaskVoid ButtonPulseAnim()
    {
        while (true)
        {
            m_Pivot.DOScale(0.15f, .2f).SetLoops(2, LoopType.Yoyo).SetRelative(true);
            await UniTask.Delay(TimeSpan.FromSeconds(m_PulseDelay));
        }
    }

    public override void OnClick()
    {
        base.OnClick();
        
        m_UIManager.SetInteractable(false);
        m_UIManager.TransitionManager.PlayTransition(cTransitionManager.TransitionType.Rotating, () =>
        {
            
            m_GameManager.ChangeState(m_GameManager.GameplayState);
        }, () =>
        {
            m_UIManager.SetInteractable(true);
        });
    }
}
