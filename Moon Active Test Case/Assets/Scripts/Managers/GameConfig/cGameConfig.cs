using System;
using UnityEngine;

[Serializable]
public struct cGameConfiguration
{
    public int m_ButtonCount;
    public int m_EachStepPointCount;
    public int m_GameTimeInSeconds;
    public bool m_RepeatMode;
    public float m_GameSpeed;

    public override string ToString()
    {
        return $"button Count: {m_ButtonCount}," +
               $"Each Step Point Count: {m_EachStepPointCount}," +
               $"Game Time In Seconds Count: {m_GameTimeInSeconds}," +
               $"Repeat Mode: {m_RepeatMode}," +
               $"Game Speed: {m_GameSpeed},";
    }
}