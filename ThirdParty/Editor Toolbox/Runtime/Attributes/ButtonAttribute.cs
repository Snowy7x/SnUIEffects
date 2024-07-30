using System;

namespace UnityEngine
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class ButtonAttribute : Attribute
    {
        public ButtonAttribute(string label = null, string tooltip = null)
        {
            Label = label;
            Tooltip = tooltip;
        }
        
        public string Label { get; }
        
        public string Tooltip { get; set; }
    }
}