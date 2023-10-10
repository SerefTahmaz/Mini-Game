using System.Collections;
using System.Collections.Generic;
using SimonSays.Managers.SaveManager;
using UnityEngine;
using Zenject;

namespace SimonSays.UI
{
    public class cHapticUIButton : MonoBehaviour
    {
        [SerializeField] private GameObject m_DisableGO;
        [Inject] private ISaveManager m_SaveManager;

        private void Awake()
        {
            var hapticState = m_SaveManager.SaveData.m_HapticState;;

            if (hapticState)
            {
                EnableHaptic();
            }
            else
            {
                DisableHaptic();
            }
        }

        public void OnClick()
        {
            var hapticState = m_SaveManager.SaveData.m_HapticState;

            if (hapticState)
            {
                DisableHaptic();
            }
            else
            {
                EnableHaptic();
            }
        }

        public void EnableHaptic()
        {
            m_SaveManager.SaveData.m_HapticState = true;
            m_DisableGO.SetActive(false);
        }
    
        public void DisableHaptic()
        {
            m_SaveManager.SaveData.m_HapticState = false;
            m_DisableGO.SetActive(true);
        }
    }
}