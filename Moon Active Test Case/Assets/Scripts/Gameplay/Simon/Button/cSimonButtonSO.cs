using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Simon Button", menuName = "Simon Button/Button")]
public class cSimonButtonSO : ScriptableObject
{
    [SerializeField] private Material m_ColorMat;
    [SerializeField] private AudioClip m_OnLightSound;

    public Material ColorMat => m_ColorMat;

    public AudioClip OnLightSound => m_OnLightSound;
}
