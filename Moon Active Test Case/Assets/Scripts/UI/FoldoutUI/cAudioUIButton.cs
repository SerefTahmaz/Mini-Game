using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class cAudioUIButton : MonoBehaviour
{
    [SerializeField] private GameObject m_DisableGO;
    [Inject] private ISoundManager m_SoundManager;

    private void Awake()
    {
        var audiostate = cSaveDataHandler.GameConfiguration.m_AudioState;;

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
        var audiostate = cSaveDataHandler.GameConfiguration.m_AudioState;

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
        cSaveDataHandler.GameConfiguration.m_AudioState = true;
        m_DisableGO.SetActive(false);
        m_SoundManager.SetActive( true);
    }
    
    public void DisableAudio()
    {
        cSaveDataHandler.GameConfiguration.m_AudioState = false;
        m_DisableGO.SetActive(true);
        m_SoundManager.SetActive( false);
    }
}
