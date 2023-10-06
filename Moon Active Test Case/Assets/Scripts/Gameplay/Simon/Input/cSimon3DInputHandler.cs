using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cSimon3DInputHandler : MonoBehaviour, ISimonInputHandler
{
    public Action<cSimonButton> OnInput { get; set; }

    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition + new Vector3(0, 0, 5));
        if (Physics.Raycast(ray, out var hit) && Input.GetMouseButtonDown(0))
        {
            if (hit.collider.attachedRigidbody&& hit.collider.attachedRigidbody.TryGetComponent(out cSimonButton button) && button.m_IsSelectable)
            {
                OnInput.Invoke(button);
            }
        }
    }
}