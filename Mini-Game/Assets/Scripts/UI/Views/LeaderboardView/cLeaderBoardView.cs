using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using AINamesGenerator;
using Cysharp.Threading.Tasks;
using Dan.Main;
using Dan.Models;
using DG.Tweening;
using Seref.Utils;
using SimonSays.Managers;
using SimonSays.Managers.SaveManager;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Zenject;
using Random = UnityEngine.Random;

namespace SimonSays.UI.Leaderboard
{
    public class cLeaderBoardView : cView
    {
        [SerializeField] private Transform m_LayoutTranform;
        [SerializeField] private cSmoothLayoutController m_SmoothLayoutController;
        [Inject] private IObjectPooler m_ObjectPooler;
        [Inject] private ISaveManager m_SaveManager;
    
        private bool m_Selected;
        private cLeaderBoardSlotController m_PlayerSlot;
        private List<cLeaderBoardSlotController> m_Slots = new List<cLeaderBoardSlotController>();

        public class LeaderBoardUnitWrapper
        {
            public Entry Entry = new Entry();
            public bool IsPlayer;
        }

        void Start()
        {
            StartAsync().Forget();
        }

        private async UniTaskVoid StartAsync()
        {
            await UniTask.Delay(TimeSpan.FromSeconds(.5f));
            SendPlayerEntry().Forget();
            await UniTask.Delay(TimeSpan.FromSeconds(.5f));
            CheckBoard().Forget();
        }

        public async UniTaskVoid SendPlayerEntry()
        {
            while (true)
            {
                cOnlineLeaderboard.SetPlayerEntry(m_SaveManager);
                await UniTask.Delay(TimeSpan.FromSeconds(20));
            }
        }

        private async UniTaskVoid CheckBoard()
        {
            while (true)
            {
                cOnlineLeaderboard.GetLeaderBoard((success, entries,player) =>
                {
                    List<LeaderBoardUnitWrapper> selectedEntries = new List<LeaderBoardUnitWrapper>();
                    if (success)
                    {
                        AddOnlineEntries(selectedEntries, entries, player);
                    }
                    else
                    {
                        AddRandomEntries(selectedEntries);
                    }
                
                    OnLeaderboardLoaded(selectedEntries).Forget();
                });
                await UniTask.Delay(TimeSpan.FromSeconds(360));
            }
        }

        private void AddOnlineEntries(List<LeaderBoardUnitWrapper> selectedEntries, LeaderBoardUnitWrapper[] entries, LeaderBoardUnitWrapper player)
        {
            selectedEntries.AddRange(entries);
            if (player != null)
            {
                m_SaveManager.SaveData.m_CurrentRank = player.Entry.Rank;
                m_SaveManager.Save();
            }
            else
            {
                selectedEntries.Add(CreatePlayerUnitWrapper());
            }
        }
        
        private void AddRandomEntries(List<LeaderBoardUnitWrapper> selectedEntries)
        {
            selectedEntries.AddRange(cRandomAILeaderboard.GetRandomEntries(30, m_SaveManager));
        }

        private LeaderBoardUnitWrapper CreatePlayerUnitWrapper()
        {
            return new LeaderBoardUnitWrapper()
            {
                Entry = new Entry()
                {
                    Rank = m_SaveManager.SaveData.m_CurrentRank,
                    Score = m_SaveManager.SaveData.m_MaxCoinCount,
                    Username = m_SaveManager.SaveData.m_PlayerName
                },
                IsPlayer = true
            };
        }

        private async UniTaskVoid OnLeaderboardLoaded(List<LeaderBoardUnitWrapper> scores)
        {
            //Clear
            m_SmoothLayoutController.ClearAll();
            for (int i = m_Slots.Count - 1; i >= 0; i--)
            {
                m_Slots[i].ResetState();
                m_ObjectPooler.DeSpawn(m_Slots[i].gameObject);
                m_Slots.RemoveAt(i);
            }
            m_PlayerSlot = null;
        
            await UniTask.DelayFrame(1);
        
            //Generate
            foreach (var leaderboardEntry in scores) {
                var ins = m_ObjectPooler.Spawn<cLeaderBoardSlotController>("LeaderboardSlot", m_LayoutTranform);
                ins.transform.ResetTransform();
                ins.Init();
                ins.m_StartCount = (int)leaderboardEntry.Entry.Score;
                ins.m_PlayerName = leaderboardEntry.Entry.Username;
                ins.m_Rank = leaderboardEntry.Entry.Rank;
                ins.UpdateUI();

                if (leaderboardEntry.IsPlayer)
                {
                    m_PlayerSlot = ins;
                    m_PlayerSlot.m_PlayerName = m_SaveManager.SaveData.m_PlayerName;
                    m_PlayerSlot.UpdateUI();
                }
                
                m_Slots.Add(ins);
                m_SmoothLayoutController.AddLayoutUnit(ins.transform, ins.m_Rank);
            }
            m_PlayerSlot.Selected();
            m_SmoothLayoutController.SetFocusTransform(m_PlayerSlot.transform);
        
            FixRanks();
        }

        public override void Activate()
        {
            base.Activate();

            FixRanks();
        }

        private void FixRanks()
        {
            if(m_PlayerSlot == null) return;
        
            m_PlayerSlot.m_StartCount = m_SaveManager.SaveData.m_MaxCoinCount;
            m_PlayerSlot.m_PlayerName = m_SaveManager.SaveData.m_PlayerName;

            var baseRank = m_Slots
                .Select((slot => slot.m_Rank))
                .OrderByDescending((i => i))
                .FirstOrDefault();
            baseRank -= m_LayoutTranform.childCount-1;

            var orderedList = m_Slots
                .OrderByDescending((slot => slot.m_StartCount)).ToArray();

            for (var index = 0; index < orderedList.Length; index++)
            {
                var VARIABLE = orderedList[index];
                VARIABLE.m_Rank = index + baseRank;
                VARIABLE.UpdateUI();
            }

            m_SaveManager.SaveData.m_CurrentRank = m_PlayerSlot.m_Rank;

            foreach (var VARIABLE in m_Slots)
            {
                m_SmoothLayoutController.UpdateRank(VARIABLE.transform, VARIABLE.m_Rank);
            }
            m_SmoothLayoutController.FixLayout();
        }
    }
}