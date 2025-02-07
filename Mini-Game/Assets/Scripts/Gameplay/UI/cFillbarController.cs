using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using SimonSays.Managers.GameManager;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UI.ProceduralImage;
using Zenject;

namespace SimonSays.Gameplay.UI
{
    public class cFillbarController : MonoBehaviour
    {
        [SerializeField] private float m_Duration;
        [SerializeField] private ProceduralImage m_FillImage;
        [SerializeField] private Gradient m_FillColorGradient;
        [SerializeField] private RawImage m_ScrollBG;
        [SerializeField] private float m_ScrollSpeed;
    
        [Inject] private cGameManagerStateMachine m_GameManager;

        private void Awake()
        {
            m_GameManager.GameEvents.OnPlayerInputStartEvent += Fill;
            m_GameManager.GameEvents.OnSuccessTurn += Refresh;
            m_GameManager.GameEvents.OnWrongButtonEvent += () =>
            {
                m_FillImage.DOKill();
            };
        }

        private void Update()
        {
            var rect = m_ScrollBG.uvRect;
            rect.x += Time.deltaTime*m_ScrollSpeed;
            m_ScrollBG.uvRect = rect;
        }

        public void Init(float duration)
        {
            m_Duration = duration;
            Refresh();
        }

        public void Fill()
        {
            m_FillImage.DOFillAmount(1, m_Duration).SetEase(Ease.Linear).OnUpdate((() =>
            {
                m_FillImage.color = m_FillColorGradient.Evaluate(m_FillImage.fillAmount);
            })).OnComplete((() =>
            {
                m_FillImage.DOKill();
                m_GameManager.GameEvents.OnTimeIsUpEvent.Invoke();;
            }));
        }

        public void Refresh()
        {
            m_FillImage.DOKill();
            m_FillImage.fillAmount = 0;
        }
    }
}