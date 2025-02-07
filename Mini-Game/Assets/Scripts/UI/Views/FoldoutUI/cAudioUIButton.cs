using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using SimonSays.Managers;
using SimonSays.Managers.SaveManager;
using UnityEngine;
using Zenject;

namespace SimonSays.UI
{
    public class cAudioUIButton : cBaseFoldoutItem
    {
        [SerializeField] private CanvasGroup m_CanvasGroup;
        [SerializeField] private GameObject m_DisableGO;
        private ISoundManager m_SoundManager;
        private ISaveManager m_SaveManager;
    
        [Inject]
        public void Initialize(ISoundManager soundManager, ISaveManager saveManager) 
        {
            m_SoundManager = soundManager;
            m_SaveManager = saveManager;
        }

        public void OnClick()
        {
            var audiostate = m_SaveManager.SaveData.m_AudioState;

            if (audiostate)
            {
                DisableAudio();
            }
            else
            {
                EnableAudio();
            }
            m_SoundManager.PlayClick();
        }

        public void EnableAudio()
        {
            m_SaveManager.SaveData.m_AudioState = true;
            m_DisableGO.SetActive(false);
            m_SoundManager.SetActive( true);
        }
    
        public void DisableAudio()
        {
            m_SaveManager.SaveData.m_AudioState = false;
            m_DisableGO.SetActive(true);
            m_SoundManager.SetActive( false);
        }
        
        public override void Refresh()
        {
            var audioState = m_SaveManager.SaveData.m_AudioState;;

            if (audioState)
            {
                EnableAudio();
            }
            else
            {
                DisableAudio();
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