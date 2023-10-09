using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI.ProceduralImage;
using Zenject;

public class cRotationalTransition : MonoBehaviour
{
    [SerializeField] private RectTransform m_Pivot;
    [SerializeField] private RectTransform m_Icon;
    [SerializeField] private RectTransform m_IconOutline;
    [SerializeField] private ProceduralImage m_BG;
    [Inject] private ISoundManager m_SoundManager;

    public async UniTaskVoid Anim(Action onFullCoverScreen, Action onFinish)
    {
        m_Pivot.DORotate(Vector3.forward * 102, 0);
        m_BG.DOFade(0, 0);
        m_Icon.DOScale(.65f, 0);
        m_IconOutline.DOScale(.65f, 0);
            
        m_Icon.GetComponent<ProceduralImage>().DOFade(1, 0);
        m_IconOutline.GetComponent<ProceduralImage>().DOFade(.43f, 0);

        await UniTask.DelayFrame(1);
            
        m_BG.DOFade(1, .3f).SetDelay(.2f);
        m_Pivot.DORotate(-Vector3.forward * 120, 1).SetRelative(true);
            
        await UniTask.Delay(TimeSpan.FromSeconds(.4f));
            
        onFullCoverScreen.Invoke();
        m_SoundManager.PlaySwoosh();
            
        m_Icon.gameObject.SetActive(true);
        m_Icon.DOScale(.3f, .3f).SetRelative(true);
        m_IconOutline.gameObject.SetActive(true);
        m_IconOutline.DOScale(.3f, .3f).SetRelative(true);

        await UniTask.Delay(TimeSpan.FromSeconds(.3f));

        m_Icon.DOScale(1.4f, .4f).SetEase(Ease.InQuad).SetRelative(true);
        m_IconOutline.DOScale(2f, .4f).SetEase(Ease.InQuad).SetRelative(true);

        m_Pivot.DORotate(-Vector3.forward * 180, .5f).SetEase(Ease.InQuad).SetRelative(true).OnComplete((() =>
        {
                    
        }));
            
        m_BG.DOFade(0, .4f).SetDelay(.2f);
            
        await UniTask.Delay(TimeSpan.FromSeconds(.2f));
            
        m_Icon.GetComponent<ProceduralImage>().DOFade(.5f, .2f);
        m_IconOutline.GetComponent<ProceduralImage>().DOFade(-2f, .2f).SetRelative(true);
            
        await UniTask.Delay(TimeSpan.FromSeconds(.2f));
            
        m_Icon.gameObject.SetActive(false);
        m_IconOutline.gameObject.SetActive(false);
            
        m_Pivot.DORotate(Vector3.forward * 102, 0);
        m_BG.DOFade(0, 0);
        m_Icon.DOScale(.65f, 0);
        m_IconOutline.DOScale(.65f, 0);
            
        m_Icon.GetComponent<ProceduralImage>().DOFade(1, 0);
        m_IconOutline.GetComponent<ProceduralImage>().DOFade(.43f, 0);
            
        onFinish.Invoke();
    }
}