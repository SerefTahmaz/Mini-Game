using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FiniteStateMachine
{
    public abstract class cStateBase : MonoBehaviour
    {
        public string m_StateName;
        protected cStateMachine m_StateMachine;

        public virtual void InitializeState(string stateName, cStateMachine gameManagerSM)
        {
            this.m_StateName = stateName;
            this.m_StateMachine = gameManagerSM;
        }

        public virtual void Enter()
        {
            m_StateMachine.CurrentStateName = m_StateName;
        }

        public virtual void StateMachineUpdate()
        {
        }

        public virtual void StateMachineFixedUpdate()
        {
        }

        public virtual void Exit()
        {
        }
    }
}