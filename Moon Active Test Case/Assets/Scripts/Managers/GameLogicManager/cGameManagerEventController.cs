using System;

public class cGameManagerEventController
{
    private Action m_OnStartButtonEvent = delegate {  };
    private Action m_OnGameStartEvent = delegate {  };
    private Action m_OnDeadEvent = delegate {  };
    private Action m_OnLevelCompleteEvent = delegate {  };
    private Action m_OnGameStartBeforeLevelDestroy = delegate {  };
    
    public Action OnStartButtonEvent
    {
        get => m_OnStartButtonEvent;
        set => m_OnStartButtonEvent = value;
    }
    public Action OnGameStartEvent
    {
        get => m_OnGameStartEvent;
        set => m_OnGameStartEvent = value;
    }
    
    public Action OnDeadEvent
    {
        get => m_OnDeadEvent;
        set => m_OnDeadEvent = value;
    }
    
    public Action OnLevelCompleteEvent
    {
        get => m_OnLevelCompleteEvent;
        set => m_OnLevelCompleteEvent = value;
    }
    
    public Action OnGameStartBeforeLevelDestroy
    {
        get => m_OnGameStartBeforeLevelDestroy;
        set => m_OnGameStartBeforeLevelDestroy = value;
    }
}