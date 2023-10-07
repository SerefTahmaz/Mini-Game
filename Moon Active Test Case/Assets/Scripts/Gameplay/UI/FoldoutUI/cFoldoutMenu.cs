using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class cFoldoutMenu : MonoBehaviour
{
    [SerializeField] private List<CanvasGroup> m_CanvasGroups;
    [SerializeField] private HorizontalLayoutGroup m_HorizontalLayoutGroup;
    [SerializeField] private float m_TargetValue;

    [SerializeField] private bool m_Closed;
    [SerializeField] private Transform m_CogWhell;
    [Inject] private ISoundManager m_SoundManager;

    private void Awake()
    {
        OnToggle(0);
    }

    public void OnClick()
    {
        OnToggle(1);   
    }

    public void OnToggle(float duration)
    {
        if (m_Closed)
        {
            DOVirtual.Float(m_HorizontalLayoutGroup.spacing, 20, 1* duration, value =>
            {
                m_HorizontalLayoutGroup.spacing = value;
            });

            foreach (var VARIABLE in m_CanvasGroups)
            {
                VARIABLE.DOKill();
                VARIABLE.DOFade(1, 1* duration);
            }
        }
        else
        {
            DOVirtual.Float(m_HorizontalLayoutGroup.spacing, m_TargetValue, 1* duration, value =>
            {
                m_HorizontalLayoutGroup.spacing = value;
            });
            
            foreach (var VARIABLE in m_CanvasGroups)
            {
                VARIABLE.DOKill();
                VARIABLE.DOFade(0, 1* duration);
            }
        }

        m_Closed = !m_Closed;

        m_CogWhell.DOKill();
        m_CogWhell.DOLocalRotate(new Vector3(0, 0, -60), .5f * duration).SetRelative(true);
        
        if(duration > 0) m_SoundManager.PlayClick();
    }
}