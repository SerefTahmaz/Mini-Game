using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class cReplayButton : cButton
{
    [Inject] private cGameManagerStateMachine m_GameManager;
    
    public override void OnClick()
    {
        base.OnClick();
        m_GameManager.Replay();
    }
}