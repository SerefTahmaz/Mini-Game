using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Zenject;

public class cLevel : MonoBehaviour
{
    [SerializeField] private cSimonButton m_SimonButtonUnit;
    [SerializeField] private Transform m_PosToSpawn;
    [SerializeField] private List<cSimonButtonSO> m_SimonButtonSos;
    [SerializeField] private cSimonSaysGameLogic m_SimonSaysGameLogic; 
    
    private ISoundManager m_SoundManager;
    private IInstantiator m_Instantiator;

    [Inject]
    public void Initialize(IInstantiator instantiator, ISoundManager soundManager) {
        m_Instantiator = instantiator;
        m_SoundManager = soundManager;
    }
    
    public void InitLevel(cGameConfiguration gameConfiguration)
    {
        InitSimonLogic(gameConfiguration);
    }

    private void InitSimonLogic(cGameConfiguration gameConfiguration)
    {
        StartCoroutine(SpawnParts());

        IEnumerator SpawnParts()
        {
            int index = 0;
            var buttonCount = gameConfiguration.m_ButtonCount;

            List<cSimonButton> buttons = new List<cSimonButton>();

            for (int i = 0; i < buttonCount; i++)
            {
                float unityAngle = ((float)360 / buttonCount);
                float currentAngle = unityAngle * i;
                var ins = m_Instantiator.InstantiatePrefabForComponent<cSimonButton>(m_SimonButtonUnit, m_PosToSpawn);
                ins.transform.localEulerAngles = new Vector3(0, currentAngle, 0);
                ins.Init(m_SimonButtonSos[index % m_SimonButtonSos.Count], Mathf.Clamp((unityAngle / 90), .1f, 1));
                index++;

                buttons.Add(ins);

                ins.transform.localScale = Vector3.zero;
                ins.transform.DOScale(1, .5f).SetEase(Ease.OutBack);
                m_SoundManager.PlayPop();
                yield return new WaitForSeconds(.5f);
            }

            m_SimonSaysGameLogic.Init(buttons, gameConfiguration);
            m_SimonSaysGameLogic.AddRound();
        }
    }
}
