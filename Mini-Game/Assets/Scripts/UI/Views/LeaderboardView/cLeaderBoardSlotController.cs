﻿using System;
using AINamesGenerator;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace SimonSays.UI.Leaderboard
{
    public class cLeaderBoardSlotController: MonoBehaviour
    {
        [SerializeField] private TMP_Text m_NameText;
        [SerializeField] private TMP_Text m_StarText;
        [SerializeField] private GameObject m_Selected;
        [SerializeField] private TMP_Text m_RankText;

        public int m_StartCount;
        public int m_Rank;
        public string m_PlayerName;

        public void Init()
        {
            m_PlayerName = Utils.GetRandomName();
            m_StartCount = Random.Range(0, 1000);
            UpdateUI();
        }

        public void SetRank(int rank)
        {
            m_Rank = rank;
        }

        public void Selected()
        {
            m_Selected.SetActive(true);
        }

        public void UpdateUI()
        {
            m_StarText.text = m_StartCount.ToString();
            m_NameText.text = m_PlayerName.Length >= 10 ? m_PlayerName.Substring(0,10) : m_PlayerName;
            m_RankText.text = m_Rank.ToString();
        }

        public void ResetState()
        {
            m_Selected.SetActive(false);
            m_StartCount = 0;
            m_Rank = 0;
            m_PlayerName = "";
            UpdateUI();
        }
    }
}