using System;
using TMPro;
using UnityEngine;

public class cCurrencyBar : MonoBehaviour
{
    public TextMeshProUGUI m_CurrencyText;
    RectTransform m_CurrencyTextTransform;
    Vector3 m_TextScale = new Vector3(1,1 ,1);
    float m_fMeshSpinSpeed;
    
    void Start()
    {
        cCurrencyBarScreen.Instance.RegisterBar(this);
        m_CurrencyTextTransform = m_CurrencyText.rectTransform;
        Refresh(cCurrencyBarScreen.Instance.CurrentCurrencyAmount);
    }
    
    void Update()
    {
        if (m_TextScale.x != 1)
        {
            m_TextScale.x = Mathf.MoveTowards(m_TextScale.x, 1, Time.deltaTime);
            m_TextScale.y = m_TextScale.x;
            m_CurrencyTextTransform.localScale = m_TextScale;
        }

        if (m_fMeshSpinSpeed != 180)
        {
            m_fMeshSpinSpeed = Mathf.Lerp(m_fMeshSpinSpeed, 180, Time.deltaTime);
        }
    }

    public void Refresh(int currency)
    {
        m_CurrencyText.SetText("{0}", currency);
        m_TextScale.x = 1.25f;
        m_TextScale.y = m_TextScale.x;
        m_CurrencyTextTransform.localScale = m_TextScale;
        m_fMeshSpinSpeed = 1800;
    }
}
