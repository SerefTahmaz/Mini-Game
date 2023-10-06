using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class cFailView : cView
{
    [SerializeField] private Transform m_BottonLayout;
    
    public override void Activate()
    {
        base.Activate();
        cUIManager.Instance.ShowPage(Page.LeaderBoardView);
        
        m_BottonLayout.DOKill();
        m_BottonLayout.localScale = Vector3.zero;
        m_BottonLayout.DOScale(1, .5f).SetEase(Ease.OutBack).SetDelay(.3f);
    }
}
