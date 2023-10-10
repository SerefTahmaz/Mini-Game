using System.Collections;
using System.Collections.Generic;
using SimonSays.Managers.GameManager;
using UnityEngine;
using Zenject;

namespace SimonSays.UI
{
    public class cReplayButton : cButton
    {
        [Inject] private cGameManagerStateMachine m_GameManager;
    
        public override void OnClick()
        {
            base.OnClick();
            m_GameManager.Replay();
        }
    }
}