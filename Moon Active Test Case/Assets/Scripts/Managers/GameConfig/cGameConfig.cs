using System;
using UnityEngine;

namespace SimonSays.Managers.Config
{
    /// <summary>
    ///   <para>Game configuration data</para>
    /// </summary>
    [Serializable]
    public struct cGameConfiguration
    {
        public int m_ButtonCount;
        public int m_EachStepPointCount;
        [Min(0)] public int m_GameTimeInSeconds;
        public bool m_RepeatMode;
        [Min(0)] public float m_GameSpeed;

        public override string ToString()
        {
            return $"button Count: {m_ButtonCount}," +
                   $"Each Step Point Count: {m_EachStepPointCount}," +
                   $"Game Time In Seconds Count: {m_GameTimeInSeconds}," +
                   $"Repeat Mode: {m_RepeatMode}," +
                   $"Game Speed: {m_GameSpeed},";
        }
    }
}