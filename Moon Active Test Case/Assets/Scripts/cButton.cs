using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class cButton : MonoBehaviour
{
    private bool m_IsClicked = false;

    public virtual void OnEnter()
    {
        if(m_IsClicked) return;
        
        ResetState();
        transform.DOScale(.1f, .25f).SetRelative(true);
        cSoundManager.Instance.PlayMouseEnter();
    }
    
    public virtual void OnClick()
    {
        ResetState();
        transform.DOScale(.15f, .25f).SetLoops(2,LoopType.Yoyo).SetRelative(true);
        cSoundManager.Instance.PlayClick();

        m_IsClicked = true;
        DOVirtual.DelayedCall(.25f, () =>
        {
            m_IsClicked = false;
        });
    }
    
    public virtual void OnExit()
    {
        if(m_IsClicked) return;
        transform.DOKill();
        transform.DOScale(1, .25f);
    }

    public virtual void Success()
    {
        ResetState();
        transform.SuccessShakeUI();
        
        m_IsClicked = true;
        DOVirtual.DelayedCall(.4f, () =>
        {
            m_IsClicked = false;
        });
    }

    public virtual void Fail()
    {
        ResetState();
        transform.FailShakeUI();
        
        m_IsClicked = true;
        DOVirtual.DelayedCall(.4f, () =>
        {
            m_IsClicked = false;
        });
    }

    public void ResetState()
    {
        transform.DOComplete();
        transform.localScale = Vector3.one;
    }
}
