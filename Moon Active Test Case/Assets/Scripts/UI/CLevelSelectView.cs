using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CLevelSelectView : cView
{
    [SerializeField] private cPlayerNameInputController m_NameSelect;
    [SerializeField] private cLevelSelectUIController m_LevelSelect;

    public override void Activate()
    {
        base.Activate();

        if (cSaveDataHandler.PlayerName()=="")
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
        cUIManager.Instance.HidePage(Page.LevelSelect);
    }
}
