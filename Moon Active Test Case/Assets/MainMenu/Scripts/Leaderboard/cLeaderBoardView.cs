using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Dan.Main;
using Dan.Models;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class cLeaderBoardView : cView
{
    [SerializeField] private Transform m_LayoutTranform;

    [SerializeField] private cLeaderBoardSlotController m_BoardSlotControllerPrefab;

    [Min(0)]
    [SerializeField] private int m_HiearcyIndex;

    [SerializeField] private ScrollRect m_ScrollView;

    private bool m_Selected;
    private cLeaderBoardSlotController m_PlayerSlot;
    
    public class SmoothLayoutHelper
    {
        public Transform m_SmoothMainBody;
        public Transform m_StaticTargetBody;
    }

    private List<SmoothLayoutHelper> m_LayoutHelpers = new List<SmoothLayoutHelper>();

    [SerializeField] private GameObject m_EmptyPrefab;
    [SerializeField] private float m_Speed;
    
    private Tween m_FlowTween;

    public string publicLeaderboardKey = "59e10e929e180eeaa1f18ea1a5f880a469b063718115098bb55a6a61ccda8197";
    private string leaderboardKey = "leaderboard-97094";

    private string secretKey =
        "8d39621defd2f89ba7636bba481ec9611e6fa657b5688a15266d180083e29567c7cf42b8f6d440bc72a22820e0a66ddd304bfb2706458018fce352f43dfcad5a288523bd785b49a5660786db412605678a21ba62191760938d0ac5e24daf34070acdd221341bbc1feb936f32155f6da3c0ebfe78c7b929ad95615ae8d9b303e7";

    IEnumerator Start()
    {
        yield return new WaitForSeconds(1);
        cGameLogicManager.Instance.LeaderBoardView.SetLeaderBoardEntry(PlayerPrefs.GetString("PlayerName","Default"), cCurrencyBarScreen.Instance.PlayerMaxScore);
    }

    public void GetLeaderBoard()
    {
        LeaderboardCreator.GetLeaderboard(publicLeaderboardKey, ((msg) =>
        {
            OnLeaderboardLoaded(msg);
        }));
    }

    public void SetLeaderBoardEntry(string userName, int score)
    {
        LeaderboardCreator.UploadNewEntry(publicLeaderboardKey, userName, score, ((msg) =>
        {
            GetLeaderBoard();
        }));
    }

    public void OnLeaderboardLoaded(Entry[] scores)
    {
        for (int i = m_LayoutHelpers.Count - 1; i >= 0; i--)
        {
            Destroy(m_LayoutHelpers[i].m_SmoothMainBody.gameObject);
            Destroy(m_LayoutHelpers[i].m_StaticTargetBody.gameObject);
            m_LayoutHelpers.RemoveAt(i);
        }
    
        m_PlayerSlot = null;
        m_FlowTween.Kill();
        
        // for (int i = 0; i < 100; i++)
        // {
        //     var ins = Instantiate(m_BoardSlotControllerPrefab, m_LayoutTranform);
        //     ins.Init();
        //     ins.m_LeaderBoardView = this;
        //     AddLayout(ins.transform);
        // }
    
    
        StartCoroutine(FrameDelay());
    
        IEnumerator FrameDelay()
        {
            yield return null;
            
            foreach (var leaderboardEntry in scores) {
                var ins = Instantiate(m_BoardSlotControllerPrefab, m_LayoutTranform);
                ins.Init();
                ins.m_LeaderBoardView = this;
                AddLayout(ins.transform);
            
                ins.m_StartCount = (int)leaderboardEntry.Score;
                ins.m_PlayerName = leaderboardEntry.Username;
                ins.m_Rank = leaderboardEntry.Rank+1;
                ins.UpdateUI();
    
                if (leaderboardEntry.IsMine())
                {
                    m_PlayerSlot = ins;
                }
            }
            m_PlayerSlot.Selected();
        
            var orderedList = m_LayoutHelpers
                .OrderByDescending((transform1 => transform1.m_SmoothMainBody.GetComponent<cLeaderBoardSlotController>().m_StartCount))
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
                VARIABLE.m_SmoothMainBody.GetComponent<cLeaderBoardSlotController>().SetRank(VARIABLE.m_StaticTargetBody.GetSiblingIndex());
            }
    
        
            m_HiearcyIndex = Mathf.Max(m_LayoutHelpers.Count, m_PlayerIndex-5);
            m_PlayerIndex = GetStaticTransform(m_PlayerSlot.transform).GetSiblingIndex();
            if (m_IsActive == false)
            {
                GetStaticTransform(m_PlayerSlot.transform).SetSiblingIndex(m_HiearcyIndex);
            }
            else
            {
                UpdateLayoutPos(1);
                UpdateLayoutPos(1);
            }
        }
    }

    private int m_PlayerIndex;

    public override void Activate()
    {
        base.Activate();

        FlowPlayerAnim();
    }

    public void FlowPlayerAnim()
    {
        m_FlowTween.Kill();
        m_FlowTween=DOVirtual.Int(m_HiearcyIndex, m_PlayerIndex, 4, value =>
        {
            if(m_PlayerSlot == null) return;
            GetStaticTransform(m_PlayerSlot.transform).SetSiblingIndex(value);
        });
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

        m_ScrollView.verticalNormalizedPosition = 1 - (float)GetStaticTransform(m_PlayerSlot.transform).GetSiblingIndex()
            / GetStaticTransform(m_PlayerSlot.transform).parent.childCount;

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
}
