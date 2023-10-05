using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class cFoldoutMenu : MonoBehaviour
{
    [SerializeField] private List<CanvasGroup> m_CanvasGroups;
    [SerializeField] private HorizontalLayoutGroup m_HorizontalLayoutGroup;
    [SerializeField] private float m_TargetValue;

    [SerializeField] private bool m_Closed;
    [SerializeField] private Transform m_CogWhell;

    private void Awake()
    {
        OnClick();
    }

    public void OnClick()
    {
        if (m_Closed)
        {
            DOVirtual.Float(m_HorizontalLayoutGroup.spacing, 20, 1, value =>
            {
                m_HorizontalLayoutGroup.spacing = value;
            });

            foreach (var VARIABLE in m_CanvasGroups)
            {
                VARIABLE.DOKill();
                VARIABLE.DOFade(1, 1);
            }
        }
        else
        {
            DOVirtual.Float(m_HorizontalLayoutGroup.spacing, m_TargetValue, 1, value =>
            {
                m_HorizontalLayoutGroup.spacing = value;
            });
            
            foreach (var VARIABLE in m_CanvasGroups)
            {
                VARIABLE.DOKill();
                VARIABLE.DOFade(0, 1);
            }
        }

        m_Closed = !m_Closed;

        m_CogWhell.DOKill();
        m_CogWhell.DOLocalRotate(new Vector3(0, 0, -60), .5f).SetRelative(true);
        
        cSoundManager.Instance.PlayClick();
    }
}