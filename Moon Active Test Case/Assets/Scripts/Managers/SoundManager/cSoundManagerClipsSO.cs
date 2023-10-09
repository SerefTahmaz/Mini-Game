using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Clips data for sound manager
/// </summary>
[CreateAssetMenu(fileName = "SoundManagerClips", menuName = "Scriptable Objects/Sound Manager Clips SO")]
public class cSoundManagerClipsSO : ScriptableObject
{
    [Tooltip("Plays on creating simon button")]
    [SerializeField] private AudioClip m_Pops;
    [SerializeField] private AudioClip m_SuccessSound;
    [SerializeField] private AudioClip m_FailSound;
    [Tooltip("Generic button click")]
    [SerializeField] private AudioClip m_Click;
    [SerializeField] private AudioClip m_OnMouseEnter;
    [SerializeField] private AudioClip m_Swoosh;

    public AudioClip Pops => m_Pops;
    public AudioClip SuccessSound => m_SuccessSound;
    public AudioClip FailSound => m_FailSound;
    public AudioClip Click => m_Click;
    public AudioClip OnMouseEnter => m_OnMouseEnter;
    public AudioClip Swoosh => m_Swoosh;
}
