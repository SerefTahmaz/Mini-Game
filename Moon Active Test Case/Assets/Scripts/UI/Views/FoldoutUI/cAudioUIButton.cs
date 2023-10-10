using System;
using System.Collections;
using System.Collections.Generic;
using SimonSays.Managers;
using SimonSays.Managers.SaveManager;
using UnityEngine;
using Zenject;

namespace SimonSays.UI
{
    public class cAudioUIButton : MonoBehaviour
    {
        [SerializeField] private GameObject m_DisableGO;
        private ISoundManager m_SoundManager;
        private ISaveManager m_SaveManager;
    
        [Inject]
        public void Initialize(ISoundManager soundManager, ISaveManager saveManager) 
        {
            m_SoundManager = soundManager;
            m_SaveManager = saveManager;
        }

        private void Awake()
        {
            var audiostate = m_SaveManager.SaveData.m_AudioState;;

            if (audiostate)
            {
                EnableAudio();
            }
            else
            {
                DisableAudio();
            }
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
    }
}