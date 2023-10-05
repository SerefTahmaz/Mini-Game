using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class cRestorePurchasesButton : MonoBehaviour
{
    [SerializeField] private TMP_Text m_Text;

    public void OnRestoredPurchase(bool state, string product)
    {
        Debug.Log("STATE " + state);
        Debug.Log("PRODUCT " + product);
        cSoundManager.Instance.SuccessSound();
    }

    public void PlayClick()
    {
        cSoundManager.Instance.PlayClick();
    }
}
