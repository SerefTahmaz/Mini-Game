using System;
using System.Collections;
using System.Collections.Generic;
using SimonSays.Managers;
using UnityEngine;
using Zenject;

namespace SimonSays.Gameplay
{
    /// <summary>
    /// Handles detecting input on simon button in the scene using raycasts
    /// </summary>
    public class cSimon3DInputHandler : ISimonInputHandler, IDisposable
    {
        private IInputManager m_InputManager;
        private Action<cSimonButton> m_OnInput = delegate(cSimonButton button) {  }; 

        public Action<cSimonButton> OnInput
        {
            get => m_OnInput;
            set => m_OnInput = value;
        }

        public cSimon3DInputHandler(IInputManager inputManager)
        {
            m_InputManager = inputManager;
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

        public void Dispose()
        {
            m_InputManager.OnInputDown -= CheckInput;
        }
    }
}