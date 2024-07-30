using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace Toolbox.Editor
{
    /// <summary>
    /// Default drawer responsible for drawing <see cref="UnityEditor.Editor"/>s.
    /// This helper class that can be used across many Toolbox-based editors that cannot share the same base class.
    /// </summary>
    public class ToolboxEditorDrawer : IToolboxEditorDrawer
    {
        private readonly HashSet<string> propertiesToIgnore = new HashSet<string>();
        private readonly Action<SerializedProperty> toolboxDrawingAction;
        private readonly Action<SerializedProperty> defaultDrawingAction;

        public ToolboxEditorDrawer()
            : this(ToolboxEditorGui.DrawToolboxProperty)
        { }

        public ToolboxEditorDrawer(Action<SerializedProperty> toolboxDrawingAction)
            : this(toolboxDrawingAction, ToolboxEditorGui.DrawNativeProperty)
        { }

        public ToolboxEditorDrawer(Action<SerializedProperty> toolboxDrawingAction, Action<SerializedProperty> defaultDrawingAction)
        {
            this.toolboxDrawingAction = toolboxDrawingAction;
            this.defaultDrawingAction = defaultDrawingAction;
        }

        private void DrawProperty(SerializedProperty property, Action<SerializedProperty> drawingAction)
        {
            if (!IsPropertyIgnored(property))
            {
                drawingAction(property);
            }
        }

        /// <inheritdoc />
        public void DrawEditor(SerializedObject serializedObject)
        {
            if (ToolboxDrawerModule.ToolboxDrawersAllowed)
            {
                DrawToolboxEditor(serializedObject);
            }
            else
            {
                DrawDefaultEditor(serializedObject);
            }
            
            // Handle ButtonAttribute
            var target = serializedObject.targetObject;
            var type = target.GetType();
            // Get all methods specific to the target object
            var eFlags = BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly;
            var methods = type.GetMethods(eFlags);
            foreach (var method in methods)
            {
                var attributes = method.GetCustomAttributes(typeof(ButtonAttribute), true);
                if (attributes.Length > 0)
                {
                    var attribute = attributes[0] as ButtonAttribute;
                    if (attribute == null) continue;
                    // nicify the method name using the Nicify method
                    if (GUILayout.Button(attribute.Label ?? ObjectNames.NicifyVariableName(method.Name)))
                    {
                        method.Invoke(target, null);
                    }
                }
            }
        }

        /// <inheritdoc />
        public void DrawEditor(SerializedObject serializedObject, Action<SerializedProperty> drawingAction)
        {
            serializedObject.UpdateIfRequiredOrScript();
            var property = serializedObject.GetIterator();
            if (property.NextVisible(true))
            {
                var isScript = PropertyUtility.IsDefaultScriptProperty(property);
                using (new EditorGUI.DisabledScope(isScript))
                {
                    DrawProperty(property.Copy(), drawingAction);
                }

                while (property.NextVisible(false))
                {
                    DrawProperty(property.Copy(), drawingAction);
                }
            }

            serializedObject.ApplyModifiedProperties();
        }

        /// <inheritdoc />
        public void DrawToolboxEditor(SerializedObject serializedObject)
        {
            DrawEditor(serializedObject, toolboxDrawingAction);
        }

        /// <inheritdoc />
        public void DrawDefaultEditor(SerializedObject serializedObject)
        {
            DrawEditor(serializedObject, defaultDrawingAction);
        }

        /// <inheritdoc />
        public void IgnoreProperty(SerializedProperty property)
        {
            IgnoreProperty(property.propertyPath);
        }

        /// <inheritdoc />
        public void IgnoreProperty(string propertyPath)
        {
            propertiesToIgnore.Add(propertyPath);
        }

        /// <inheritdoc />
        public bool IsPropertyIgnored(SerializedProperty property)
        {
            return IsPropertyIgnored(property.propertyPath);
        }

        /// <inheritdoc />
        public bool IsPropertyIgnored(string propertyPath)
        {
            return propertiesToIgnore.Contains(propertyPath);
        }
    }
}