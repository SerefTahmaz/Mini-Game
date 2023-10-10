using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Seref.Utils;
using SimonSays.Managers;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace SimonSays.UI
{
    public class cSmoothLayoutController : MonoBehaviour
    {
        [SerializeField] private Transform m_StaticGOTransform;
        [SerializeField] private Transform m_DynamicGOTransform;
        [SerializeField] private ScrollRect m_ScrollView;
        [SerializeField] private float m_Speed = 1;
        [Inject] private IObjectPooler m_ObjectPooler;
    
        private Dictionary<Transform, LayoutHelper> m_LayoutDict = new Dictionary<Transform, LayoutHelper>();
        private List<Transform> m_DynamicTransforms = new List<Transform>();
        private Transform m_FocusTransform;
    
        private class LayoutHelper
        {
            public Transform StaticTransform;
            public int Rank;
        }

        public void AddLayoutUnit(Transform ins, int rank)
        {
            var staticBody = m_ObjectPooler.Spawn("SmoothLayoutSlot", m_StaticGOTransform).transform;
            staticBody.SetParentResetTransform(ins);
            staticBody.SetParent(m_StaticGOTransform);
            ins.SetParent(m_DynamicGOTransform);
            m_LayoutDict.Add(ins, new LayoutHelper(){StaticTransform = staticBody, Rank = rank});
            m_DynamicTransforms.Add(ins);
        }

        public void UpdateRank(Transform ins, int rank)
        {
            m_LayoutDict[ins].Rank = rank;
        }

        public void SetFocusTransform(Transform ins)
        {
            m_FocusTransform = ins;
        }

        // Update is called once per frame
        void Update()
        {
            if(m_FocusTransform == null) return;
        
            AdjustScrollView();
            AdjustVerticalLayout();
        }

        private void AdjustVerticalLayout()
        {
            var speed = Time.deltaTime * 5 * m_Speed;
            foreach (var VARIABLE in m_DynamicTransforms)
            {
                VARIABLE.transform.position = Vector3.Lerp(VARIABLE.transform.position,
                    m_LayoutDict[VARIABLE].StaticTransform.transform.position, speed);

                var rectT = VARIABLE.transform as RectTransform;
                var rect = rectT.rect;

                var rectT2 = m_LayoutDict[VARIABLE].StaticTransform.transform as RectTransform;
                var rect2 = rectT2.rect;

                Vector4 temp = new Vector4(rect.x, rect.y, rect.width, rect.height);
                Vector4 temp2 = new Vector4(rect2.x, rect2.y, rect2.width, rect2.height);
                var temp3 = Vector4.Lerp(temp, temp2, speed);

                // rectT.anchoredPosition = new Vector2(temp3.x, temp3.y);
                rectT.sizeDelta = new Vector2(temp3.z, temp3.w);
            }
        }

        private void AdjustScrollView()
        {
            float value = 1 - (float)m_LayoutDict[m_FocusTransform].StaticTransform.GetSiblingIndex()
                / m_StaticGOTransform.childCount;
            m_ScrollView.verticalNormalizedPosition = Mathf.Lerp(-.2f, 1, value);
        }

        public void ClearAll()
        {
            for (var index = m_DynamicTransforms.Count - 1; index >= 0; index--)
            {
                var VARIABLE = m_DynamicTransforms[index];
                m_ObjectPooler.DeSpawn(m_LayoutDict[VARIABLE].StaticTransform.gameObject);
                m_DynamicTransforms.RemoveAt(index);
            }

            m_FocusTransform = null;
            m_LayoutDict.Clear();
        }

        public void FixLayout()
        {
            var staticOrdered = m_DynamicTransforms.OrderBy((transform1 => m_LayoutDict[transform1].Rank))
                .Select((transform1 => m_LayoutDict[transform1].StaticTransform)).ToArray();
            for (var index = 0; index < staticOrdered.Length; index++)
            {
                staticOrdered[index].SetSiblingIndex(index);
            }
        }
    }
}