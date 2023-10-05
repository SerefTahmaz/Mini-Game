using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cBattleView : cView
{
    public override void Deactivate()
    {
        base.Deactivate();
        // m_Castle.SetActive(false);
    }

    public override void Activate()
    {
        base.Activate();
        // m_Castle.SetActive(true);
    }
}
