using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI.ProceduralImage;

public class cFillbarController : MonoBehaviour
{
    [SerializeField] private float m_Duration;
    [SerializeField] private ProceduralImage m_FillImage;
    [SerializeField] private Gradient m_FillColorGradient;

    private void Awake()
    {
        cGameLogicManager.Instance.GameEvents.OnPlayerInputStartEvent += Fill;
        cGameLogicManager.Instance.GameEvents.OnSuccessTurn += Refresh;
        cGameLogicManager.Instance.GameEvents.OnWrongButtonEvent += () =>
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
            cGameLogicManager.Instance.GameEvents.OnTimeIsUpEvent.Invoke();;
        }));
    }

    public void Refresh()
    {
        m_FillImage.DOKill();
        m_FillImage.fillAmount = 0;
    }
}
