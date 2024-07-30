/*
 * MIT License © 2024 Snowy (aka Snowy7x) - See LICENSE file for details.
 * X (Twitter): https://twitter.com/Snowy7x7
 * GitHub: https://github.com/Snowy7x
*/

using UnityEngine;
using UnityEngine.UI;

namespace Snowy.UI.Effects
{
    /// <summary>
    /// Used on the scripts that manage the effects.
    /// </summary>
    public interface IEffectsManager
    {
        // The main transform to be used in the effects.
        public Transform Transform { get; }
        
        // The main graphic to be used in the effects.
        public Graphic TargetGraphic { get; }
        
        // Reference to the mono behaviour to run coroutines.
        public MonoBehaviour Mono { get; }
    }
}