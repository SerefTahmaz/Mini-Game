using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class cButton : MonoBehaviour
{

    public virtual void OnEnter()
    {
        
    }
    
    public virtual void OnClick()
    {
        transform.DOComplete();
        transform.DOScale(.25f, .25f).SetRelative(true);
    }
    
    public virtual void OnExit()
    {
        
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
    }
}
