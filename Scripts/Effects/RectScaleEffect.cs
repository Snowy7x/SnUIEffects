/*
 * MIT License © 2024 Snowy (aka Snowy7x) - See LICENSE file for details.
 * X (Twitter): https://twitter.com/Snowy7x7
 * GitHub: https://github.com/Snowy7x
*/

using System.Collections;
using UnityEngine;

namespace Snowy.UI.Effects.Effects
{
    public class RectScaleEffect : SnEffect
    {
        [SerializeField] private bool changeHeight;
        [SerializeField, ShowIf(nameof(changeHeight), true)] private float toHeight;
        [SerializeField] private bool changeWidth;
        [SerializeField, ShowIf(nameof(changeWidth), true)] private float toWidth;
        [SerializeField] float duration = 0.1f;
        [SerializeField] private bool forceFrom;
        [SerializeField, ShowIf(nameof(forceFrom), true)] private float fHeight;
        [SerializeField, ShowIf(nameof(forceFrom), true)] private float fWidth;
        
        private float m_originalHeight;
        private float m_originalWidth;
        
        public override void Initialize(IEffectsManager manager)
        {
            base.Initialize(manager);
            
            m_originalHeight = graphicTarget.rectTransform.sizeDelta.y;
            m_originalWidth = graphicTarget.rectTransform.sizeDelta.x;

            if (!changeWidth)
            {
                toWidth = m_originalWidth;
                fWidth = m_originalWidth;
            }

            if (!changeHeight)
            {
                toHeight = m_originalHeight;
                fHeight = m_originalHeight;
            }
        }

        public override IEnumerator Apply(IEffectsManager manager)
        {
            IsPlaying = true;
            if (forceFrom)
            {
                graphicTarget.rectTransform.sizeDelta = new Vector2(fWidth, fHeight);
            }
            
            float time = 0f;
            Vector2 from = graphicTarget.rectTransform.sizeDelta;
            while (time < duration)
            {
                graphicTarget.rectTransform.sizeDelta = new Vector2(Mathf.Lerp(from.x, toWidth, time / duration), Mathf.Lerp(from.y, toHeight, time / duration));
                
                time += Time.unscaledDeltaTime;
                yield return null;
            }
            
            graphicTarget.rectTransform.sizeDelta = new Vector2(toWidth, toHeight);
            IsPlaying = false;
        }

        public override IEnumerator Cancel(IEffectsManager manager)
        {
            // go back to the original localScale
            IsPlaying = true;
            float time = 0f;
            var from = graphicTarget.rectTransform.sizeDelta;
            float dur = duration * Vector3.Distance(from, new Vector2(toWidth, toHeight)) / Vector3.Distance(new Vector2(toWidth, toHeight), new Vector2(m_originalWidth, m_originalHeight));
            while (time < dur)
            {
                graphicTarget.rectTransform.sizeDelta = new Vector2(Mathf.Lerp(toWidth, m_originalWidth, time / dur), Mathf.Lerp(toHeight, m_originalHeight, time / dur));
                time += Time.unscaledDeltaTime;
                yield return null;
            }
            graphicTarget.rectTransform.sizeDelta = new Vector2(m_originalWidth, m_originalHeight);
            IsPlaying = false;
        }

        public override void ImmediateCancel(IEffectsManager manager)
        {
            graphicTarget.rectTransform.sizeDelta = new Vector2(m_originalWidth, m_originalHeight);
        }
    }
}