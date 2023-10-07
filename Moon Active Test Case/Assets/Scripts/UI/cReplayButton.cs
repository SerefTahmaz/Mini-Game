using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class cReplayButton : cButton
{
    [Inject] private cGameLogicStateMachine m_GameLogicManager;
    
    public override void OnClick()
    {
        base.OnClick();
        m_GameLogicManager.Replay();
    }
}