using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cAudioUIButton : MonoBehaviour
{
    [SerializeField] private GameObject m_DisableGO;

    private void Awake()
    {
        var audiostate = cSaveData.GameConfiguration.AudioState;;

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
        var audiostate = cSaveData.GameConfiguration.AudioState;

        if (audiostate)
        {
            DisableAudio();
        }
        else
        {
            EnableAudio();
        }
        cSoundManager.Instance.PlayClick();
    }

    public void EnableAudio()
    {
        cSaveData.GameConfiguration.AudioState = true;
        m_DisableGO.SetActive(false);
        cSoundManager.Instance.SetActive( true);
    }
    
    public void DisableAudio()
    {
        cSaveData.GameConfiguration.AudioState = false;
        m_DisableGO.SetActive(true);
        cSoundManager.Instance.SetActive( false);
    }
}
