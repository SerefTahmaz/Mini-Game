using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Zenject;

public class cPlayerNameInputController : cView
{
    [SerializeField] private UnityEvent m_OnNameSelected;
    [SerializeField] private TMP_Text m_InputText;
    [SerializeField] private GameObject m_DisableInteractableGO;
    private cUIManager m_UIManager;
    private ISoundManager m_SoundManager;
    private ISaveManager m_SaveManager;
    
    [Inject]
    public void Initialize(cUIManager uiManager, ISoundManager soundManager, ISaveManager saveManager) {
        m_UIManager = uiManager;
        m_SoundManager = soundManager;
        m_SaveManager = saveManager;
    }

    public override void Activate()
    {
        base.Activate();
        m_DisableInteractableGO.SetActive(true);

        transform.DOKill();
        transform.localScale = Vector3.zero;
        transform.DOScale(1, .5f).SetEase(Ease.OutBack).OnComplete((() =>
        {
            m_DisableInteractableGO.SetActive(false);
        }));
    }

    public void OnConfirm()
    {
        if (m_InputText.text.Length > 1)
        {
            transform.SuccessShakeUI();

            m_SaveManager.SaveData.m_PlayerName = m_InputText.text;
            m_SaveManager.SaveData.m_IsPlayerSetName = true;
            m_SaveManager.Save();

            DOVirtual.DelayedCall(.35f, () =>
            {
                m_UIManager.LeaderBoardView.SendPlayerEntry().Forget();
                m_OnNameSelected.Invoke();
            });
            m_SoundManager.SuccessSound();
        }
        else
        {
            transform.FailShakeUI();
            m_SoundManager.FailSound();
        }
    }
}
