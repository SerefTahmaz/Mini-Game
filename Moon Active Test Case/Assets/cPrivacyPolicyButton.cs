using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class cPrivacyPolicyButton : MonoBehaviour
{
    [SerializeField] private string m_PrivacyPolicyURL;

    public void OnClick()
    {
        Application.OpenURL(m_PrivacyPolicyURL);
    }
}
