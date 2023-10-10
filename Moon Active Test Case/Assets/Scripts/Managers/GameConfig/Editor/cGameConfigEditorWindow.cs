using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEditor;
using Object = UnityEngine.Object;

namespace SimonSays.Managers.Config.Editor
{
    /// <summary>
    ///   <para>Editor window for creating and editing game configuration files</para>
    /// </summary>
    [Serializable]
    public class cGameConfigEditorWindow : EditorWindow
    {
        [SerializeField] private cGameConfiguration m_GameConfiguration = 
            new cGameConfiguration() 
            {
                m_ButtonCount = 4,
                m_EachStepPointCount = 1,
                m_GameTimeInSeconds = 50,
                m_RepeatMode = true,
                m_GameSpeed = 1
            };
        [SerializeField] private FileType m_FileType= FileType.json;
        [SerializeField] private TextAsset m_EditedConfig;
    
        private SerializedObject m_SerializedObject;
        private IGameConfigHandler m_JsonGameConfigHandler= new cJsonGameConfigHandler();
        private IGameConfigHandler m_XMLGameConfigHandler= new cXMLGameConfigHandler();

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
            EditorGUILayout.PropertyField(m_SerializedObject.FindProperty("m_GameConfiguration"));

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
            EditorGUILayout.PropertyField(m_SerializedObject.FindProperty("m_FileType"));
            if (GUILayout.Button("Create Game Config"))
            {
                CreateNewConfig(m_GameConfiguration);
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
                        Debug.Log("Invalid Action");
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
                    m_GameConfiguration = m_JsonGameConfigHandler.FileToConfig(m_EditedConfig);
                    break;
                case ".xml":
                    m_GameConfiguration = m_XMLGameConfigHandler.FileToConfig(m_EditedConfig);
                    break;
                default:
                    m_EditedConfig = null;
                    Debug.Log("Invalid Action");
                    break;
            }
        }

        private void ReplaceGUI()
        {
            if (EditorUtility.DisplayDialog("Replacing File",
                    "Are you sure you want to replace the file?", "Yes", "No"))
            {
                var extension = Path.GetExtension(AssetDatabase.GetAssetPath(m_EditedConfig));
                switch (extension)
                {
                    case ".json":
                        CreateConfig(m_GameConfiguration, AssetDatabase.GetAssetPath(m_EditedConfig), FileType.json);
                        break;
                    case ".xml":
                        CreateConfig(m_GameConfiguration, AssetDatabase.GetAssetPath(m_EditedConfig), FileType.xml);
                        break;
                    default:
                        m_EditedConfig = null;
                        Debug.Log("Invalid Action");
                        break;
                }
            }
        }
    
        private void CreateNewConfig(cGameConfiguration gameConfiguration)
        {
            string path;
            switch (m_FileType)
            {
                case FileType.json:
                    path = EditorUtility.SaveFilePanel(
                        "Save config",
                        "",
                        "GameConfig" + ".json",
                        "json");
                    CreateConfig(gameConfiguration,path,m_FileType);
                    break;
                case FileType.xml:
                    path = EditorUtility.SaveFilePanel(
                        "Save config",
                        "",
                        "GameConfig" + ".xml",
                        "xml");
                    CreateConfig(gameConfiguration,path,m_FileType);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void CreateConfig(cGameConfiguration gameConfiguration,string path, FileType fileType)
        {
            if (path.Length != 0)
            {
                switch (fileType)
                {
                    case FileType.json:
                        m_JsonGameConfigHandler.ConfigToFile(path,gameConfiguration);
                        break;
                    case FileType.xml:
                        m_XMLGameConfigHandler.ConfigToFile(path,gameConfiguration);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }
    }
}