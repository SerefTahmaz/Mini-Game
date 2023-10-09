using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// <para>Used for initializing base simon button</para>
/// </summary>
[CreateAssetMenu(fileName = "Simon Button", menuName = "Simon Button/Button")]
public class cSimonButtonSO : ScriptableObject
{
    [Tooltip("Model color")]
    [SerializeField] private Material m_ColorMat;
    [Tooltip("Play clip when it is lit")]
    [SerializeField] private AudioClip m_OnLightSound;

    public Material ColorMat => m_ColorMat;

    public AudioClip OnLightSound => m_OnLightSound;
}
