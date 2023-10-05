using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cFailView : cView
{
    public override void Activate()
    {
        base.Activate();
        cUIManager.Instance.ShowPage(Page.LeaderBoardView);
    }
}
