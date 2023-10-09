using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "GameLevel", menuName = "GameLevel/GameLevel")]
public class cGameLevelSO : ScriptableObject
{
    [SerializeField] private Sprite m_Icon;
    [SerializeField] private TextAsset m_ConfigFile;
    [SerializeField] private string m_LevelName;
    [SerializeField] private Color m_Color;

    public Sprite Icon => m_Icon;
    public TextAsset ConfigFile => m_ConfigFile;
    public string LevelName => m_LevelName;
    public Color Color1 => m_Color;
}
