using System.Reflection;
using UnityEngine.Rendering.Universal;

namespace Snowy.Utils
{
    public static class UniversalRenderPipelineAssetExtensions
    {
        public static ScriptableRendererFeature DisableRenderFeature<T>(this UniversalRenderPipelineAsset asset) where T : ScriptableRendererFeature
        {
            var type = asset.GetType();
            var propertyInfo = type.GetField("m_RendererDataList", BindingFlags.Instance | BindingFlags.NonPublic);
 
            if (propertyInfo == null)
            {
                return null;
            }
 
            var scriptableRenderData = (ScriptableRendererData[])propertyInfo.GetValue(asset);
 
            if (scriptableRenderData != null && scriptableRenderData.Length > 0)
            {
                foreach (var renderData in scriptableRenderData)
                {
                    foreach (var rendererFeature in renderData.rendererFeatures)
                    {
                        if (rendererFeature is T)
                        {
                            rendererFeature.SetActive(false);
 
                            return rendererFeature;
                        }
                    }
                }
            }
 
            return null;
        }
    }
}