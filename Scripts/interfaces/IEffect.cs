/*
 * MIT License © 2024 Snowy (aka Snowy7x) - See LICENSE file for details.
 * X (Twitter): https://twitter.com/Snowy7x7
 * GitHub: https://github.com/Snowy7x
*/

using System.Collections;

namespace Snowy.UI.Effects
{
    public interface IEffect
    {
        bool IsPlaying { get; }
        void Initialize(IEffectsManager manager);
        IEnumerator Apply(IEffectsManager manager);
        IEnumerator Cancel(IEffectsManager manager);
        void ImmediateCancel(IEffectsManager manager); 
    }
}