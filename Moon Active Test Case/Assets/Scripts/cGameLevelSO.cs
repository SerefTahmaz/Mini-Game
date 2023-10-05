using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu]
public class cGameLevelSO : ScriptableObject
{
    public Sprite m_Icon;
    public TextAsset m_ConfigFile;
    public string m_LevelName;
}
