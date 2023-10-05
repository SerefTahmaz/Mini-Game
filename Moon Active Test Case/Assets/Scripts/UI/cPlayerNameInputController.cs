using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class cPlayerNameInputController : cView
{
    [SerializeField] private UnityEvent m_OnNameSelected;
    [SerializeField] private TMP_Text m_InputText;
    [SerializeField] private GameObject m_DisableInteractableGO;

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

            PlayerPrefs.SetString("PlayerName", m_InputText.text);

            DOVirtual.DelayedCall(.35f, () =>
            {
                m_OnNameSelected.Invoke();
            });

        }
        else
        {
            transform.FailShakeUI();
        }
    }
}
