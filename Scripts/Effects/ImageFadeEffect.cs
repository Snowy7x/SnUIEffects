using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Snowy.UI.Effects.Effects
{
    [Serializable]
    public class ImageFadeEffect : SnEffect
    {
        [SerializeField] float to = 1f;
        [SerializeField] float duration = 0.1f;
        [SerializeField] bool forceFrom;
        [SerializeField, ShowIf(nameof(forceFrom), true)] float fFrom;
        
        Image m_image;
        
        public override void Initialize(IEffectsManager manager)
        {
            var image = customGraphicTarget && graphicTarget ? graphicTarget : manager.TargetGraphic;
            if (image == null) return;
            if (image is Image img)
            {
                m_image = img;
                
                m_image.type = Image.Type.Filled;
                fFrom = m_image.fillAmount;
            }
            
            base.Initialize(manager);
        }
        
        public override IEnumerator Apply(IEffectsManager manager)
        {
            IsPlaying = true;
            if (forceFrom)
                m_image.fillAmount = fFrom;
            
            float time = 0f;
            while (time < duration)
            {
                m_image.fillAmount = Mathf.Lerp(fFrom, to, time / duration);
                time += Time.unscaledDeltaTime;
                yield return null;
            }
            m_image.fillAmount = to;
            IsPlaying = false;
        }
        
        public override IEnumerator Cancel(IEffectsManager manager)
        {
            IsPlaying = true;
            float time = 0f;
            var from = m_image.fillAmount;
            
            // Calculate the new duration based on the ration between the current fill amount and the target fill amount
            var dur = duration * Mathf.Abs((from - fFrom) / (to - fFrom));
            
            while (time < dur)
            {
                m_image.fillAmount = Mathf.Lerp(from, fFrom, time / dur);
                time += Time.unscaledDeltaTime;
                yield return null;
            }
            m_image.fillAmount = fFrom;
            IsPlaying = false;
        }
        
        public override void ImmediateCancel(IEffectsManager manager)
        {
            m_image.fillAmount = fFrom;
            IsPlaying = false;
        }
    }
}