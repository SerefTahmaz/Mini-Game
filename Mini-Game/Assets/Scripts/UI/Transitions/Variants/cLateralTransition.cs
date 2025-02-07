using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using SimonSays.Managers;
using UnityEngine;
using UnityEngine.UI.ProceduralImage;
using Zenject;

namespace SimonSays.UI
{
    public class cLateralTransition : MonoBehaviour
    {
        [SerializeField] private GameObject m_Left;
        [SerializeField] private GameObject m_Right;
        [SerializeField] private ProceduralImage m_Image;
        [Inject] private ISoundManager m_SoundManager;

        public async UniTaskVoid Anim(Action onFullCoverScreen, Action onFinish)
        {
            var color = new Color(1,1,1,0);
            m_Image.color = color;
            m_Left.SetActive(true);
            m_Left.transform.localScale = new Vector3(0, 1, 1);
            m_Left.transform.DOScaleX(1, .4f);
            m_Right.SetActive(true);
            m_Right.transform.localScale = new Vector3(0, 1, 1);
            m_Right.transform.DOScaleX(1, .4f);
            await UniTask.Delay(TimeSpan.FromSeconds(.4f));
            
            onFullCoverScreen.Invoke();
            m_SoundManager.PlaySwoosh();
            
            color.a = 1;
            m_Image.DOColor(color, .3f).SetLoops(2, LoopType.Yoyo);
            m_Image.transform.DORotate(new Vector3(0, 0, 55), .3f).SetRelative(true).SetLoops(2, LoopType.Yoyo);
            m_Image.transform.DOScale(-.4f, .3f).SetRelative(true).SetLoops(2, LoopType.Yoyo);
            await UniTask.Delay(TimeSpan.FromSeconds(.4f));
            m_Left.transform.DOScaleX(0, .4f);
            m_Right.transform.DOScaleX(0, .4f);
            await UniTask.Delay(TimeSpan.FromSeconds(.4f));
            m_Left.SetActive(false);
            m_Right.SetActive(false);
            onFinish.Invoke();
        }
    }
}