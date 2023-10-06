using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class cMainMenuView : MonoBehaviour
{
    [SerializeField] private List<RectTransform> m_MenuImages;
    [SerializeField] private Scrollbar m_Scrollbar;
    [SerializeField] private HorizontalLayoutGroup m_HorizontalLayoutGroup;

    [SerializeField] private List<cView> m_Menus;


    private Rect m_StartScale;

    private int m_ActiveIndex=-1;

    private void Start()
    {
        m_StartScale = m_MenuImages[0].rect;
        
        OnValueChanged((float)2/3);
    }

    private Tween t;

    private int callNumber;

    public void OnValueChanged(float value)
    {
        StartCoroutine(test());

        IEnumerator test()
        {
            callNumber++;
            int t2 = callNumber;
            yield return new WaitForSeconds(0.05f);
            if(callNumber != t2) yield break;
            
            int nextIndex = Mathf.FloorToInt(value * 2);
        
            t.Complete(true);

            // Debug.Log(value);
            
            int activeIndex = m_ActiveIndex;
            
            // m_Menus[nextIndex].SetActive(true);
            m_Menus[nextIndex].Activate();
            if (activeIndex != -1)
            {
                // m_Menus[activeIndex].SetActive(false);
                m_Menus[activeIndex].Deactivate();
            }
            
            cSoundManager.Instance.PlayClick();


            t=DOVirtual.Float(0, 1, .5f, f =>
            {
                m_MenuImages[nextIndex].sizeDelta = Vector2.Lerp( new Vector2(m_StartScale.width, m_StartScale.height) , 
                    1.41f *  new Vector2(m_StartScale.width, m_StartScale.height), f);
                
                if (activeIndex != -1)
                {
                    m_MenuImages[activeIndex].sizeDelta =  Vector2.Lerp( new Vector2(m_StartScale.width, m_StartScale.height) , 
                        1.41f *  new Vector2(m_StartScale.width, m_StartScale.height), 1-f);
                }
                
                m_HorizontalLayoutGroup.CalculateLayoutInputHorizontal();
                m_HorizontalLayoutGroup.CalculateLayoutInputVertical();
                m_HorizontalLayoutGroup.SetLayoutHorizontal();
                m_HorizontalLayoutGroup.SetLayoutVertical();

            }).OnComplete((() =>
            {
                m_MenuImages[nextIndex].sizeDelta = Vector2.Lerp( new Vector2(m_StartScale.width, m_StartScale.height) , 
                    1.41f *  new Vector2(m_StartScale.width, m_StartScale.height), 1);
                
                if (activeIndex != -1)
                {
                    m_MenuImages[activeIndex].sizeDelta =  Vector2.Lerp( new Vector2(m_StartScale.width, m_StartScale.height) , 
                        1.41f *  new Vector2(m_StartScale.width, m_StartScale.height), 0);
                }
                
                m_HorizontalLayoutGroup.CalculateLayoutInputHorizontal();
                m_HorizontalLayoutGroup.CalculateLayoutInputVertical();
                m_HorizontalLayoutGroup.SetLayoutHorizontal();
                m_HorizontalLayoutGroup.SetLayoutVertical();
                
            }));
            m_ActiveIndex = nextIndex;
        }
    }
}
