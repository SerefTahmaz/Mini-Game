using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI.ProceduralImage;
using Zenject;

public class cLateralTransition : MonoBehaviour
{
    [SerializeField] private Transform m_Left;
    [SerializeField] private Transform m_Right;
    [SerializeField] private ProceduralImage m_Image;
    [Inject] private ISoundManager m_SoundManager;

    [ContextMenu("Anim")]
    public void Anim(Action onFullCoverScreen, Action onFinish)
    {
        StartCoroutine(Anim());
        IEnumerator Anim()
        {
            var color = new Color(1,1,1,0);
            m_Image.color = color;
            m_Left.DOLocalMove(Vector3.right, .4f);
            m_Right.DOLocalMove(-Vector3.right, .4f);
            yield return new WaitForSeconds(.4f);
            
            onFullCoverScreen.Invoke();
            m_SoundManager.PlaySwoosh();
            
            color.a = 1;
            m_Image.DOColor(color, .3f).SetLoops(2, LoopType.Yoyo);
            m_Image.transform.DORotate(new Vector3(0, 0, 55), .3f).SetRelative(true).SetLoops(2, LoopType.Yoyo);
            m_Image.transform.DOScale(-.4f, .3f).SetRelative(true).SetLoops(2, LoopType.Yoyo);
            yield return new WaitForSeconds(.4f);
            m_Left.DOLocalMove(-Vector3.right*500,.4f);
            m_Right.DOLocalMove(Vector3.right*500, .4f);
            yield return new WaitForSeconds(.4f);
            onFinish.Invoke();
        }
    }
}