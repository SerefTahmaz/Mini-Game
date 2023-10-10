using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace SimonSays.UI
{
    public class cStoreWipAnim : MonoBehaviour
    {
        [SerializeField] private Transform m_StartTransform;
        [SerializeField] private Transform m_EndTransform;
        [SerializeField] private float m_Duration;

        public void OnActivate()
        {
            transform.DOComplete();
            transform.rotation = m_StartTransform.rotation;
            transform.DORotate(m_EndTransform.eulerAngles, m_Duration).SetEase(Ease.OutElastic);
        }
    }
}