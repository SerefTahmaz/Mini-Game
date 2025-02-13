using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SimonSays.UI.LevelSelect
{
    [CreateAssetMenu(fileName = "GameLevelList", menuName = "GameLevel/GameLevelList")]
    public class cGameLevelListSO : ScriptableObject
    {
        [SerializeField] private List<cGameLevelSO> m_GameLevelSOs;

        public List<cGameLevelSO> GameLevelSOs => m_GameLevelSOs;
    }
}