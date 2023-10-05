using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cGameplayView : cView
{
    public override void Activate()
    {
        base.Activate();
        cUIManager.Instance.ShowPage(Page.Start);
    }
}
