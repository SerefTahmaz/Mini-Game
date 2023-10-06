using System;

public class cGameManagerEventController
{
    private Action m_OnPlayerInputStartEvent = delegate {  };
    private Action m_OnGameplayStartEvent = delegate {  };
    private Action m_OnTimeIsUpEvent = delegate {  };
    private Action m_OnWrongButtonEvent = delegate {  };
    private Action m_OnSuccessTurn = delegate {  };
    private Action m_OnGameStartBeforeLevelDestroy = delegate {  };
    
    public Action OnPlayerInputStartEvent
    {
        get => m_OnPlayerInputStartEvent;
        set => m_OnPlayerInputStartEvent = value;
    }
    public Action OnGameplayStartEvent
    {
        get => m_OnGameplayStartEvent;
        set => m_OnGameplayStartEvent = value;
    }
    
    public Action OnTimeIsUpEvent
    {
        get => m_OnTimeIsUpEvent;
        set => m_OnTimeIsUpEvent = value;
    }
    
    public Action OnSuccessTurn
    {
        get => m_OnSuccessTurn;
        set => m_OnSuccessTurn = value;
    }
    
    public Action OnGameStartBeforeLevelDestroy
    {
        get => m_OnGameStartBeforeLevelDestroy;
        set => m_OnGameStartBeforeLevelDestroy = value;
    }

    public Action OnWrongButtonEvent
    {
        get => m_OnWrongButtonEvent;
        set => m_OnWrongButtonEvent = value;
    }
}