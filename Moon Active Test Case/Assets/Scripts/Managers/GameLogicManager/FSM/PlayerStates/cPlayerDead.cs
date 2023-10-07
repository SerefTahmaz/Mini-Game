using System;
using System.Collections;
using System.Linq;
using DG.Tweening;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

namespace FiniteStateMachine
{
    public class cPlayerDead : cStateBase
    {
        cCharacterStateMachine StateMachine => m_StateMachine as cCharacterStateMachine;

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