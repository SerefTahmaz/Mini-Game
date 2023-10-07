using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class cLevelSelectView : cView
{
    [SerializeField] private cPlayerNameInputController m_NameSelect;
    [SerializeField] private cLevelSelectUIController m_LevelSelect;
    [Inject] private cUIManager m_UIManager;

    public override void Activate()
    {
        base.Activate();

        if (cSaveDataHandler.PlayerName() == "NewPlayer")
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
