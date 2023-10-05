using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using AINamesGenerator;
using Dan.Main;
using Dan.Models;
using DG.Tweening;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

public class cLeaderBoardView : cView
{
    [SerializeField] private Transform m_LayoutTranform;
    [SerializeField] private cLeaderBoardSlotController m_BoardSlotControllerPrefab;
    [SerializeField] private ScrollRect m_ScrollView;
    [SerializeField] private GameObject m_EmptyPrefab;
    [SerializeField] private float m_Speed;

    private bool m_Selected;
    private cLeaderBoardSlotController m_PlayerSlot;
    private List<SmoothLayoutHelper> m_LayoutHelpers = new List<SmoothLayoutHelper>();
    private Tween m_FlowTween;
    //Boards
    private cRandomAILeaderboard m_RandomAILeaderboard = new cRandomAILeaderboard();
    private cOnlineLeaderboard m_OnlineLeaderboard = new cOnlineLeaderboard();
    
    private class SmoothLayoutHelper
    {
        public Transform m_SmoothMainBody;
        public Transform m_StaticTargetBody;
    }
    
    
    public class LeaderBoardUnitWrapper
    {
        public Entry Entry = new Entry();
        public bool IsPlayer;
    }

    IEnumerator Start()
    {
        yield return new WaitForSeconds(.5f);
        SendPlayerEntry();
        yield return new WaitForSeconds(.5f);
        CheckBoard();
        
    }

    public void SendPlayerEntry()
    {
        m_OnlineLeaderboard.SetPlayerEntry();
        DOVirtual.DelayedCall(20, () =>
        {
            m_OnlineLeaderboard.SetPlayerEntry();
            SendPlayerEntry();
        });
    }

    public void CheckBoard()
    {
        m_OnlineLeaderboard.GetLeaderBoard((success =>
        {
            OnLeaderboardLoaded(success ? m_OnlineLeaderboard.m_Entries : m_RandomAILeaderboard.GetRandomEntries(30));
            DOVirtual.DelayedCall(30, CheckBoard);
        }));
    }

    public void OnLeaderboardLoaded(LeaderBoardUnitWrapper[] scores)
    {
        for (int i = m_LayoutHelpers.Count - 1; i >= 0; i--)
        {
            Destroy(m_LayoutHelpers[i].m_SmoothMainBody.gameObject);
            Destroy(m_LayoutHelpers[i].m_StaticTargetBody.gameObject);
            m_LayoutHelpers.RemoveAt(i);
        }

        m_PlayerSlot = null;
        
        m_FlowTween.Kill();

        StartCoroutine(FrameDelay());
    
        IEnumerator FrameDelay()
        {
            yield return null;
            
            foreach (var leaderboardEntry in scores) {
                var ins = Instantiate(m_BoardSlotControllerPrefab, m_LayoutTranform);
                ins.Init();
                ins.m_LeaderBoardView = this;
                AddLayout(ins.transform);
            
                ins.m_StartCount = (int)leaderboardEntry.Entry.Score;
                ins.m_PlayerName = leaderboardEntry.Entry.Username;
                ins.m_Rank = leaderboardEntry.Entry.Rank+1;
                ins.UpdateUI();
    
                if (leaderboardEntry.IsPlayer)
                {
                    m_PlayerSlot = ins;
                }
            }
            m_PlayerSlot.Selected();
        
            FixRanks();
        }
    }

    public override void Activate()
    {
        base.Activate();

        FixRanks();
    }

    private void FixRanks()
    {
        if(m_PlayerSlot == null) return;
        
        m_PlayerSlot.m_StartCount = cSaveDataHandler.GameConfiguration.MaxCoinCount;

        var baseRank = m_LayoutHelpers
            .Select((transform1 => transform1.m_SmoothMainBody.GetComponent<cLeaderBoardSlotController>().m_Rank))
            .OrderByDescending((i => i))
            .FirstOrDefault();

        var orderedList = m_LayoutHelpers
            .OrderByDescending((transform1 =>
                transform1.m_SmoothMainBody.GetComponent<cLeaderBoardSlotController>().m_StartCount))
            .Select((helper => helper.m_StaticTargetBody));

        foreach (var VARIABLE in orderedList)
        {
            VARIABLE.transform.SetParent(null);
        }

        foreach (var VARIABLE in orderedList)
        {
            VARIABLE.transform.SetParent(m_LayoutTranform);
        }

        foreach (var VARIABLE in m_LayoutHelpers)
        {
            VARIABLE.m_SmoothMainBody.GetComponent<cLeaderBoardSlotController>().m_Rank =
                VARIABLE.m_StaticTargetBody.GetSiblingIndex() + 1 + baseRank;
            
            VARIABLE.m_SmoothMainBody.GetComponent<cLeaderBoardSlotController>().UpdateUI();
        }

        cSaveDataHandler.GameConfiguration.CurrentRank = m_PlayerSlot.m_Rank;
    }

    public void AddLayout(Transform ins)
    {
        var staticBody = Instantiate(m_EmptyPrefab, m_LayoutTranform).transform;
        staticBody.SetParentResetTransform(ins);
        staticBody.SetParent(ins.parent);
        ins.SetParent(ins.parent.parent);

        m_LayoutHelpers.Add(new SmoothLayoutHelper(){m_SmoothMainBody = ins, m_StaticTargetBody = staticBody});
    }

    public Transform GetStaticTransform(Transform ins)
    {
        return m_LayoutHelpers.Where((helper => helper.m_SmoothMainBody == ins)).FirstOrDefault().m_StaticTargetBody;
    }

    private void Update()
    {
        UpdateLayoutPos(Time.deltaTime * 5 * m_Speed);
    }

    private void UpdateLayoutPos(float speed)
    {
        if (m_PlayerSlot == null) return;

        float value = 1 - (float)GetStaticTransform(m_PlayerSlot.transform).GetSiblingIndex()
            / GetStaticTransform(m_PlayerSlot.transform).parent.childCount;
        m_ScrollView.verticalNormalizedPosition = Mathf.Lerp(-1, 1, value);

        foreach (var VARIABLE in m_LayoutHelpers)
        {
            VARIABLE.m_SmoothMainBody.transform.position = Vector3.Lerp(VARIABLE.m_SmoothMainBody.transform.position,
                VARIABLE.m_StaticTargetBody.transform.position, speed);

            var rectT = VARIABLE.m_SmoothMainBody.GetComponent<RectTransform>();
            var rect = rectT.rect;

            var rectT2 = VARIABLE.m_StaticTargetBody.GetComponent<RectTransform>();
            var rect2 = rectT2.rect;

            Vector4 temp = new Vector4(rect.x, rect.y, rect.width, rect.height);
            Vector4 temp2 = new Vector4(rect2.x, rect2.y, rect2.width, rect2.height);
            var temp3 = Vector4.Lerp(temp, temp2, speed);

            // rectT.anchoredPosition = new Vector2(temp3.x, temp3.y);
            rectT.sizeDelta = new Vector2(temp3.z, temp3.w);
        }
    }

    public int GetIndex(Transform ins)
    {
        return GetStaticTransform(ins).GetSiblingIndex();
    }
    
    [ContextMenu("Delete")]
    public void DeleteLeaderBoard()
    {
        LeaderboardCreator.ResetPlayer();
    }

    public void SendPlayerData()
    {
        m_OnlineLeaderboard.SetPlayerEntry();
    }
}
