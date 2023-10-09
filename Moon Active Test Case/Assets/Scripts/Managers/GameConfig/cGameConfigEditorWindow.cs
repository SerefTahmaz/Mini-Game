using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEditor;
using Object = UnityEngine.Object;

/// <summary>
///   <para>Editor window for creating and editing game configuration files</para>
/// </summary>
[Serializable]
public class cGameConfigEditorWindow : EditorWindow
{
    [SerializeField, Min(1)] private int m_ButtonCount = 4;
    [SerializeField] private int m_EachStepPointCount=1;
    [SerializeField, Min(0)] private int m_GameTimeInSeconds=60;
    [SerializeField] private bool m_RepeatMode= true;
    [SerializeField, Min(0)] private float m_GameSpeed=1;
    [SerializeField] private FileType m_FileType= FileType.json;
    [SerializeField] private TextAsset m_EditedConfig;

    private SerializedObject m_SerializedObject;
    private cJsonGameConfigHandler m_JsonGameConfigHandler= new cJsonGameConfigHandler();
    private cXMLGameConfigHandler m_XMLGameConfigHandler= new cXMLGameConfigHandler();

    private enum FileType
    {
        json,
        xml
    }

    private void OnEnable() {
        m_SerializedObject = new SerializedObject(this);
    }
    
    [MenuItem("Game Config/Config Editor")]
    static void Init()
    {
        // Get existing open window or if none, make a new one:
        var window = (cGameConfigEditorWindow)EditorWindow.GetWindow(typeof(cGameConfigEditorWindow), false, "Game Config Editor");
        window.Show();
    }

    void OnGUI()
    {
        m_SerializedObject.Update();

        EditorGUILayout.LabelField("CREATE GAME CONFIG", new GUIStyle(){fontStyle = FontStyle.Bold, normal = new GUIStyleState(){textColor = Color.yellow}});
        EditorGUILayout.IntSlider(m_SerializedObject.FindProperty("m_ButtonCount")  , 1 , 25, new GUIContent("ButtonCount"));
        EditorGUILayout.PropertyField(m_SerializedObject.FindProperty("m_EachStepPointCount"));
        EditorGUILayout.PropertyField(m_SerializedObject.FindProperty("m_GameTimeInSeconds"));
        EditorGUILayout.PropertyField(m_SerializedObject.FindProperty("m_RepeatMode"));
        EditorGUILayout.PropertyField(m_SerializedObject.FindProperty("m_GameSpeed"));
        EditorGUILayout.PropertyField(m_SerializedObject.FindProperty("m_FileType"));
        
        EditorGUILayout.HelpBox("Durations are in Seconds", MessageType.Info);
        
        EditorGUILayout.Space(20);

        CreateConfigGUI();
        
        DisplayOr();

        EditConfigGUI();

        m_SerializedObject.ApplyModifiedProperties();
    }

    private void DisplayOr()
    {
        var assetPreviewCenteredStyle = GUI.skin.GetStyle("Label");
        assetPreviewCenteredStyle.alignment = TextAnchor.UpperCenter;
        assetPreviewCenteredStyle.normal.textColor = Color.white;
        EditorGUILayout.LabelField("OR", assetPreviewCenteredStyle, GUILayout.Height(20));
    }

    private void CreateConfigGUI()
    {
        if (GUILayout.Button("Create Game Config"))
        {
            var gameConfig = new cGameConfiguration()
            {
                m_ButtonCount = m_ButtonCount,
                m_EachStepPointCount = m_EachStepPointCount,
                m_GameTimeInSeconds = m_GameTimeInSeconds,
                m_RepeatMode = m_RepeatMode,
                m_GameSpeed = m_GameSpeed
            };
            CreateNewConfig(gameConfig);
        }
    }

    private void EditConfigGUI()
    {
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.PropertyField(m_SerializedObject.FindProperty("m_EditedConfig"),
            label: new GUIContent("Edit Existing File"));
        if (m_EditedConfig != null)
        {
            var extension = Path.GetExtension(AssetDatabase.GetAssetPath(m_EditedConfig));
            switch (extension)
            {
                case ".json":
                    break;
                case ".xml":
                    break;
                default:
                    m_EditedConfig = null;
                    Debug.Log("Not Supported");
                    break;
            }
        }

        if (GUILayout.Button("Load"))
        {
            LoadGUI();
        }

        if (GUILayout.Button("Replace"))
        {
            ReplaceGUI();
        }

        EditorGUILayout.EndHorizontal();
    }

    private void LoadGUI()
    {
        var extension = Path.GetExtension(AssetDatabase.GetAssetPath(m_EditedConfig));
        switch (extension)
        {
            case ".json":
                var loadedConfigjson = m_JsonGameConfigHandler.Convert(m_EditedConfig);
                m_ButtonCount = loadedConfigjson.m_ButtonCount;
                m_EachStepPointCount = loadedConfigjson.m_EachStepPointCount;
                m_GameTimeInSeconds = loadedConfigjson.m_GameTimeInSeconds;
                m_RepeatMode = loadedConfigjson.m_RepeatMode;
                m_GameSpeed = loadedConfigjson.m_GameSpeed;
                break;
            case ".xml":
                var loadedConfigxml = m_XMLGameConfigHandler.Convert(m_EditedConfig);
                m_ButtonCount = loadedConfigxml.m_ButtonCount;
                m_EachStepPointCount = loadedConfigxml.m_EachStepPointCount;
                m_GameTimeInSeconds = loadedConfigxml.m_GameTimeInSeconds;
                m_RepeatMode = loadedConfigxml.m_RepeatMode;
                m_GameSpeed = loadedConfigxml.m_GameSpeed;
                break;
            default:
                m_EditedConfig = null;
                Debug.Log("Not Supported");
                break;
        }
    }

    private void ReplaceGUI()
    {
        if (EditorUtility.DisplayDialog("Replacing File",
                "Are you sure you want to replace the file?", "Yes", "No"))
        {
            var gameConfig = new cGameConfiguration()
            {
                m_ButtonCount = m_ButtonCount,
                m_EachStepPointCount = m_EachStepPointCount,
                m_GameTimeInSeconds = m_GameTimeInSeconds,
                m_RepeatMode = m_RepeatMode,
                m_GameSpeed = m_GameSpeed
            };

            var extension = Path.GetExtension(AssetDatabase.GetAssetPath(m_EditedConfig));
            switch (extension)
            {
                case ".json":
                    CreateConfig(gameConfig, AssetDatabase.GetAssetPath(m_EditedConfig), FileType.json);
                    break;
                case ".xml":
                    CreateConfig(gameConfig, AssetDatabase.GetAssetPath(m_EditedConfig), FileType.xml);
                    break;
                default:
                    m_EditedConfig = null;
                    Debug.Log("Not Supported");
                    break;
            }
        }
    }
    
    private void CreateNewConfig(cGameConfiguration gameConfiguration)
    {
        var path = EditorUtility.SaveFilePanel(
            "Save config",
            "",
            "GameConfig" + ".xml",
            "xml");

        CreateConfig(gameConfiguration,path,m_FileType);
    }

    private void CreateConfig(cGameConfiguration gameConfiguration,string path, FileType fileType)
    {
        if (path.Length != 0)
        {
            switch (fileType)
            {
                case FileType.json:
                    m_JsonGameConfigHandler.CreateConfig(path,gameConfiguration);
                    break;
                case FileType.xml:
                    m_XMLGameConfigHandler.CreateConfig(path,gameConfiguration);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}