using UnityEngine;

namespace Snowy.Utils
{
    public static class SnGUISkin
    {
        // Create a box style with border radius with white outline
# if UNITY_2019_3_OR_NEWER
        public static GUIStyle boxStyle = new GUIStyle("helpBox");
# else
        public static GUIStyle boxStyle = new GUIStyle("box");
# endif
            
        // Create a title style
        public static GUIStyle titleStyle = new GUIStyle
        {
            alignment = TextAnchor.MiddleCenter,
            fontSize = 16,
            fontStyle = FontStyle.Bold,
            normal = {textColor = Color.white},
            border = new RectOffset(4, 4, 4, 4),
        };
            
        // Create a section title style
        public static GUIStyle sectionTitleStyle = new GUIStyle
        {
            alignment = TextAnchor.MiddleCenter,
            fontSize = 14,
            fontStyle = FontStyle.Bold,
            normal = {textColor = Color.white},
            border = new RectOffset(4, 4, 4, 4),
        };

        // inherit from foldout style
        public static GUIStyle foldoutTitleStyle = new GUIStyle("foldout")
        {
            fontStyle = FontStyle.Bold,
            fontSize = 14,
            alignment = TextAnchor.MiddleLeft,
            richText = true,
        }; 
    }
}