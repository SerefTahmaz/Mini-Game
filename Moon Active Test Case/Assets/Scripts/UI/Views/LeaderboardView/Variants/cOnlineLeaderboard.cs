using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using AINamesGenerator;
using Dan.Main;
using Dan.Models;
using DG.Tweening;
using SimonSays.Managers.SaveManager;
using UnityEngine;

namespace SimonSays.UI.Leaderboard
{
    public static class cOnlineLeaderboard
    {
        private static string publicLeaderboardKey = "38ed723f6061e48ab4c7b9d7d25b29d2b2da42c43b222cf506b6fc2e0f388a08";
    
        public static void GetLeaderBoard(Action<bool, cLeaderBoardView.LeaderBoardUnitWrapper[],cLeaderBoardView.LeaderBoardUnitWrapper> successCallback)
        {
            bool timeout=false;

            DOVirtual.DelayedCall(4, () =>
            {
                if (timeout)
                {
                    return;
                }
                timeout = true;
                successCallback.Invoke(false,null,null);
            });
        
        
            LeaderboardCreator.GetLeaderboard(publicLeaderboardKey, ((entries) =>
            {
                if (timeout)
                {
                    return;
                }
                timeout = true;
            
                if (entries.Any())
                {
                    cLeaderBoardView.LeaderBoardUnitWrapper[] onlineEntries = new cLeaderBoardView.LeaderBoardUnitWrapper[entries.Length];
                    cLeaderBoardView.LeaderBoardUnitWrapper player=null;
                
                    for (int i = 0; i < entries.Length; i++)
                    {
                        onlineEntries[i] = new cLeaderBoardView.LeaderBoardUnitWrapper() { Entry = entries[i] };

                        if (entries[i].IsMine())
                        {
                            onlineEntries[i].IsPlayer = true;
                            player = onlineEntries[i];
                        }
                    }

                    successCallback.Invoke(true,onlineEntries,player);
                }
                else
                {
                    successCallback.Invoke(false,null,null);
                }
            }));
        }

        private static void SetLeaderBoardEntry(string userName, int score, Action<bool> successCallback)
        {
            LeaderboardCreator.Ping((hasConnection =>
            {
                if (hasConnection)
                {
                    LeaderboardCreator.UploadNewEntry(publicLeaderboardKey, userName, score, ((msg) =>
                    {
                        if (msg)
                        {
                            successCallback.Invoke(true);
                        }
                        else
                        {
                            successCallback.Invoke(false);
                        }
                    }));
                    // Debug.Log("<color=green>Connected leaderboard</color>");
                }
                else
                {
                    // Debug.Log("<color=red>No Connection leaderboard</color>");
                    successCallback.Invoke(false);
                }
            }));
        
        
        }

        public static void SetPlayerEntry(ISaveManager saveManager)
        {
            SetLeaderBoardEntry(saveManager.SaveData.m_PlayerName, saveManager.SaveData.m_MaxCoinCount, b => {});
        }
    }
}