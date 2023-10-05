using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class cSimonButton : MonoBehaviour
{
    [SerializeField] private Renderer m_Rend;
    [SerializeField] private Transform m_Center;

    public bool Selected;
    public bool IsSelectable;

    private Tween light;

    private cSimonButtonSO SimonButtonSo;

    private void Awake()
    {
        cGameLogicManager.Instance.m_OnSelected += () =>
        {
            Deselect();
        };
    }

    public void Init(cSimonButtonSO simonButtonSo, float scale)
    {
        SimonButtonSo = simonButtonSo;
        m_Rend.material = simonButtonSo.m_ColorMat;
        m_Center.transform.localScale = Vector3.one * scale;
    }

    public void Light(float duration=Single.PositiveInfinity)
    {
        light.Kill();
        m_Rend.material.EnableKeyword("_EMISSION");
        light=DOVirtual.DelayedCall(duration, Unlight);
        cSoundManager.Instance.PlayTrack(SimonButtonSo.m_OnLightSound);
    }
    
    public void Unlight()
    {
        light.Kill();
        m_Rend.material.DisableKeyword("_EMISSION");
    }
    
    public void Deselect()
    {
        Unlight();
        Selected = false;
    }

    public void Select()
    {
        if(!IsSelectable) return;
        
        cGameLogicManager.Instance.m_OnSelected.Invoke();
        Selected = true;
        Light(.5f);
    }
}
