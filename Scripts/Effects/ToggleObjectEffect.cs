/*
 * MIT License © 2024 Snowy (aka Snowy7x) - See LICENSE file for details.
 * X (Twitter): https://twitter.com/Snowy7x7
 * GitHub: https://github.com/Snowy7x
*/

using System.Collections;
using UnityEngine;

namespace Snowy.UI.Effects.Effects
{
    public class ToggleObjectEffect : SnEffect
    {
        [SerializeField] private GameObject m_gameObject;
        [SerializeField] private bool to;
        
        private bool m_originalState;
        
        public override void Initialize(IEffectsManager manager)
        {
            base.Initialize(manager);

            m_originalState = m_gameObject.activeSelf;
        }
        
        public override IEnumerator Apply(IEffectsManager manager)
        {
            IsPlaying = true;
            m_gameObject.SetActive(to);
            IsPlaying = false;
            
            yield return null;
        }

        public override IEnumerator Cancel(IEffectsManager manager)
        {
            ImmediateCancel(manager);
            
            yield return null;
        }

        public override void ImmediateCancel(IEffectsManager manager)
        {
            m_gameObject.SetActive(m_originalState);
        }
    }
}