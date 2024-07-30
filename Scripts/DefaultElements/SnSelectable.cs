/*
 * MIT License © 2024 Snowy (aka Snowy7x) - See LICENSE file for details.
 * X (Twitter): https://twitter.com/Snowy7x7
 * GitHub: https://github.com/Snowy7x
*/

using System;
using Snowy.UI.Effects;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Snowy.UI
{
    public class SnSelectable : UIBehaviour,
        IMoveHandler,
        IPointerDownHandler, IPointerUpHandler,
        IPointerEnterHandler, IPointerExitHandler,
        ISelectHandler, IDeselectHandler, IEffectsManager
    {
        [Title("Effects Settings")]
        public EffectsCollection onHoverEffects = new(true);
        public EffectsCollection onClickEffects = new(true);
        public EffectsCollection onSelectEffects = new(true);
        private bool m_isHovered;
        private bool m_isSelected;
        private bool m_isPressed;
        private bool m_interactable = true;
        private bool m_enableEffects = true;
        
        // OnHover, OnClick
        public event Action OnClick;
        public event Action OnHover;
        
        public Graphic TargetGraphic => m_targetGraphic;
        public Transform Transform => transform;
        public MonoBehaviour Mono => this;
        
        public bool Interactable
        {
            get => m_interactable;
            set
            {
                if (m_targetGraphic)
                {
                    m_targetGraphic.raycastTarget = value;
                }
                m_interactable = value;
            }
        }
        
        public bool EnableEffects
        {
            get => m_enableEffects;
            set => m_enableEffects = value;
        }
        
        private Graphic m_targetGraphic;
        
        protected override void Awake()
        {
            m_targetGraphic = GetComponentInChildren<Graphic>();
            
            if (onHoverEffects == null) onHoverEffects = new EffectsCollection(true);
            if (onClickEffects == null) onClickEffects = new EffectsCollection(true);
            if (onSelectEffects == null) onSelectEffects = new EffectsCollection(true);
            
            onHoverEffects.Initialize(this);
            onClickEffects.Initialize(this);
            onSelectEffects.Initialize(this);
            
            base.Awake();
        }
        
        # if UNITY_EDITOR
        protected override void OnValidate()
        {
            base.OnValidate();
            if (onHoverEffects == null) onHoverEffects = new EffectsCollection(true);
            if (onClickEffects == null) onClickEffects = new EffectsCollection(true);
            if (onSelectEffects == null) onSelectEffects = new EffectsCollection(true);
            
            if (m_targetGraphic == null) m_targetGraphic = GetComponentInChildren<Graphic>();
        }
        # endif
        
        public void OnMove(AxisEventData eventData)
        {
            // TODO: Implement
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            m_isPressed = true;
            if (!m_enableEffects) return;
            StopAllCoroutines();
            
            // Cancel the hover effect
            onHoverEffects.ImmediateCancel(this);
            
            StartCoroutine(onClickEffects.Apply(this));
            
            if (EventSystem.current.currentSelectedGameObject != gameObject)
            {
                EventSystem.current.SetSelectedGameObject(gameObject);
            }
        }

        protected override void OnDisable()
        {
            m_isHovered = false;
            m_isSelected = false;
            m_isPressed = false;
            
            // Disable all effects
            StopAllCoroutines();
            onHoverEffects.ImmediateCancel(this);
            onClickEffects.ImmediateCancel(this);
            onSelectEffects.ImmediateCancel(this);
            base.OnDisable();
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            OnClick?.Invoke();

            m_isPressed = false;
            if (!m_enableEffects) return;
            if (!m_isHovered && m_isSelected)
            {
                StopAllCoroutines();
                onClickEffects.ImmediateCancel(this);
                StartCoroutine(onSelectEffects.Apply(this));
            }
            else if (m_isHovered)
            {
                StopAllCoroutines();
                onClickEffects.ImmediateCancel(this);
                StartCoroutine(onHoverEffects.Apply(this));
            }
            else
            {
                StopAllCoroutines();
                StartCoroutine(onClickEffects.Cancel(this));
            }
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            m_isHovered = true;
            if (!m_enableEffects) return;
            if (!m_isPressed)
            {
                StopAllCoroutines();
                StartCoroutine(onHoverEffects.Apply(this));
                OnHover?.Invoke();
            }
        }
 
        public void OnPointerExit(PointerEventData eventData)
        {
            m_isHovered = false;
            if (!m_enableEffects) return;
            if (!m_isPressed && !m_isSelected)
            {
                StopAllCoroutines();
                StartCoroutine(onHoverEffects.Cancel(this));
            } else if (m_isSelected)
            {
                StopAllCoroutines();
                StartCoroutine(onHoverEffects.Cancel(this));
                StartCoroutine(onSelectEffects.Apply(this));
            } else if (m_isPressed)
            {
                StopAllCoroutines();
                StartCoroutine(onHoverEffects.Cancel(this));
                StartCoroutine(onClickEffects.Apply(this));
            }
        }

        public void OnSelect(BaseEventData eventData)
        {
            m_isSelected = true;
            if (!m_enableEffects) return;
            if (!m_isPressed)
            {
                StopAllCoroutines();
                StartCoroutine(onSelectEffects.Apply(this));
            }
        }

        public void OnDeselect(BaseEventData eventData)
        {
            m_isSelected = false;
            if (!m_enableEffects) return;
            if (!m_isPressed && m_isHovered)
            {
                StopAllCoroutines();
                StartCoroutine(onHoverEffects.Apply(this));
            }
            else if (m_isPressed)
            {
                StopAllCoroutines();
                StartCoroutine(onClickEffects.Apply(this));
            }
            else
            {
                StopAllCoroutines();
                StartCoroutine(onSelectEffects.Cancel(this));
            }
        }
        
        public virtual bool IsInteractable()
        {
            return m_interactable;
        }
    }
}