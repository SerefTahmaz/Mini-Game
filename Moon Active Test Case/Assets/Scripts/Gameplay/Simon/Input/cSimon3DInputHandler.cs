using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class cSimon3DInputHandler : MonoBehaviour, ISimonInputHandler
{
    [Inject] private IInputManager m_InputManager;
    
    public Action<cSimonButton> OnInput { get; set; }

    private void Awake()
    {
        m_InputManager.OnInputDown += CheckInput;
    }

    private void CheckInput()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition + new Vector3(0, 0, 5));
        if (Physics.Raycast(ray, out var hit))
        {
            if (hit.collider.attachedRigidbody && hit.collider.attachedRigidbody.TryGetComponent(out cSimonButton button) &&
                button.IsSelectable)
            {
                OnInput.Invoke(button);
            }
        }
    }

    private void OnDestroy()
    {
        m_InputManager.OnInputDown -= CheckInput;
    }
}