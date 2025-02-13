using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using SimonSays.Managers;
using SimonSays.Managers.Config;
using SimonSays.Managers.GameManager;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace SimonSays.Gameplay
{
    /// <summary>
    ///   <para>Simon says game logic controller</para>
    /// </summary>
    public class cSimonSaysGameLogic : MonoBehaviour
    {
        [Inject] private ISimonInputHandler m_SimonInputHandler;
        [Inject] private cGameManagerStateMachine m_GameManager;
        [Inject] private ISoundManager m_SoundManager;

        private float m_Speed = 1;
        private bool m_RepeatAllSequence;
        private int m_CurrentIndex = 0;
        private List<cSimonButton> m_CurrentMatchList = new List<cSimonButton>();
        private List<cSimonButton> m_SimonButtons = new List<cSimonButton>();

        private void Awake()
        {
            m_SimonInputHandler.OnInput += CheckInput;

            m_GameManager.GameEvents.OnTimeIsUpEvent += () =>
            {
                WrongButton().Forget();
            };
        }
    
        /// <summary>
        /// <para>Use initialize simon says logic</para>
        /// </summary>
        /// <param name="buttons">simon buttons</param>
        /// /// <param name="gameConfiguration">game configuration for speed etc.</param>
        public void Init(List<cSimonButton> buttons, cGameConfiguration gameConfiguration)
        {
            m_SimonButtons=buttons;
            m_Speed = gameConfiguration.m_GameSpeed;
            m_RepeatAllSequence = gameConfiguration.m_RepeatMode;
            AddRound();
        }

        private void CheckInput(cSimonButton button)
        {
            if (button == m_CurrentMatchList[m_CurrentIndex])
            {
                NextButton(button).Forget();
            }
            else
            {
                WrongButton().Forget();
            }
        }
    
        private async UniTaskVoid NextButton(cSimonButton selectedButton)
        {
            foreach (var button in m_SimonButtons)
            {
                button.Deselect();
            }
            selectedButton.Select();
        
            m_CurrentIndex++;
            if (m_CurrentIndex >= m_CurrentMatchList.Count)
            {
                m_CurrentIndex = 0;
                foreach (var button in m_SimonButtons)
                {
                    button.SetSelectable(false);
                }

                m_GameManager.OnSuccessTurn();
                await UniTask.Delay(TimeSpan.FromSeconds(1));
                AddRound();
            }
        }

        private async UniTaskVoid WrongButton()
        {
            m_GameManager.GameEvents.OnWrongButtonEvent.Invoke();
            m_SoundManager.PlayGameFail();
        
            m_CurrentIndex = 0;
            foreach (var VARIABLE in m_SimonButtons)
            {
                VARIABLE.Deselect();
            }

            foreach (var VARIABLE in m_SimonButtons)
            {
                VARIABLE.SetSelectable(false);
            }

            for (int i = 0; i < 5; i++)
            {
                foreach (var button in m_SimonButtons)
                {
                    button.EnableLight(sound:false);
                }

                await UniTask.Delay(TimeSpan.FromSeconds(.15f));

                foreach (var button in m_SimonButtons)
                {
                    button.DisableLight();
                }

                await UniTask.Delay(TimeSpan.FromSeconds(.15f));
            }

            await UniTask.Delay(TimeSpan.FromSeconds(.5f));
        
            m_CurrentMatchList.Clear();
            m_GameManager.ChangeState(m_GameManager.FailState);
        }

        private void AddRound()
        {
        
            var rndButton = m_SimonButtons.OrderBy((button => Random.value)).FirstOrDefault();
            m_CurrentMatchList.Add(rndButton);
            ShowSequenceAsync(m_CurrentMatchList).Forget();
        }

        private async UniTaskVoid ShowSequenceAsync(List<cSimonButton> sequence)
        {
            var sequenceStart = m_RepeatAllSequence ? 0 : sequence.Count-1;
            
            for (var index = sequenceStart; index < sequence.Count; index++)
            {
                var VARIABLE = sequence[index];
                VARIABLE.EnableLight(.5f / m_Speed);
                await UniTask.Delay(TimeSpan.FromSeconds(.75f / m_Speed));
                VARIABLE.DisableLight();
            }

            EnablePlayerSelection();
        }

        private void EnablePlayerSelection()
        {
            m_GameManager.GameEvents.OnPlayerInputStartEvent.Invoke();
            foreach (var button in m_SimonButtons)
            {
                button.SetSelectable(true);
            }
        }
    }
}