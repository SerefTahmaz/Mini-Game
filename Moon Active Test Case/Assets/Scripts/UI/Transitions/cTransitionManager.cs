using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cTransitionManager : cView
{
    [SerializeField] private cLateralTransition m_Lateral;
    [SerializeField] private cRotationalTransition m_Rotation;
    
    public enum TransitionType
    {
        Rotating,
        Lateral
    }

    public void PlayTransition(TransitionType transitionType, Action onFullCoverScreen, Action onFinish)
    {
        switch (transitionType)
        {
            case TransitionType.Rotating:
                m_Rotation.Anim(onFullCoverScreen, onFinish);
                break;
            case TransitionType.Lateral:
                m_Lateral.Anim(onFullCoverScreen, onFinish);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(transitionType), transitionType, null);
        }
    }
}
