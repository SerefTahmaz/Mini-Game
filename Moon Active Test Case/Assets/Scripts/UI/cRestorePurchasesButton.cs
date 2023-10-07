using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Zenject;

public class cRestorePurchasesButton : MonoBehaviour
{
    [SerializeField] private TMP_Text m_Text;
    [Inject] private ISoundManager m_SoundManager;

    public void OnRestoredPurchase(bool state, string product)
    {
        Debug.Log("STATE " + state);
        Debug.Log("PRODUCT " + product);
        m_SoundManager.SuccessSound();
    }

    public void PlayClick()
    {
        m_SoundManager.PlayClick();
    }
}
