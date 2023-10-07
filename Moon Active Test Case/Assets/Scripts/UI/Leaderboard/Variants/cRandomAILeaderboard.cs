using System.Collections;
using System.Collections.Generic;
using System.Linq;
using AINamesGenerator;
using UnityEngine;

public static class cRandomAILeaderboard
{
    public static cLeaderBoardView.LeaderBoardUnitWrapper[] GetRandomEntries(int size, ISaveManager saveManager)
    {
        cLeaderBoardView.LeaderBoardUnitWrapper[] aiEntries = new cLeaderBoardView.LeaderBoardUnitWrapper[size];
        for (int i = 0; i < aiEntries.Length; i++)
        {
            aiEntries[i] = new cLeaderBoardView.LeaderBoardUnitWrapper();
            aiEntries[i].Entry.Username = Utils.GetRandomName();
            var rangeScaler = Mathf.Max(Random.Range(-25, 100) * 1, 0);
            aiEntries[i].Entry.Score = rangeScaler + saveManager.SaveData.m_MaxCoinCount;
        }

        aiEntries[0].Entry.Rank = saveManager.SaveData.m_CurrentRank;
        aiEntries[0].Entry.Score = saveManager.SaveData.m_MaxCoinCount;
        aiEntries[0].Entry.Username = saveManager.SaveData.m_PlayerName;
        aiEntries[0].IsPlayer = true;

        var temp = FixRanks(aiEntries, saveManager);

        return temp;
    }

    public static cLeaderBoardView.LeaderBoardUnitWrapper[] FixRanks(cLeaderBoardView.LeaderBoardUnitWrapper[] entries, ISaveManager saveManager)
    {
        var tempA = entries.OrderBy((wrapper => wrapper.Entry.Score)).ToList();
        var playerEntry = entries.Where((wrapper => wrapper.IsPlayer)).FirstOrDefault();
        var indexPlayer = tempA.IndexOf(playerEntry);

        for (int i = 0; i < tempA.Count; i++)
        {
            tempA[i].Entry.Rank = (i) + (saveManager.SaveData.m_CurrentRank - indexPlayer);
        }

        return tempA.Where((wrapper => wrapper.Entry.Rank > 0)).ToArray();
    }
}