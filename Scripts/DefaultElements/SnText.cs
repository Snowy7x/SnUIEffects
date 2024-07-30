/*
 * MIT License © 2024 Snowy (aka Snowy7x) - See LICENSE file for details.
 * X (Twitter): https://twitter.com/Snowy7x7
 * GitHub: https://github.com/Snowy7x
*/

// A TEMP SCRIPT FOR TESTING PURPOSES

using Snowy.UI.Effects;
using TMPro;
using UnityEngine;

namespace Snowy.UI
{
    public class SnText : TMP_Text, IAudioInteraction
    {
        public AudioClip HoverSound { get; set; }
        public AudioClip ClickSound { get; set; }
    }
}