using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI.ProceduralImage;
using Zenject;

public class cFillbarController : MonoBehaviour
{
    [SerializeField] private float m_Duration;
    [SerializeField] private ProceduralImage m_FillImage;
    [SerializeField] private Gradient m_FillColorGradient;
    [Inject] private cGameManagerStateMachine m_GameManager;

    private void Awake()
    {
        m_GameManager.GameEvents.OnPlayerInputStartEvent += Fill;
        m_GameManager.GameEvents.OnSuccessTurn += Refresh;
        m_GameManager.GameEvents.OnWrongButtonEvent += () =>
        {
            m_FillImage.DOKill();
        };
    }

    public void Init(float duration)
    {
        m_Duration = duration;
        Refresh();
    }

    public void Fill()
    {
        m_FillImage.DOFillAmount(1, m_Duration).SetEase(Ease.Linear).OnUpdate((() =>
        {
            m_FillImage.color = m_FillColorGradient.Evaluate(m_FillImage.fillAmount);
        })).OnComplete((() =>
        {
            m_FillImage.DOKill();
            m_GameManager.GameEvents.OnTimeIsUpEvent.Invoke();;
        }));
    }

    public void Refresh()
    {
        m_FillImage.DOKill();
        m_FillImage.fillAmount = 0;
    }
}
