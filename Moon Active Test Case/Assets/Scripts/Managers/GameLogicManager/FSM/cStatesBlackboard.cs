using FiniteStateMachine;
using UnityEngine;

namespace SimonSays.Managers.GameManager
{
    public class cStatesBlackboard : MonoBehaviour
    {
        public cStateBase m_Empty;
        public cStateBase m_MenuState;
        public cStateBase m_GameplayState;
        public cStateBase m_FailState;
    }
}