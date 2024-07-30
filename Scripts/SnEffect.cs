/*
 * MIT License © 2024 Snowy (aka Snowy7x) - See LICENSE file for details.
 * X (Twitter): https://twitter.com/Snowy7x7
 * GitHub: https://github.com/Snowy7x
*/

using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Snowy.UI.Effects
{
    [Serializable] public abstract class SnEffect : IEffect
    {
        // Whether the effect has a custom graphic target.
        public bool customGraphicTarget;
        [ShowIf(nameof(customGraphicTarget), true)] public Graphic graphicTarget;
        
        public bool IsPlaying { get; protected set; }

        public virtual void Initialize(IEffectsManager manager)
        {
            // Define the graphic target as custom or default.
            if (customGraphicTarget && graphicTarget == null)
            {
                Debug.LogWarning("No graphic target assigned to the effect, using the manager's target graphic instead.");
                graphicTarget = manager.TargetGraphic;
            } else if (!customGraphicTarget)
            {
                graphicTarget = manager.TargetGraphic;
            }
        }
        
        /// <summary>
        /// Applies the effect over the specified duration.
        /// </summary>
        /// <param name="manager"></param>
        /// <returns></returns>
        public abstract IEnumerator Apply(IEffectsManager manager);
        
        /// <summary>
        /// Cancel the effect over the specified duration.
        /// </summary>
        /// <param name="manager"></param>
        /// <returns></returns>
        public abstract IEnumerator Cancel(IEffectsManager manager);

        /// <summary>
        /// Cancels the effect immediately.
        /// </summary>
        /// <param name="manager"></param>
        public abstract void ImmediateCancel(IEffectsManager manager);
        
        
    }
}