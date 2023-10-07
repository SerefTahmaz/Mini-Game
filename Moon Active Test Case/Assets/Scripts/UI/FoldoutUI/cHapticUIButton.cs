using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class cHapticUIButton : MonoBehaviour
{
    [SerializeField] private GameObject m_DisableGO;
    [Inject] private ISaveManager m_SaveManager;

    private void Awake()
    {
        var audiostate = m_SaveManager.SaveData.m_HapticState;;

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
        var audiostate = m_SaveManager.SaveData.m_HapticState;

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
        m_SaveManager.SaveData.m_HapticState = true;
        m_DisableGO.SetActive(false);
    }
    
    public void DisableAudio()
    {
        m_SaveManager.SaveData.m_HapticState = false;
        m_DisableGO.SetActive(true);
    }
}
