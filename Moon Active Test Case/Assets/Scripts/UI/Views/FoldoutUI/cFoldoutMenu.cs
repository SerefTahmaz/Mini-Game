using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using SimonSays.Managers;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace SimonSays.UI
{
    public class cFoldoutMenu : MonoBehaviour
    {
        [SerializeField] private List<cBaseFoldoutItem> m_FoldoutItems;
        [SerializeField] private HorizontalLayoutGroup m_HorizontalLayoutGroup;
        [SerializeField] private float m_TargetValue;

        [SerializeField] private bool m_Closed;
        [SerializeField] private Transform m_CogWhell;
        [Inject] private ISoundManager m_SoundManager;

        public void Refresh()
        {
            SetToggle(true, 0);
            foreach (var foldoutItem in m_FoldoutItems)
            {
                foldoutItem.Refresh();
            }

            m_Closed = true;
        }

        public void OnClick()
        {
            OnToggle(1);   
        }

        public void OnToggle(float duration)
        {
            m_Closed = !m_Closed;
            SetToggle(m_Closed, duration);
        }

        public void SetToggle(bool close, float duration)
        {
            if (close)
            {
                DOVirtual.Float(m_HorizontalLayoutGroup.spacing, m_TargetValue, 1* duration, value =>
                {
                    m_HorizontalLayoutGroup.spacing = value;
                });
            
                foreach (var foldoutItem in m_FoldoutItems)
                {
                    foldoutItem.Deactivate(1 * duration);
                }
            }
            else
            {
                DOVirtual.Float(m_HorizontalLayoutGroup.spacing, 20, 1* duration, value =>
                {
                    m_HorizontalLayoutGroup.spacing = value;
                });

                foreach (var foldoutItem in m_FoldoutItems)
                {
                    foldoutItem.Activate(1 * duration);
                }
            }

            m_CogWhell.DOKill();
            m_CogWhell.DOLocalRotate(new Vector3(0, 0, -60), .5f * duration).SetRelative(true);
        
            if(duration > 0) m_SoundManager.PlayClick();
        }
    }
}