using System.Collections;
using System.Collections.Generic;
using SimonSays.Managers;
using SimonSays.Managers.SaveManager;
using UnityEngine;
using Zenject;

namespace SimonSays.UI.LevelSelect
{
    public class cLevelSelectView : cView
    {
        [SerializeField] private cPlayerNameInputController m_NameSelect;
        [SerializeField] private cLevelSelectUIController m_LevelSelect;
        [Inject] private cUIManager m_UIManager;
        [Inject] private ISaveManager m_SaveManager;

        public override void Activate()
        {
            base.Activate();

            if (!m_SaveManager.SaveData.m_IsPlayerSetName)
            {
                m_NameSelect.Activate();
                m_LevelSelect.Deactivate();
            }
            else
            {
                m_NameSelect.Deactivate();
                m_LevelSelect.Activate();
            }
        }

        public void OnNameSelected()
        {
            m_LevelSelect.Activate();
            m_NameSelect.Deactivate();
        }
    
        public void OnLevelSelected()
        {
            m_UIManager.HidePage(Page.LevelSelect);
        }
    }
}