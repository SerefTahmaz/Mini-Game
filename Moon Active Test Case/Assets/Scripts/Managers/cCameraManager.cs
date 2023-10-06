using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cCameraManager : cSingleton<cCameraManager>
{
    [SerializeField] private Camera m_UiCamera;
    [SerializeField] private Camera m_MainCamera;

    public Camera UiCamera => m_UiCamera;

    public Camera MainCamera => m_MainCamera;
}
