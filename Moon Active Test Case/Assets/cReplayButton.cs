using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cReplayButton : MonoBehaviour
{
    public void Onclick()
    {
        cGameLogicManager.Instance.Replay();
    }
}
