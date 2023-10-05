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
        transform.DOComplete();
        transform.DOScale(.1f, .25f).SetRelative(true);
        cSoundManager.Instance.PlayMouseEnter();
    }
    
    public virtual void OnClick()
    {
        transform.DOComplete();
        transform.DOScale(.25f, .25f).SetRelative(true);
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
        transform.DOComplete();
        transform.DOScale(1, .25f);
    }

    public virtual void Success()
    {
        transform.DOComplete();

        transform.DOShakeScale(.3f, .2f);

        foreach (var VARIABLE in GetComponentsInChildren<Image>())
        {
            VARIABLE.DOComplete();
            Color colorToLerp = Color.green;
            colorToLerp.a = VARIABLE.color.a;
            VARIABLE.DOColor(colorToLerp, .15f).SetLoops(2, LoopType.Yoyo);
        }
        
        m_IsClicked = true;
        DOVirtual.DelayedCall(.4f, () =>
        {
            m_IsClicked = false;
        });
    }

    public virtual void Fail()
    {
        transform.DOComplete();

        transform.DOShakeRotation(.3f, 10, 25);

        foreach (var VARIABLE in GetComponentsInChildren<Image>())
        {
            VARIABLE.DOComplete();
            Color colorToLerp = Color.red;
            colorToLerp.a = VARIABLE.color.a;
            VARIABLE.DOColor(colorToLerp, .15f).SetLoops(2, LoopType.Yoyo);
        }
        
        m_IsClicked = true;
        DOVirtual.DelayedCall(.4f, () =>
        {
            m_IsClicked = false;
        });
    }
}
