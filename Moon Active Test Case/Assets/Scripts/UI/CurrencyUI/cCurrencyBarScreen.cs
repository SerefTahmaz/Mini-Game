using System.Collections.Generic;
using SimonSays.Managers.SaveManager;
using UnityEngine;
using Zenject;

namespace SimonSays.UI.Currency
{
    public class cCurrencyBarScreen : MonoBehaviour
    {
        List<cCurrencyBar> m_CurrencyBars = new List<cCurrencyBar>();
        [Inject] private ISaveManager m_SaveManager;

        public int CurrentCurrencyAmount
        {
            get => m_SaveManager.SaveData.m_CurrentCoinCount;
            set
            {
                m_SaveManager.SaveData.m_CurrentCoinCount = value;
                PlayerMaxScore = value;
            }
        }
    
        public int PlayerMaxScore
        {
            get =>  m_SaveManager.SaveData.m_MaxCoinCount;
            set
            {
                if (value > PlayerMaxScore)
                {
                    m_SaveManager.SaveData.m_MaxCoinCount = value;
                }
            }
        }

        public void RegisterBar(cCurrencyBar bar)
        {
            m_CurrencyBars.Add(bar);
        }
    
        public void SpendCurrency(int currency)
        {
            CurrentCurrencyAmount -= currency;
        
            foreach (var bar in m_CurrencyBars)
            {
                bar.Refresh(CurrentCurrencyAmount);
            }
        }
    
        public void GainCurrency(int currency)
        {
            CurrentCurrencyAmount += currency;
        
            foreach (var bar in m_CurrencyBars)
            {
                bar.Refresh(CurrentCurrencyAmount);
            }
        }
    
        public void Refresh(int currency)
        {
            foreach (var bar in m_CurrencyBars)
            {
                bar.Refresh(currency);
            }
        }
    }
}