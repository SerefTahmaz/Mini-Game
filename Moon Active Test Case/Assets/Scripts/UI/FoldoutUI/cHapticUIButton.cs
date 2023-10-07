using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cHapticUIButton : MonoBehaviour
{
    [SerializeField] private GameObject m_DisableGO;

    private void Awake()
    {
        var audiostate = cSaveDataHandler.GameConfiguration.m_HapticState;;

        if (audiostate)
        {
            EnableAudio();
        }
        else
        {
            DisableAudio();
        }
    }

    public void OnClick()
    {
        var audiostate = cSaveDataHandler.GameConfiguration.m_HapticState;

        if (audiostate)
        {
            DisableAudio();
        }
        else
        {
            EnableAudio();
        }
    }

    public void EnableAudio()
    {
        cSaveDataHandler.GameConfiguration.m_HapticState = true;
        m_DisableGO.SetActive(false);
    }
    
    public void DisableAudio()
    {
        cSaveDataHandler.GameConfiguration.m_HapticState = false;
        m_DisableGO.SetActive(true);
    }
}
