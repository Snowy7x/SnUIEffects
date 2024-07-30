/*
 * MIT License © 2024 Snowy (aka Snowy7x) - See LICENSE file for details.
 * X (Twitter): https://twitter.com/Snowy7x7
 * GitHub: https://github.com/Snowy7x
*/

using UnityEngine;

namespace Snowy.UI
{
    [CreateAssetMenu(fileName = "ButtonAudioPreset", menuName = "Snowy/UI/Button Audio Preset")]
    public class ButtonAudioPreset : ScriptableObject
    {
        public AudioClip hover;
        public AudioClip click;
        public AudioClip disabled;
    }
}