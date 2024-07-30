using UnityEditor;

namespace Snowy.Utils
{
    public static class SnEditorUtils
    {
        // Get all scene names in the specified path
        public static string[] GetSceneNames(string path)
        {
            // Get all the scenes in the specified path
            string[] guids = AssetDatabase.FindAssets("t:Scene", new[] { path });
            string[] sceneNames = new string[guids.Length];
            
            for (int i = 0; i < guids.Length; i++)
            {
                string assetPath = AssetDatabase.GUIDToAssetPath(guids[i]);
                sceneNames[i] = assetPath;
            }
            
            // Sort the scene names
            System.Array.Sort(sceneNames);
            return sceneNames;
        }
        
        // Get all subclasses of the specified type
        public static System.Type[] GetSubclasses<T>()
        {
            System.Type parentType = typeof(T);
            System.Type[] types = System.Reflection.Assembly.GetAssembly(parentType).GetTypes();
            System.Collections.Generic.List<System.Type> subclasses = new System.Collections.Generic.List<System.Type>();
            
            foreach (System.Type type in types)
            {
                if (type.IsSubclassOf(parentType) && !type.IsAbstract)
                {
                    subclasses.Add(type);
                }
            }
            
            return subclasses.ToArray();
        }
    }
}