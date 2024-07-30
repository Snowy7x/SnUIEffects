/*
 * MIT License © 2024 Snowy (aka Snowy7x) - See LICENSE file for details.
 * X (Twitter): https://twitter.com/Snowy7x7
 * GitHub: https://github.com/Snowy7x
*/

using System.Collections;
using UnityEngine;

namespace Snowy.UI.Effects.Effects
{
    public class ScaleEffect : SnEffect
    {
        [SerializeField] private Vector3 to;
        [SerializeField] float duration = 0.1f;
        [SerializeField] private bool forceFrom;
        [SerializeField, ShowIf(nameof(forceFrom), true)] private Vector3 fFrom;
        
        private Vector3 m_originalScale;
        
        public override void Initialize(IEffectsManager manager)
        {
            base.Initialize(manager);

            m_originalScale = graphicTarget.transform.localScale;
        }

        public override IEnumerator Apply(IEffectsManager manager)
        {
            IsPlaying = true;
            if (forceFrom)
                graphicTarget.transform.localScale = fFrom;
            
            Vector3 from = graphicTarget.transform.localScale;
            float time = 0f;
            while (time < duration)
            {
                graphicTarget.transform.localScale = Vector3.Lerp(from, to, time / duration);
                time += Time.unscaledDeltaTime;
                yield return null;
            }
            graphicTarget.transform.localScale = to;
            IsPlaying = false;
        }

        public override IEnumerator Cancel(IEffectsManager manager)
        {
            // go back to the original localScale
            IsPlaying = true;
            float time = 0f;
            var from = graphicTarget.transform.localScale;
            float dur = duration * Vector3.Distance(from, m_originalScale) / Vector3.Distance(to, m_originalScale);
            while (time < dur)
            {
                graphicTarget.transform.localScale = Vector3.Lerp(from, m_originalScale, time / dur);
                time += Time.unscaledDeltaTime;
                yield return null;
            }
            graphicTarget.transform.localScale = m_originalScale;
            IsPlaying = false;
        }

        public override void ImmediateCancel(IEffectsManager manager)
        {
            graphicTarget.transform.localScale = m_originalScale;
        }
    }
}