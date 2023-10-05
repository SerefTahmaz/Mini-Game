using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cTransitionManager : cPage
{
    [SerializeField] private cDownTransition m_Rotation;
    [SerializeField] private cDownTransition2 m_Lateral;
    
    public enum TransitionType
    {
        Rotating,
        Lateral
    }

    public void PlayTransition(TransitionType transitionType)
    {
        switch (transitionType)
        {
            case TransitionType.Rotating:
                m_Rotation.Anim();
                break;
            case TransitionType.Lateral:
                m_Lateral.Anim();
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(transitionType), transitionType, null);
        }
    }
}
