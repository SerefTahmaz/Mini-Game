using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class cHomeButton : cButton
{
    public override void OnClick()
    {
        base.OnClick();
        cGameLogicManager.Instance.TransitionManager.PlayTransition(cTransitionManager.TransitionType.Lateral, () =>
        {
            cUIManager.Instance.HidePage(Page.LeaderBoardView);
            cUIManager.Instance.HidePage(Page.FailView);
            cUIManager.Instance.ShowPage(Page.Start);
            cUIManager.Instance.ShowPage(Page.MainMenuSliderView);
            cLevelManager.Instance.RemoveLevel();
        }, () =>
        {
            
        });
    }
}
