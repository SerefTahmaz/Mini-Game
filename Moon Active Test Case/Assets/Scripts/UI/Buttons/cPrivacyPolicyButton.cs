using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class cPrivacyPolicyButton : cButton
{
    [SerializeField] private string m_PrivacyPolicyURL;

    public override void OnClick()
    {
        base.OnClick();
        Application.OpenURL(m_PrivacyPolicyURL);
    }
}
