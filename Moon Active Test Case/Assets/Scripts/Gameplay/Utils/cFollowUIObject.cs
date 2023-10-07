using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class cFollowUIObject : MonoBehaviour
{
    [SerializeField] private RectTransform m_FollowObject;
    [SerializeField] private float m_PositionScale;
    [SerializeField] private Vector3 m_Offset;
    [SerializeField] private bool m_Alltime = false;
    [Inject] private cCameraManager m_CameraManager;

    private void Start()
    {
        if (m_Alltime == false)
        {
            var pos =  m_CameraManager.UiCamera.WorldToScreenPoint(m_FollowObject.position);
            pos += m_Offset;
            transform.position = m_CameraManager.MainCamera.ScreenToWorldPoint( (m_PositionScale*pos));
            transform.rotation = m_CameraManager.MainCamera.transform.rotation;
        
            transform.SetParent(m_CameraManager.MainCamera.transform);
        }
    }

    private void Update()
    {
        if (m_Alltime)
        {
            var pos =  m_CameraManager.UiCamera.WorldToScreenPoint(m_FollowObject.position);
            pos += m_Offset;
            transform.position = m_CameraManager.MainCamera.ScreenToWorldPoint( (m_PositionScale*pos));
            transform.rotation = m_CameraManager.MainCamera.transform.rotation;
        }
    }
}
