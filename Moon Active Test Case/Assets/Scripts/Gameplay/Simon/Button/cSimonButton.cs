using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Zenject;

public class cSimonButton : MonoBehaviour
{
    [SerializeField] private Renderer m_Renderer;
    [SerializeField] private Transform m_Center;
    [Inject] private ISoundManager m_SoundManager;

    private Tween m_LightTween;
    private cSimonButtonSO m_SimonButtonSO;
    private bool m_IsSelectable;

    public bool IsSelectable
    {
        get => m_IsSelectable;
        private set => m_IsSelectable = value;
    }

    public void Init(cSimonButtonSO simonButtonSo, float scale)
    {
        m_SimonButtonSO = simonButtonSo;
        m_Renderer.material = simonButtonSo.ColorMat;
        m_Center.transform.localScale = Vector3.one * scale;
    }

    public void EnableLight(float duration=Single.PositiveInfinity)
    {
        m_LightTween.Kill();
        m_Renderer.material.EnableKeyword("_EMISSION");
        m_LightTween=DOVirtual.DelayedCall(duration, DisableLight);
        if(m_SimonButtonSO) m_SoundManager.PlayClip(m_SimonButtonSO.OnLightSound);
    }
    
    public void DisableLight()
    {
        m_LightTween.Kill();
        m_Renderer.material.DisableKeyword("_EMISSION");
    }
    
    public void Deselect()
    {
        DisableLight();
    }

    public void Select()
    {
        if(!IsSelectable) return;
        EnableLight(.5f);
    }

    public void SetSelectable(bool state)
    {
        IsSelectable = state;
    }
}