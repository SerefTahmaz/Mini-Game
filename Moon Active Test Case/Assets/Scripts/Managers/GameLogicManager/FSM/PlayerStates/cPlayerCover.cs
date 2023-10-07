using System.Collections;
using System.Linq;
using DG.Tweening;
using UnityEngine;
using UnityEngine.AI;

namespace FiniteStateMachine
{
    public class cPlayerCover : cStateBase
    {
        cGameLogicStateMachine StateMachine => m_StateMachine as cGameLogicStateMachine;

        public override void Enter()
        {
            base.Enter();
        }

        public override void StateMachineUpdate()
        {
            base.StateMachineUpdate();
        }
    }
}