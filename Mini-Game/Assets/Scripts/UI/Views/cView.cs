using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace SimonSays.UI
{
    public class cView : MonoBehaviour
    {
        [SerializeField] private CanvasGroup m_CanvasGroup;

        protected bool m_IsActive;

        public virtual void Activate()
        {
            m_CanvasGroup.DOComplete();
            m_CanvasGroup.blocksRaycasts = true;
            m_CanvasGroup.interactable = true;
            m_CanvasGroup.DOFade(1, .2f);
            m_IsActive = true;
        }
    
        public virtual void Deactivate()
        {
            m_CanvasGroup.DOComplete();
            m_CanvasGroup.blocksRaycasts = false;
            m_CanvasGroup.interactable = false;
            m_CanvasGroup.DOFade(0,.2f);
            m_IsActive = false;
        }
    }
}