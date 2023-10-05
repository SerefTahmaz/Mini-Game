using System.Collections;
using System.Collections.Generic;
using System.Linq;
using AINamesGenerator;
using UnityEngine;

public class cRandomAILeaderboard
{
    public cLeaderBoardView.LeaderBoardUnitWrapper[] GetRandomEntries(int size)
    {
        cLeaderBoardView.LeaderBoardUnitWrapper[] aiEntries = new cLeaderBoardView.LeaderBoardUnitWrapper[size];
        for (int i = 0; i < aiEntries.Length; i++)
        {
            aiEntries[i].Entry.Username = Utils.GetRandomName();
            var rangeScaler = Mathf.Max(Random.Range(-10, 10) * 5, 0);
            aiEntries[i].Entry.Score = rangeScaler * cSaveDataHandler.GameConfiguration.MaxCoinCount + 20;
        }

        aiEntries[0].Entry.Rank = cSaveDataHandler.GameConfiguration.CurrentRank;
        aiEntries[0].Entry.Score = cSaveDataHandler.GameConfiguration.MaxCoinCount;
        aiEntries[0].IsPlayer = true;

        aiEntries = FixRanks(aiEntries);

        return aiEntries;
    }

    public cLeaderBoardView.LeaderBoardUnitWrapper[] FixRanks(cLeaderBoardView.LeaderBoardUnitWrapper[] entries)
    {
        var tempA = entries.OrderBy((wrapper => wrapper.Entry.Score)).ToList();
        var playerEntry = entries.Where((wrapper => wrapper.IsPlayer)).FirstOrDefault();
        var indexPlayer = tempA.IndexOf(playerEntry);

        for (int i = 0; i < entries.Length; i++)
        {
            entries[i].Entry.Rank = (i + 1) + (cSaveDataHandler.GameConfiguration.CurrentRank - indexPlayer);
        }

        return entries.Where((wrapper => wrapper.Entry.Rank > 0)).ToArray();
    }
}