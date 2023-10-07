using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using Zenject;

public class cLevel : MonoBehaviour
{
    [SerializeField] private Transform m_PosToSpawn;
    [SerializeField] private List<cSimonButtonSO> m_SimonButtonSos;
    [SerializeField] private cSimonSaysGameLogic m_SimonSaysGameLogic;

    private List<cSimonButton> m_Buttons = new List<cSimonButton>();
    private ISoundManager m_SoundManager;
    private IObjectPooler m_ObjectPooler;
    private cGameManagerStateMachine m_GameManager;

    [Inject]
    public void Initialize(IObjectPooler objectPooler, ISoundManager soundManager, cGameManagerStateMachine gameManager) 
    {
        m_ObjectPooler = objectPooler;
        m_SoundManager = soundManager;
        m_GameManager = gameManager;
    }
    
    public void InitLevel(cGameConfiguration gameConfiguration)
    {
        InitSimonLogic(gameConfiguration).Forget();
        m_GameManager.GameEvents.OnGameStartBeforeLevelDestroy += () =>
        {
            foreach (var VARIABLE in m_Buttons)
            {
                m_ObjectPooler.DeSpawn(VARIABLE.gameObject);
            }
        };
    }

    private async UniTaskVoid InitSimonLogic(cGameConfiguration gameConfiguration)
    {
        int index = 0;
        var buttonCount = gameConfiguration.m_ButtonCount;

        for (int i = 0; i < buttonCount; i++)
        {
            float unityAngle = ((float)360 / buttonCount);
            float currentAngle = unityAngle * i;
            var ins = m_ObjectPooler.Spawn<cSimonButton>("SimonButton", m_PosToSpawn);
            ins.transform.localEulerAngles = new Vector3(0, currentAngle, 0);
            ins.Init(m_SimonButtonSos[index % m_SimonButtonSos.Count], Mathf.Clamp((unityAngle / 90), .1f, 1));
            index++;

            m_Buttons.Add(ins);

            ins.transform.localScale = Vector3.zero;
            ins.transform.DOScale(1, .5f).SetEase(Ease.OutBack);
            m_SoundManager.PlayPop();
            await UniTask.Delay(TimeSpan.FromSeconds(.5f));
        }

        m_SimonSaysGameLogic.Init(m_Buttons, gameConfiguration);
        m_SimonSaysGameLogic.AddRound();
    }
}
