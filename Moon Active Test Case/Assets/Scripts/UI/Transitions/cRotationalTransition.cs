using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI.ProceduralImage;

public class cRotationalTransition : MonoBehaviour
{
    [SerializeField] private RectTransform m_Pivot;
    [SerializeField] private RectTransform m_Icon;
    [SerializeField] private RectTransform m_IconOutline;
    [SerializeField] private ProceduralImage m_BG;
    
    public void Anim(Action onFullCoverScreen, Action onFinish)
    {
        StartCoroutine(Anim());
        
        IEnumerator Anim()
        {
            m_Pivot.DORotate(Vector3.forward * 102, 0);
            m_BG.DOFade(0, 0);
            m_Icon.DOScale(.65f, 0);
            m_IconOutline.DOScale(.65f, 0);
            
            m_Icon.GetComponent<ProceduralImage>().DOFade(1, 0);
            m_IconOutline.GetComponent<ProceduralImage>().DOFade(.43f, 0);
            
            yield return null;
            
            m_BG.DOFade(1, .3f).SetDelay(.2f);
            m_Pivot.DORotate(-Vector3.forward * 120, 1).SetRelative(true);
            
            yield return new WaitForSeconds(.4f);
            
            onFullCoverScreen.Invoke();
            cSoundManager.Instance.PlaySwoosh();
            
            m_Icon.gameObject.SetActive(true);
            m_Icon.DOScale(.3f, .3f).SetRelative(true);
            m_IconOutline.gameObject.SetActive(true);
            m_IconOutline.DOScale(.3f, .3f).SetRelative(true);

            yield return new WaitForSeconds(.3f);

            m_Icon.DOScale(1.4f, .4f).SetEase(Ease.InQuad).SetRelative(true);
            m_IconOutline.DOScale(2f, .4f).SetEase(Ease.InQuad).SetRelative(true);

            m_Pivot.DORotate(-Vector3.forward * 180, .5f).SetEase(Ease.InQuad).SetRelative(true).OnComplete((() =>
            {
                    
            }));
            
            m_BG.DOFade(0, .4f).SetDelay(.2f);
            
            yield return new WaitForSeconds(.2f);
            
            m_Icon.GetComponent<ProceduralImage>().DOFade(.5f, .2f);
            m_IconOutline.GetComponent<ProceduralImage>().DOFade(-2f, .2f).SetRelative(true);
            
            yield return new WaitForSeconds(.2f);
            
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
}