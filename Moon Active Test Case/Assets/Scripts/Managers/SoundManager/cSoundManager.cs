using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

/// <summary>
/// Manager for game audio
/// </summary>
public class cSoundManager : MonoBehaviour, ISoundManager
{
    [SerializeField] private cSoundManagerClipsSO m_SoundManagerClipsSO;
    [SerializeField] private AudioSource m_AudioSource;
    [SerializeField] private AudioSource m_AmbientSource;
    
    private AudioClip m_Pops => m_SoundManagerClipsSO.Pops;
    private AudioClip m_SuccessSound => m_SoundManagerClipsSO.SuccessSound;
    private AudioClip m_FailSound => m_SoundManagerClipsSO.FailSound;
    private AudioClip m_Click => m_SoundManagerClipsSO.Click;
    private AudioClip m_OnMouseEnter => m_SoundManagerClipsSO.OnMouseEnter;
    private AudioClip m_Swoosh => m_SoundManagerClipsSO.Swoosh;
    
    
    private void Awake()
    {
        // m_AudioSource.PlayOneShot(m_forestBGSounds[Random.Range(0, m_forestBGSounds.Count-1)], .25f);
    }

    public void SetActive(bool state)
    {
        m_AudioSource.mute = !state;
        m_AmbientSource.mute = !state;
    }

    public void PlayPop()
    {
        m_AudioSource.PlayOneShot(m_Pops);
    }

    public void SuccessSound()
    {
        m_AudioSource.PlayOneShot(m_SuccessSound);
    }
    
    public void FailSound()
    {
        m_AudioSource.PlayOneShot(m_FailSound);
    }

    public void PlayClick()
    {
        m_AudioSource.PlayOneShot(m_Click);
    }
    
    public void PlayMouseEnter()
    {
        m_AudioSource.PlayOneShot(m_OnMouseEnter);
    }

    public void PlaySwoosh()
    {
        m_AudioSource.PlayOneShot(m_Swoosh);
    }

    public void PlayAmbient(AudioClip clip)
    {
        m_AmbientSource.Stop();
        m_AmbientSource.clip = clip;
        m_AmbientSource.volume = .1f;
        m_AmbientSource.Play();
    }

    public void PlayClip(AudioClip clip)
    {
        m_AudioSource.PlayOneShot(clip);
    }
}