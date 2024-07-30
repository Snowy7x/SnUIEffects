/*
 * MIT License © 2024 Snowy (aka Snowy7x) - See LICENSE file for details.
 * X (Twitter): https://twitter.com/Snowy7x7
 * GitHub: https://github.com/Snowy7x
*/

using System.Collections;
using UnityEngine;

namespace Snowy.UI.Effects.Effects
{
    public class MoveEffect : SnEffect
    {
        [SerializeField] private Vector3 to;
        [SerializeField] float duration = 0.1f;
        [SerializeField] private bool forceFrom;
        [SerializeField, ShowIf(nameof(forceFrom), true)] private Vector3 fFrom;
        
        private Vector3 m_originalPosition;
        
        public override void Initialize(IEffectsManager manager)
        {
            base.Initialize(manager);

            m_originalPosition = graphicTarget.transform.localPosition;
        }

        public override IEnumerator Apply(IEffectsManager manager)
        {
            IsPlaying = true;
            if (forceFrom)
                graphicTarget.transform.localPosition = fFrom;
            
            Vector3 from = graphicTarget.transform.localPosition;
            float time = 0f;
            while (time < duration)
            {
                graphicTarget.transform.localPosition = Vector3.Lerp(from, to, time / duration);
                time += Time.unscaledDeltaTime;
                yield return null;
            }
            graphicTarget.transform.localPosition = to;
            IsPlaying = false;
        }

        public override IEnumerator Cancel(IEffectsManager manager)
        {
            // go back to the original position
            IsPlaying = true;
            float time = 0f;
            var from = graphicTarget.transform.localPosition;
            float dur = duration * Vector3.Distance(from, m_originalPosition) / Vector3.Distance(to, m_originalPosition);
            while (time < dur)
            {
                graphicTarget.transform.localPosition = Vector3.Lerp(from, m_originalPosition, time / dur);
                time += Time.unscaledDeltaTime;
                yield return null;
            }
            graphicTarget.transform.localPosition = m_originalPosition;
            IsPlaying = false;
        }

        public override void ImmediateCancel(IEffectsManager manager)
        {
            graphicTarget.transform.localPosition = m_originalPosition;
        }
    }
}