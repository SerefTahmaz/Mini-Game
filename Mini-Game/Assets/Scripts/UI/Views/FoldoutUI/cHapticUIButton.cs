using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using SimonSays.Managers.SaveManager;
using UnityEngine;
using Zenject;

namespace SimonSays.UI
{
    public class cHapticUIButton : cBaseFoldoutItem
    {
        [SerializeField] private CanvasGroup m_CanvasGroup;
        [SerializeField] private GameObject m_DisableGO;
        [Inject] private ISaveManager m_SaveManager;

        public void OnClick()
        {
            var hapticState = m_SaveManager.SaveData.m_HapticState;

            if (hapticState)
            {
                DisableHaptic();
            }
            else
            {
                EnableHaptic();
            }
        }

        public void EnableHaptic()
        {
            m_SaveManager.SaveData.m_HapticState = true;
            m_DisableGO.SetActive(false);
        }
    
        public void DisableHaptic()
        {
            m_SaveManager.SaveData.m_HapticState = false;
            m_DisableGO.SetActive(true);
        }

        public override void Refresh()
        {
            var hapticState = m_SaveManager.SaveData.m_HapticState;;

            if (hapticState)
            {
                EnableHaptic();
            }
            else
            {
                DisableHaptic();
            }
        }

        public override void Activate(float duration)
        {
            m_CanvasGroup.DOKill();
            m_CanvasGroup.DOFade(1, 1* duration);
        }

        public override void Deactivate(float duration)
        {
            m_CanvasGroup.DOKill();
            m_CanvasGroup.DOFade(0, 1* duration);
        }
    }
}

