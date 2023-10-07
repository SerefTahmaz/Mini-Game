using System;
using System.Collections;
using System.Linq;
using DG.Tweening;
using UnityEngine;
using UnityEngine.AI;

namespace FiniteStateMachine
{
    public class cPlayerAttacking : cPlayerCover
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

        public override void Exit()
        {
            base.Exit();
        }
    }
}