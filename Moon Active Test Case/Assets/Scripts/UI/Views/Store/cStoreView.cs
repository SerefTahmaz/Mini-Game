using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

namespace SimonSays.UI
{
    public class cStoreView : cView
    {
        [SerializeField] private UnityEvent m_OnActivate;

        public override void Activate()
        {
            base.Activate();
            m_OnActivate.Invoke();
        }
    }
}