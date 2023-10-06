using System;

public interface ISimonInputHandler
{
    public Action<cSimonButton> OnInput
    {
        get;
        set;
    }
}