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


public class cSimonSaysController : MonoBehaviour
{
    public List<cSimonButton> SimonButtons;

    private List<cSimonButton> CurrentMatchSelectionList = new List<cSimonButton>();

    private int currentIndex = 0;

    private float m_Speed = 1;
    
    public void Init(List<cSimonButton> buttons, cGameConfiguration gameConfiguration)
    {
        SimonButtons=buttons;
        m_Speed = gameConfiguration.m_GameSpeed;
    }

    private void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition + new Vector3(0, 0, 5));
        if (Physics.Raycast(ray, out var hit) && Input.GetMouseButtonDown(0))
        {
            if (hit.collider.attachedRigidbody&& hit.collider.attachedRigidbody.TryGetComponent(out cSimonButton button) && button.m_IsSelectable)
            {
                if (button == CurrentMatchSelectionList[currentIndex])
                {
                    button.Select();
                    currentIndex++;

                    if (currentIndex >= CurrentMatchSelectionList.Count)
                    {
                        currentIndex = 0;
                        foreach (var VARIABLE in SimonButtons)
                        {
                            VARIABLE.m_IsSelectable = false;
                        }

                        cGameLogicManager.Instance.OnSuccessTurn();
                        DOVirtual.DelayedCall(1, () =>
                        {
                            AddRound();
                        });
                    }
                }
                else
                {
                    currentIndex = 0;
                    foreach (var VARIABLE in SimonButtons)
                    {
                        VARIABLE.Deselect();
                    }
                    
                    foreach (var VARIABLE in SimonButtons)
                    {
                        VARIABLE.m_IsSelectable = false;
                    }

                    StartCoroutine(WrongAnim());
                    IEnumerator WrongAnim()
                    {
                        for (int i = 0; i < 5; i++)
                        {
                            foreach (var VARIABLE in SimonButtons)
                            {
                                VARIABLE.Light();
                            }

                            yield return new WaitForSeconds(.15f);
                            
                            foreach (var VARIABLE in SimonButtons)
                            {
                                VARIABLE.Unlight();
                            }
                            yield return new WaitForSeconds(.15f);
                        }
                        
                        yield return new WaitForSeconds(.5f);
                        
                        CurrentMatchSelectionList.Clear();
                        cGameLogicManager.Instance.OnFail();
                    }
                   
                }
            }
        }
    }

    public void AddRound()
    {
        var rndButton = SimonButtons.OrderBy((button => Random.value)).FirstOrDefault();
        CurrentMatchSelectionList.Add(rndButton);
        ShowSequence(CurrentMatchSelectionList);
    }

    private void ShowSequence(List<cSimonButton> sequence)
    {
        StartCoroutine(SequenceAnim());
        IEnumerator SequenceAnim()
        {
            foreach (var VARIABLE in sequence)
            {
                VARIABLE.Light(.5f / m_Speed);
                yield return new WaitForSeconds(1 / m_Speed);
                VARIABLE.Unlight();
            }
            
            StartPlayerSelection();
        }
    }

    private void StartPlayerSelection()
    {
        foreach (var VARIABLE in SimonButtons)
        {
            VARIABLE.m_IsSelectable = true;
        }
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(cSimonSaysController))]
public class TemplateEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if (GUILayout.Button("START"))
        {
            (target as cSimonSaysController).AddRound();
        }
    }
}
#endif