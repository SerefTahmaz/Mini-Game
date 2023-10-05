using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class cStartButtonController : cButton
{
    [SerializeField] private Transform m_Pivot;
    [SerializeField] private float m_PulseDelay;

    private void Awake()
    {
        ButtonPulseAnim();
    }

    private void ButtonPulseAnim()
    {
        DOVirtual.DelayedCall(m_PulseDelay, () =>
        {
            m_Pivot.DOScale(0.25f, .3f).SetLoops(2, LoopType.Yoyo).SetRelative(true);
            ButtonPulseAnim();
        });
    }

    public override void OnClick()
    {
        base.OnClick();
        cGameLogicManager.Instance.OnStartButton();
    }
}
