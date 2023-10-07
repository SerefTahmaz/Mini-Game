using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class cMainMenuView : MonoBehaviour
{
    [SerializeField] private List<RectTransform> m_MenuImages;
    [SerializeField] private HorizontalLayoutGroup m_HorizontalLayoutGroup;
    [SerializeField] private List<cView> m_Menus;
    [Inject] private ISoundManager m_SoundManager;
    
    private Rect m_StartScale;
    private int m_ActiveIndex=-1;
    private Tween t;
    private int callNumber;

    private void Start()
    {
        m_StartScale = m_MenuImages[0].rect;

        OnValueChanged((float)2 / 3);
    }

    public void OnValueChanged(float value)
    {
        OnValueChangedTask(value).Forget();
    }

    private async UniTaskVoid OnValueChangedTask(float value)
    {
        callNumber++;
        int t2 = callNumber;
        await UniTask.Delay(TimeSpan.FromSeconds(.05f));
        if(callNumber != t2) return;
            
        int nextIndex = Mathf.FloorToInt(value * 2);
        
        t.Complete(true);
            
        int activeIndex = m_ActiveIndex;
            
        m_Menus[nextIndex].Activate();
        if (activeIndex != -1)
        {
            m_Menus[activeIndex].Deactivate();
        }
            
        m_SoundManager.PlayClick();


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