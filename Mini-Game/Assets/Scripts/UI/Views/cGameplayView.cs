using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace SimonSays.UI
{
    public class cGameplayView : cView
    {
        [SerializeField] private UnityEvent m_OnActivate;
        
        public override void Activate()
        {
            base.Activate();
            m_OnActivate.Invoke();
        }
    }
}