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
    private Tween m_FloatTween;
    private int m_CurrentCallNumber;

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
        //Avoid float lerping
        m_CurrentCallNumber++;
        int lastCallNumber = m_CurrentCallNumber;
        await UniTask.Delay(TimeSpan.FromSeconds(.05f));
        if(m_CurrentCallNumber != lastCallNumber) return;
            
        //Set desired index
        int nextIndex = Mathf.FloorToInt(value * 2);
        int activeIndex = m_ActiveIndex;
        m_Menus[nextIndex].Activate();
        if (activeIndex != -1)
        {
            m_Menus[activeIndex].Deactivate();
        }
            
        m_SoundManager.PlayClick();
        
        //Tween icons
        m_FloatTween.Complete(true);
        m_FloatTween=DOVirtual.Float(0, 1, .5f, f =>
        {
            TweenIcon(nextIndex, f, activeIndex); 
            
        }).OnComplete((() =>
        {
            TweenIcon(nextIndex, 1, activeIndex); 
        }));
        m_ActiveIndex = nextIndex;
    }

    private void TweenIcon(int nextIndex, float f, int activeIndex)
    {
        //Make Focus
        m_MenuImages[nextIndex].sizeDelta = Vector2.Lerp(new Vector2(m_StartScale.width, m_StartScale.height),
            1.41f * new Vector2(m_StartScale.width, m_StartScale.height), f);

        //Reverse Focus
        if (activeIndex != -1)
        {
            m_MenuImages[activeIndex].sizeDelta = Vector2.Lerp(new Vector2(m_StartScale.width, m_StartScale.height),
                1.41f * new Vector2(m_StartScale.width, m_StartScale.height), 1 - f);
        }

        FixLayout();
    }
    
    private void FixLayout()
    {
        m_HorizontalLayoutGroup.CalculateLayoutInputHorizontal();
        m_HorizontalLayoutGroup.CalculateLayoutInputVertical();
        m_HorizontalLayoutGroup.SetLayoutHorizontal();
        m_HorizontalLayoutGroup.SetLayoutVertical();
    }
}