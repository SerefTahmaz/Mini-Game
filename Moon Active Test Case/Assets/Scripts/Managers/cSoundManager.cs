using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;
using Random = UnityEngine.Random;

public class cSoundManager : cSingleton<cSoundManager>
{
    [SerializeField] private List<AudioClip> m_Pops;
    [SerializeField] private List<AudioClip> m_OnLightTracks;
    [SerializeField] private List<AudioClip> m_SuccessSound;
    [SerializeField] private List<AudioClip> m_FailSound;
    [SerializeField] private AudioClip m_Click;
    [SerializeField] private AudioClip m_OnMouseEnter;
    
    [SerializeField] private AudioSource m_AudioSource;
    [SerializeField] private AudioSource m_AmbientSource;

    private AudioClip m_LastAmbientSound;

    private void Start()
    {
        // m_AudioSource.PlayOneShot(m_forestBGSounds[Random.Range(0, m_forestBGSounds.Count-1)], .25f);
    }

    public void PlayPop()
    {
        m_AudioSource.PlayOneShot(m_Pops[Random.Range(0, m_Pops.Count-1)]);
    }
    
    public void PlayLightSound()
    {
        m_AudioSource.PlayOneShot(m_OnLightTracks[Random.Range(0, m_OnLightTracks.Count-1)]);
    }

    public void SuccessSound()
    {
        m_AudioSource.PlayOneShot(m_SuccessSound[Random.Range(0, m_SuccessSound.Count-1)]);
    }
    
    public void FailSound()
    {
        m_AudioSource.PlayOneShot(m_FailSound[Random.Range(0, m_FailSound.Count-1)]);
    }

    public void PlayClick()
    {
        m_AudioSource.PlayOneShot(m_Click);
    }
    
    public void PlayMouseEnter()
    {
        m_AudioSource.PlayOneShot(m_OnMouseEnter);
    }

    public void SetActive(bool state)
    {
        m_AudioSource.mute = !state;
        m_AmbientSource.mute = !state;

        // if (state)
        // {
        //     PlayAmbient(m_LastAmbientSound);
        // }
    }
    
    public bool IsActive()
    {
        return m_AudioSource.enabled;
    }

    public void PlayAmbient(AudioClip levelSoClip)
    {
        m_AmbientSource.Stop();
        m_AmbientSource.clip = levelSoClip;
        m_AmbientSource.volume = .1f;
        m_AmbientSource.Play();
        m_LastAmbientSound = levelSoClip;
    }

    public void PlayTrack(AudioClip onLightSound)
    {
        m_AudioSource.PlayOneShot(onLightSound);
    }
}
