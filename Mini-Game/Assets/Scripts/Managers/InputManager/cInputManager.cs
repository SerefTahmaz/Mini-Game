using System;
using UnityEngine;

namespace SimonSays.Managers
{
    public class cInputManager : MonoBehaviour, IInputManager
    {
        private Action m_OnInputDown = delegate {  };

        public Action OnInputDown
        {
            get => m_OnInputDown;
            set => m_OnInputDown = value;
        }

        void Update()
        {
            if(Input.GetMouseButtonDown(0)) OnInputDown.Invoke();
        }
    }
}