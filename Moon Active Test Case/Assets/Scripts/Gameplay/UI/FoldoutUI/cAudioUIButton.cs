using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cAudioUIButton : MonoBehaviour
{
    [SerializeField] private GameObject m_DisableGO;

    private void Awake()
    {
        var audiostate = PlayerPrefs.GetInt("AudioState", 1);

        if (audiostate == 1)
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
        var audiostate = PlayerPrefs.GetInt("AudioState", 1);

        if (audiostate == 1)
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
        PlayerPrefs.SetInt("AudioState",1);
        m_DisableGO.SetActive(false);
        cSoundManager.Instance.SetActive( true);
    }
    
    public void DisableAudio()
    {
        PlayerPrefs.SetInt("AudioState",0);
        m_DisableGO.SetActive(true);
        cSoundManager.Instance.SetActive( false);
    }
}
