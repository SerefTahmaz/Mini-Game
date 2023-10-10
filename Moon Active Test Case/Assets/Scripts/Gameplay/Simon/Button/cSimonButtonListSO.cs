using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SimonSays.Gameplay
{
    /// <summary>
    /// <para>Holder for simon button setting list</para>
    /// </summary>
    [CreateAssetMenu(fileName = "Simon Button", menuName = "Simon Button/ButtonList")]
    public class cSimonButtonListSO : ScriptableObject
    {
        [SerializeField] private List<cSimonButtonSO> m_SimonButtonSOs;

        public List<cSimonButtonSO> SimonButtonSOs => m_SimonButtonSOs;
    }
}