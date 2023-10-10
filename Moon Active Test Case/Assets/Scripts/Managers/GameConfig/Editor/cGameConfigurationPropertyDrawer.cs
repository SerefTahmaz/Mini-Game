using UnityEditor;
using UnityEngine;

namespace SimonSays.Managers.Config.Editor
{
    [CustomPropertyDrawer(typeof(cGameConfiguration))]
    public class cGameConfigurationPropertyDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
        
            EditorGUI.BeginProperty(position, label, property);
            Rect rectFoldout = new Rect(position.min.x, position.min.y, position.size.x, EditorGUIUtility.singleLineHeight);
            property.isExpanded = EditorGUI.Foldout(rectFoldout, property.isExpanded, label);
            int lines = 1;
            if (property.isExpanded) {
                Rect rectType = new Rect(position.min.x, position.min.y + lines++ * EditorGUIUtility.singleLineHeight, position.size.x, EditorGUIUtility.singleLineHeight);
                EditorGUI.IntSlider(rectType,property.FindPropertyRelative("m_ButtonCount")  , 1 , 25);
                Rect rectDuration = new Rect(position.min.x, position.min.y + lines++ * EditorGUIUtility.singleLineHeight, position.size.x, EditorGUIUtility.singleLineHeight);
                EditorGUI.PropertyField(rectDuration, property.FindPropertyRelative("m_EachStepPointCount"));
                Rect rectCooldown = new Rect(position.min.x, position.min.y + lines++ * EditorGUIUtility.singleLineHeight, position.size.x, EditorGUIUtility.singleLineHeight);
                EditorGUI.PropertyField(rectCooldown, property.FindPropertyRelative("m_GameTimeInSeconds"));
                Rect rectPower = new Rect(position.min.x, position.min.y + lines++ * EditorGUIUtility.singleLineHeight, position.size.x, EditorGUIUtility.singleLineHeight);
                EditorGUI.PropertyField(rectPower, property.FindPropertyRelative("m_RepeatMode"));
                Rect rectSpeed = new Rect(position.min.x, position.min.y + lines++ * EditorGUIUtility.singleLineHeight, position.size.x, EditorGUIUtility.singleLineHeight);
                EditorGUI.PropertyField(rectSpeed, property.FindPropertyRelative("m_GameSpeed"));
            }
            EditorGUI.EndProperty();
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label) {
            int totalLines = 1;

            if (property.isExpanded) {
                totalLines += 5;
            }

            return EditorGUIUtility.singleLineHeight * totalLines + EditorGUIUtility.standardVerticalSpacing * (totalLines - 1);
        }
    }
}