/*
 * MIT License © 2024 Snowy (aka Snowy7x) - See LICENSE file for details.
 * X (Twitter): https://twitter.com/Snowy7x7
 * GitHub: https://github.com/Snowy7x
*/

// Note: Do not touch unless you know what you are doing.

using System;
using Snowy.UI.Effects;
using Snowy.Utils;
using Toolbox.Editor;
using Toolbox.Editor.Drawers;
using Toolbox.Editor.Internal;
using UnityEditor;
using UnityEngine;

namespace Snowy.UI
{
    [CustomPropertyDrawer(typeof(EffectsCollection))]
    public class EffectsCollectionDrawer : PropertyDrawerBase
    {
        static EffectsCollectionDrawer()
        {
            Storage = new PropertyDataStorage<ReorderableListBase, EffectsCollectionDrawer>(false, (p, _) =>
            {
                var list = ToolboxEditorGui.CreateList(p,
                    ListStyle.Round,
                    "Effect",
                    false,
                    true,
                    true,
                    true,
                    true);
                
                // custom element height
                
                // cusotm element header
                list.drawElementCallback = (rect, index, isActive, isFocused) =>
                {
                    var element = list.List.GetArrayElementAtIndex(index);
                    
                    if (element.managedReferenceValue == null)
                    {
                        // Remove the element if it is null
                        list.List.DeleteArrayElementAtIndex(index);
                        return;
                    }
                    
                    var label = new GUIContent(element.managedReferenceValue.GetType().Name);
                    ToolboxEditorGui.DrawToolboxProperty(element, label);
                };

                return list;
            });
        }

        private static readonly PropertyDataStorage<ReorderableListBase, EffectsCollectionDrawer> Storage;

        protected override void OnGUISafe(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);
            EditorGUI.indentLevel++;

            bool isExpanded = property.isExpanded;
            SnEditorGUI.BeginSection(label.ToString(), ref isExpanded);
            property.isExpanded = isExpanded;

            if (isExpanded)
            {
                EditorGUI.indentLevel++;

                var isApplyParallel = property.FindPropertyRelative("isApplyParallel");
                EditorGUILayout.PropertyField(isApplyParallel);
                
                var isCancelParallel = property.FindPropertyRelative("isCancelParallel");
                EditorGUILayout.PropertyField(isCancelParallel);

                var effects = property.FindPropertyRelative("effects");
                var list = Storage.ReturnItem(effects, null);
                list.DoList(new GUIContent("Effects"));

                DrawAddComponentButton(effects);

                EditorGUI.indentLevel--;
            }

            SnEditorGUI.EndSection();
            
            EditorGUI.indentLevel--;
            EditorGUI.EndProperty();
        }

        private void DrawAddComponentButton(SerializedProperty effects)
        {
            // Draw a small plus "+" button that opens a popup with all available effects
            if (GUILayout.Button("+", GUILayout.Width(20), GUILayout.Height(20)))
            {
                var menu = new GenericMenu();
                var components = SnEditorUtils.GetSubclasses<SnEffect>();
                foreach (var component in components)
                {
                    // remove effect from the name if it is the last word
                    var name = component.Name;
                    if (name.EndsWith("Effect"))
                    {
                        name = name.Substring(0, name.Length - 6);
                    }
                    
                    // make the name more readable using spaces
                    name = ObjectNames.NicifyVariableName(name);

                    menu.AddItem(new GUIContent(name), false, () =>
                    {
                        // Create a new instance of the selected effect
                        var newComponent = Activator.CreateInstance(component) as SnEffect;

                        effects.arraySize++;
                        var effect = effects.GetArrayElementAtIndex(effects.arraySize - 1);
                        effect.managedReferenceValue = newComponent;
                        effects.serializedObject.ApplyModifiedProperties();
                    });
                }

                menu.ShowAsContext();
            }
        }

        protected override float GetPropertyHeightSafe(SerializedProperty property, GUIContent label)
        {
            return 0;
        }
    }
}