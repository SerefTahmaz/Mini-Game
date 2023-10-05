using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using AINamesGenerator;
using Dan.Main;
using DG.Tweening;
using UnityEngine;

public class cOnlineLeaderboard
{
    private string publicLeaderboardKey = "38ed723f6061e48ab4c7b9d7d25b29d2b2da42c43b222cf506b6fc2e0f388a08";
    private string leaderboardKey = "leaderboard-09960";
    private string secretKey =
        "0652c98053368103708279f5784aebb2ae18248a80f832ceb39a1453ccac6d1907a420f1841b46a815aa4bd870bd995b4fbf56ea7b33640ee86295b283409961ed5a8ce8a60b198c8b61a628292025e3e7e67d4ae115cf6400e6e67cc4d013e7a67b56e1c69b9f21834facd7278783b83aef65b367fcb7ca82351ba19c4308ea";

    public cLeaderBoardView.LeaderBoardUnitWrapper[] m_Entries;
    
    public void GetLeaderBoard(Action<bool> successCallback)
    {
        m_Entries = null;

        LeaderboardCreator.GetLeaderboard(publicLeaderboardKey, ((msg) =>
        {
            if (msg.Any())
            {
                cLeaderBoardView.LeaderBoardUnitWrapper[] onlineEntries = new cLeaderBoardView.LeaderBoardUnitWrapper[msg.Length];
                
                for (int i = 0; i < msg.Length; i++)
                {
                    onlineEntries[i] = new cLeaderBoardView.LeaderBoardUnitWrapper() { Entry = msg[i] };

                    if (msg[i].IsMine())
                    {
                        onlineEntries[i].IsPlayer = true;
                        cSaveDataHandler.GameConfiguration.CurrentRank = msg[i].Rank;
                        cSaveDataHandler.Save();
                    }
                }

                m_Entries = onlineEntries;
                successCallback.Invoke(true);
            }
            else
            {
                successCallback.Invoke(false);
            }
        }));
    }

    public void SetLeaderBoardEntry(string userName, int score, Action<bool> successCallback)
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
                Debug.Log("<color=green>Connected leaderboard</color>");
            }
            else
            {
                Debug.Log("<color=red>No Connection leaderboard</color>");
                successCallback.Invoke(false);
            }
        }));
        
        
    }

    public void SetPlayerEntry()
    {
        SetLeaderBoardEntry(cSaveDataHandler.PlayerName(), cSaveDataHandler.GameConfiguration.MaxCoinCount, b => {});
    }
}