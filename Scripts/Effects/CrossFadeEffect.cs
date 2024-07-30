/*
 * MIT License © 2024 Snowy (aka Snowy7x) - See LICENSE file for details.
 * X (Twitter): https://twitter.com/Snowy7x7
 * GitHub: https://github.com/Snowy7x
*/

using System.Collections;
using UnityEngine;

namespace Snowy.UI.Effects.Effects
{
    public class CrossFadeEffect : SnEffect
    {
        [SerializeField, Range(0, 1)] float to = 1f;
        [SerializeField] float duration = 0.1f;
        [SerializeField] bool forceFrom;
        [SerializeField, ShowIf(nameof(forceFrom), true), Range(0, 1)] float fFrom;
        
        private float m_originalAlpha;
        
        public override void Initialize(IEffectsManager manager)
        {
            base.Initialize(manager);

            m_originalAlpha = 1;
        }

        public override IEnumerator Apply(IEffectsManager manager)
        {
            // crossfade
            IsPlaying = true;
            if (forceFrom)
                graphicTarget.CrossFadeAlpha(fFrom, 0, true);
            yield return new WaitForSeconds(0.05f);
            graphicTarget.CrossFadeAlpha(to, duration, true);
            yield return new WaitForSeconds(duration);
        }

        public override IEnumerator Cancel(IEffectsManager manager)
        {
            // crossfade
            IsPlaying = true;
            graphicTarget.CrossFadeAlpha(m_originalAlpha, duration, true);
            yield return new WaitForSeconds(duration);
        }

        public override void ImmediateCancel(IEffectsManager manager)
        {
            graphicTarget.CrossFadeAlpha(m_originalAlpha, 0, true);
        }
    }
}