using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class cSimonButton : MonoBehaviour
{
    [SerializeField] private Renderer m_Rend;
    [SerializeField] private Transform m_Center;

    private Tween m_LightTween;
    private cSimonButtonSO m_SimonButtonSO;
    
    public bool m_Selected;
    public bool m_IsSelectable;

    private void Awake()
    {
        cGameLogicManager.Instance.m_OnSelected += Deselect;
    }

    public void Init(cSimonButtonSO simonButtonSo, float scale)
    {
        m_SimonButtonSO = simonButtonSo;
        m_Rend.material = simonButtonSo.m_ColorMat;
        m_Center.transform.localScale = Vector3.one * scale;
    }

    public void EnableLight(float duration=Single.PositiveInfinity)
    {
        m_LightTween.Kill();
        m_Rend.material.EnableKeyword("_EMISSION");
        m_LightTween=DOVirtual.DelayedCall(duration, DisableLight);
        cSoundManager.Instance.PlayTrack(m_SimonButtonSO.m_OnLightSound);
    }
    
    public void DisableLight()
    {
        m_LightTween.Kill();
        m_Rend.material.DisableKeyword("_EMISSION");
    }
    
    public void Deselect()
    {
        DisableLight();
        m_Selected = false;
    }

    public void Select()
    {
        if(!m_IsSelectable) return;
        
        cGameLogicManager.Instance.m_OnSelected.Invoke();
        m_Selected = true;
        EnableLight(.5f);
    }
}
