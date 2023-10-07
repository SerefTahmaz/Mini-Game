using System.Collections.Generic;
using UnityEngine;

public class cCurrencyBarScreen : cSingleton<cCurrencyBarScreen>
{
    List<cCurrencyBar> m_CurrencyBars = new List<cCurrencyBar>();

    public int CurrentCurrencyAmount
    {
        get => cSaveDataHandler.GameConfiguration.m_CurrentCoinCount;
        set
        {
            cSaveDataHandler.GameConfiguration.m_CurrentCoinCount = value;
            PlayerMaxScore = value;
        }
    }
    
    public int PlayerMaxScore
    {
        get =>  cSaveDataHandler.GameConfiguration.m_MaxCoinCount;
        set
        {
            if (value > PlayerMaxScore)
            {
                cSaveDataHandler.GameConfiguration.m_MaxCoinCount = value;
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
