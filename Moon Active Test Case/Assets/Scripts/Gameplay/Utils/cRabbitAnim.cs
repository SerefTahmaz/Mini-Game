using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SimonSays.Gameplay
{
    public class cRabbitAnim : MonoBehaviour
    {
        [SerializeField] private List<cSimonButton> m_SimonButtons;

        public void LightBlue()
        {
            m_SimonButtons[0].EnableLight(.5f);
        }

        public void LightYellow()
        {
            m_SimonButtons[1].EnableLight(.5f);
        }

        public void LightRed()
        {
            m_SimonButtons[2].EnableLight(.5f);
        }

        public void LightGreen()
        {
            m_SimonButtons[3].EnableLight(.5f);
        }
    }
}