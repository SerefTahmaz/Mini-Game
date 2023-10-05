using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cHapticUIButton : MonoBehaviour
{
    [SerializeField] private GameObject m_DisableGO;

    private void Awake()
    {
        var audiostate = PlayerPrefs.GetInt("HapticState", 1);

        if (audiostate == 1)
        {
            EnableHaptic();
        }
        else
        {
            DisableHaptic();
        }
    }

    public void OnClick()
    {
        var audiostate = PlayerPrefs.GetInt("HapticState", 1);

        if (audiostate == 1)
        {
            DisableHaptic();
        }
        else
        {
            EnableHaptic();
        }
        
    }

    public void EnableHaptic()
    {
        PlayerPrefs.SetInt("HapticState",1);
        m_DisableGO.SetActive(false);
        
    }
    
    public void DisableHaptic()
    {
        PlayerPrefs.SetInt("HapticState",0);
        m_DisableGO.SetActive(true);
        
    }
}
