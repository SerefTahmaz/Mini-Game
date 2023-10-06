using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cFollowUIObject : MonoBehaviour
{
    [SerializeField] private RectTransform m_FollowObject;
    [SerializeField] private float m_PositionScale;
    [SerializeField] private Vector3 m_Offset;
    [SerializeField] private bool m_Alltime = false;

    private void Start()
    {
        if (m_Alltime == false)
        {
            var pos =  cCameraManager.Instance.UiCamera.WorldToScreenPoint(m_FollowObject.position);
            pos += m_Offset;
            transform.position = cCameraManager.Instance.MainCamera.ScreenToWorldPoint( (m_PositionScale*pos));
            transform.rotation = cCameraManager.Instance.MainCamera.transform.rotation;
        
            transform.SetParent(cCameraManager.Instance.MainCamera.transform);
        }
    }

    private void Update()
    {
        if (m_Alltime)
        {
            var pos =  cCameraManager.Instance.UiCamera.WorldToScreenPoint(m_FollowObject.position);
            pos += m_Offset;
            transform.position = cCameraManager.Instance.MainCamera.ScreenToWorldPoint( (m_PositionScale*pos));
            transform.rotation = cCameraManager.Instance.MainCamera.transform.rotation;
        }
    }
}
