using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class cMoneyUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI m_Text;

    public int MoneyAmount;

    public void Fly(int amount)
    {
        (transform as RectTransform).position = FindObjectOfType<Canvas>().transform.position;

        MoneyAmount = amount;
        // var clampStorageIndex = Mathf.Clamp(Main.SaveDataPlayer.GetItemCounts()[(int)Item.STORAGE].EntireSize(),
        //     0, Main.Config.m_StoragePercentages.Count-1);
        // var percentage = Main.Config.m_StoragePercentages[clampStorageIndex];
        // float storageAddition = (float)percentage / 100;
            
        var coinsToGain = (int)(MoneyAmount * (1 + 0));
        
        // Debug.Log(Main.Config.m_HookPercentages[clampStorageIndex] + " Storage percentage");


        m_Text.text = $"+{coinsToGain}";
        var target = cCurrencyBarScreen.Instance.transform;
        transform.DOMove(target.position, 1f).SetEase(Ease.InBack).OnComplete((() =>
        {
            cCurrencyBarScreen.Instance.GainCurrency(coinsToGain);
            gameObject.SetActive(false);
        }));
    }
}
