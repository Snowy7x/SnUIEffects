using System;
using Toolbox.Editor;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Snowy.Utils
{
    public class SnEditorGUI
    {
        public static void DrawDefaultScriptField(Editor editor)
        {
            EditorGUI.BeginDisabledGroup(true);
            EditorGUILayout.ObjectField("Script", MonoScript.FromMonoBehaviour(editor.target as MonoBehaviour), typeof(MonoScript), false);
            EditorGUI.EndDisabledGroup();
        }
        
        public static void DrawTitle(string title)
        {
            EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
            EditorGUILayout.LabelField(title, SnGUISkin.titleStyle);
            EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
        }
        
        public static void DrawSectionTitle(string title)
        {
            EditorGUILayout.LabelField(title, SnGUISkin.sectionTitleStyle);
        }
        
        public static void BeginSection(string title)
        {
            EditorGUILayout.BeginVertical(SnGUISkin.boxStyle);
            if (!string.IsNullOrEmpty(title))
            {
                EditorGUILayout.Space(10);
                DrawSectionTitle(title);
                EditorGUILayout.Space(10);
            }
        }
        
        public static void BeginSection(string title, ref bool foldout)
        {
            EditorGUILayout.BeginVertical(SnGUISkin.boxStyle);
            if (!string.IsNullOrEmpty(title))
            {
                EditorGUILayout.Space(10);
                foldout = EditorGUILayout.Foldout(foldout, title, true, SnGUISkin.foldoutTitleStyle);
                EditorGUILayout.Space(10);
            }
        }
        
        public static void EndSection()
        {
            EditorGUILayout.Space();
            EditorGUILayout.EndVertical();
        }
        
        public static void DrawHelpBox(string message, MessageType type = MessageType.Info)
        {
            EditorGUILayout.HelpBox(message, type);
        }

        public static void InlineEditor(SerializedProperty property, Type type)
        {
            // if property is null return
            if (property == null) return;
            // if property is not an object reference return
            // Draw the field with a edit button
            EditorGUILayout.BeginHorizontal();
            // Draw field
            EditorGUILayout.PropertyField(property, true);
            if (GUILayout.Button("Edit", GUILayout.Width(50)))
            {
                // Property set expanded
                property.isExpanded = !property.isExpanded;
            }
            EditorGUILayout.EndHorizontal();
            
            var value = property.objectReferenceValue;
            
            
            // Draw the expanded property
            if (property.isExpanded)
            {
                // if value is not serialized object return
                if (!(value != null)) return;
                EditorGUI.indentLevel+=2;
                var serializedObject = new SerializedObject(value);
                serializedObject.Update();
                var iterator = serializedObject.GetIterator();
                iterator.NextVisible(true);
                while (iterator.NextVisible(false))
                {
                    EditorGUILayout.PropertyField(iterator, true);
                }
                serializedObject.ApplyModifiedProperties();
                EditorGUI.indentLevel-=2;
            }
        }

        public static string OpenFolderPanel(string chooseExportDirectory)
        {
            return EditorUtility.OpenFolderPanel(chooseExportDirectory, "", "");
        }
    }
}