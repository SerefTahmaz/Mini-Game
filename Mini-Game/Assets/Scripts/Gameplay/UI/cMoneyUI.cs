using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using SimonSays.Managers;
using TMPro;
using UnityEngine;
using Zenject;

namespace SimonSays.Gameplay.UI
{
    public class cMoneyUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI m_Text;
        [Inject]private cUIManager m_UIManager;

        public void Fly(int amount)
        {
            (transform as RectTransform).position = FindObjectOfType<Canvas>().transform.position;

            // var clampStorageIndex = Mathf.Clamp(Main.SaveDataPlayer.GetItemCounts()[(int)Item.STORAGE].EntireSize(),
            //     0, Main.Config.m_StoragePercentages.Count-1);
            // var percentage = Main.Config.m_StoragePercentages[clampStorageIndex];
            // float storageAddition = (float)percentage / 100;
            
            var coinsToGain = (int)(amount * (1 + 0));
        
            // Debug.Log(Main.Config.m_HookPercentages[clampStorageIndex] + " Storage percentage");


            m_Text.text = $"+{coinsToGain}";
            var target = m_UIManager.CurrencyManager.transform;
            transform.DOMove(target.position, 1f).SetEase(Ease.InBack).OnComplete((() =>
            {
                m_UIManager.CurrencyManager.GainCurrency(coinsToGain);
                gameObject.SetActive(false);
            }));
        }
    }
}