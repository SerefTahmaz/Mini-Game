using System.Collections.Generic;
using UnityEngine;

public class cCurrencyBarScreen : cSingleton<cCurrencyBarScreen>
{
    List<cCurrencyBar> m_CurrencyBars = new List<cCurrencyBar>();

    public int CurrentCurrencyAmount
    {
        get => cSaveData.GameConfiguration.CurrentCoinCount;
        set
        {
            cSaveData.GameConfiguration.CurrentCoinCount = value;
            PlayerMaxScore = value;
        }
    }
    
    public int PlayerMaxScore
    {
        get =>  cSaveData.GameConfiguration.MaxCoinCount;
        set
        {
            if (value > PlayerMaxScore)
            {
                cSaveData.GameConfiguration.MaxCoinCount = value;
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
