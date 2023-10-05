using System;

[Serializable]
public class cGameConfiguration
{
    public int m_ButtonCount=4;
    public int m_EachStepPointCount=1;
    public int m_GameTimeInSeconds=50;
    public bool m_RepeatMode=true;
    public float m_GameSpeed = 1;

    public override string ToString()
    {
        return $"button Count: {m_ButtonCount}," +
               $"Each Step Point Count: {m_EachStepPointCount}," +
               $"Game Time In Seconds Count: {m_GameTimeInSeconds}," +
               $"Repeat Mode: {m_RepeatMode}," +
               $"Game Speed: {m_GameSpeed},";
    }
}