using Snowy.Utils.Attributes;
using Toolbox.Editor.Drawers;
using UnityEditor;

namespace Utils.Attributes.Editor
{
    public class ReadOnlyDrawer : ToolboxConditionDrawer<ReadOnlyAttribute>
    {
        protected override PropertyCondition OnGuiValidateSafe(SerializedProperty property, ReadOnlyAttribute attribute)
        {
            return PropertyCondition.Disabled;
        }
    }
}