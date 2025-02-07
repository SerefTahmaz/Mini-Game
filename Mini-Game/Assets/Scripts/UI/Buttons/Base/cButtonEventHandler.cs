using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace SimonSays.UI
{
    public class cButtonEventHandler : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler, IPointerExitHandler
    {
        [SerializeField] private UnityEvent m_OnEnter;
        [SerializeField] private UnityEvent m_OnClick;
        [SerializeField] private UnityEvent m_OnExit;

        public void OnPointerEnter(PointerEventData eventData)
        {
            m_OnEnter.Invoke();
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            m_OnClick.Invoke();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            m_OnExit.Invoke();
        }
    }
}