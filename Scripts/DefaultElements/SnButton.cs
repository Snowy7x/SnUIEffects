/*
 * MIT License © 2024 Snowy (aka Snowy7x) - See LICENSE file for details.
 * X (Twitter): https://twitter.com/Snowy7x7
 * GitHub: https://github.com/Snowy7x
*/

using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Snowy.UI
{
    [Serializable] public class SnButtonEvent : UnityEvent { }
    
    [RequireComponent(typeof(AudioSource))]
    public class SnButton : SnSelectable, IPointerClickHandler, ISubmitHandler
    {
        [Title("Button Settings")]
        [Space]
        [BeginGroup]
        [SerializeField] private bool playAudio = true;
        [SerializeField] private TMP_Text buttonText;
        [SerializeField, InLineEditor] ButtonAudioPreset audioPreset;
        [EndGroup]
        [SerializeField] private SnButtonEvent onClick = new SnButtonEvent();
    
        
        private AudioSource audioSource;
        
        public new SnButtonEvent OnClick
        {
            get => onClick;
            set => onClick = value;
        }

        protected override void Awake()
        {
            base.Awake();

            OnHover += OnHoverAudio;
            base.OnClick += OnClickAudio;

            audioSource = GetComponent<AudioSource>();
            if (audioPreset && !audioSource)
            {
                Debug.LogWarning("No audio source assigned to the button, using the default one.");
                audioSource = gameObject.AddComponent<AudioSource>();
            }
            
            if (audioSource)
            {
                audioSource.playOnAwake = false;
                audioSource.loop = false;
            }
        }
        
        private void Press()
        {
            if (!IsActive() || !IsInteractable())
                return;

            UISystemProfilerApi.AddMarker("Button.onClick", this);
            onClick.Invoke();
        }
        
        public virtual void OnPointerClick(PointerEventData eventData)
        {
            if (eventData.button != PointerEventData.InputButton.Left)
                return;

            Press();
        }

        public void OnSubmit(BaseEventData eventData)
        {
            Press();
        }
        
        private void OnHoverAudio()
        {
            if (playAudio)
            {
                if (audioSource && audioPreset)
                    audioSource.PlayOneShot(audioPreset.hover);
            }
        }
        
        private void OnClickAudio()
        {
            if (playAudio)
            {
                if (audioSource && audioPreset)
                {
                    if (IsInteractable()) audioSource.PlayOneShot(audioPreset.click);
                    else audioSource.PlayOneShot(audioPreset.disabled);
                }
            }
        }
        
        public void SetText(string text)
        {
            if (buttonText) buttonText.text = text;
        }
        
        # if UNITY_EDITOR
        protected override void OnValidate()
        {
            base.OnValidate();
            
            if (!buttonText)
            {
                buttonText = GetComponentInChildren<TMP_Text>();
            }
        }
        # endif
    }
}