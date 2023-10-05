using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cGameplayView : cPage
{
    public override void Activate()
    {
        base.Activate();
        cUIManager.Instance.ShowPage(Page.Start);
    }
}
