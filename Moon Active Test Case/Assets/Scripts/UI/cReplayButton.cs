using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cReplayButton : cButton
{
    public override void OnClick()
    {
        base.OnClick();
        cGameLogicManager.Instance.Replay();
    }
}