using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

#if UNITY_EDITOR
using UnityEditor;
#endif


public class cSimonSaysGameLogic : MonoBehaviour
{
    [SerializeField] private cSimon3DInputHandler m_Simon3DInputHandler;

    private List<cSimonButton> m_SimonButtons = new List<cSimonButton>();
    private List<cSimonButton> m_CurrentMatchList = new List<cSimonButton>();
    private int m_CurrentIndex = 0;
    private float m_Speed = 1;
    private bool m_RepeatAllSequence;
    private ISimonInputHandler m_SimonInputHandler;

    private void Awake()
    {
        m_SimonInputHandler = m_Simon3DInputHandler;
        m_SimonInputHandler.OnInput += CheckInput;
    }

    private void CheckInput(cSimonButton button)
    {
        if (button == m_CurrentMatchList[m_CurrentIndex])
        {
            NextButton(button);
        }
        else
        {
            WrongButton();
        }
    }

    private void WrongButton()
    {
        m_CurrentIndex = 0;
        foreach (var VARIABLE in m_SimonButtons)
        {
            VARIABLE.Deselect();
        }

        foreach (var VARIABLE in m_SimonButtons)
        {
            VARIABLE.m_IsSelectable = false;
        }

        StartCoroutine(WrongAnim());

        IEnumerator WrongAnim()
        {
            for (int i = 0; i < 5; i++)
            {
                foreach (var VARIABLE in m_SimonButtons)
                {
                    VARIABLE.EnableLight();
                }

                yield return new WaitForSeconds(.15f);

                foreach (var VARIABLE in m_SimonButtons)
                {
                    VARIABLE.DisableLight();
                }

                yield return new WaitForSeconds(.15f);
            }

            yield return new WaitForSeconds(.5f);

            m_CurrentMatchList.Clear();
            cGameLogicManager.Instance.OnFail();
        }
    }

    private void NextButton(cSimonButton button)
    {
        button.Select();
        m_CurrentIndex++;

        if (m_CurrentIndex >= m_CurrentMatchList.Count)
        {
            m_CurrentIndex = 0;
            foreach (var VARIABLE in m_SimonButtons)
            {
                VARIABLE.m_IsSelectable = false;
            }

            cGameLogicManager.Instance.OnSuccessTurn();
            DOVirtual.DelayedCall(1, () => { AddRound(); });
        }
    }

    public void Init(List<cSimonButton> buttons, cGameConfiguration gameConfiguration)
    {
        m_SimonButtons=buttons;
        m_Speed = gameConfiguration.m_GameSpeed;
        m_RepeatAllSequence = gameConfiguration.m_RepeatMode;
    }

    public void AddRound()
    {
        var rndButton = m_SimonButtons.OrderBy((button => Random.value)).FirstOrDefault();
        m_CurrentMatchList.Add(rndButton);
        ShowSequence(m_CurrentMatchList);
    }

    private void ShowSequence(List<cSimonButton> sequence)
    {
        StartCoroutine(SequenceAnim());
        IEnumerator SequenceAnim()
        {
            var sequenceStart = m_RepeatAllSequence ? 0 : sequence.Count-1;
            
            for (var index = sequenceStart; index < sequence.Count; index++)
            {
                var VARIABLE = sequence[index];
                VARIABLE.EnableLight(.5f / m_Speed);
                yield return new WaitForSeconds(1 / m_Speed);
                VARIABLE.DisableLight();
            }

            StartPlayerSelection();
        }
    }

    private void StartPlayerSelection()
    {
        foreach (var VARIABLE in m_SimonButtons)
        {
            VARIABLE.m_IsSelectable = true;
        }
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(cSimonSaysGameLogic))]
public class TemplateEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if (GUILayout.Button("START"))
        {
            (target as cSimonSaysGameLogic).AddRound();
        }
    }
}
#endif