using System.Collections.Generic;
using UnityEngine;

public class cCurrencyBarScreen : cSingleton<cCurrencyBarScreen>
{
    List<cCurrencyBar> m_CurrencyBars = new List<cCurrencyBar>();

    public int CurrentCurrencyAmount
    {
        get => PlayerPrefs.GetInt("PlayerCurrency",0);
        set
        {
            PlayerPrefs.SetInt("PlayerCurrency",value);
            PlayerMaxScore = value;
        }
    }
    
    public int PlayerMaxScore
    {
        get => PlayerPrefs.GetInt("PlayerMaxScore",0);
        set
        {
            if (value > PlayerMaxScore)
            {
                PlayerPrefs.SetInt("PlayerMaxScore",value);
            }
        }
    }

    public void Awake()
    {
        // Main.CurrencyBar = this;
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

    // private GUIStyle customButton;
    // private void OnGUI()
    // {
    //     customButton = new GUIStyle("button");
    //     customButton.fontSize = 20;
    //     
    //     GUILayout.BeginArea(new Rect(Screen.width - 130,0, 120, 30));
    //     if (GUILayout.Button("AddMoney",customButton,new []{GUILayout.Width(100), GUILayout.Height(40)}))
    //     {
    //         GainCurrency(100);
    //     }
    //     GUILayout.EndArea();
    // }
}
