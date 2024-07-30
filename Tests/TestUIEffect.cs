using Snowy.UI.Effects;
using UnityEngine;
using UnityEngine.UI;

namespace Snowy.UI.Test
{
    public class TestUIEffect : MonoBehaviour, IEffectsManager
    {
        [SerializeField] private Graphic graphicTarget;
        public EffectsCollection onHover;
        
        
        public Graphic TargetGraphic => graphicTarget;
        public Transform Transform => graphicTarget.transform;
        public CanvasGroup CanvasGroup => graphicTarget.GetComponent<CanvasGroup>();
        public MonoBehaviour Mono => this;
        
        void Start()
        {
            onHover.Initialize(this);
        }
        
        [Button]
        public void Apply()
        {
            StartCoroutine(onHover.Apply(this));
        }
        
        [Button]
        public void Cancel()
        {
            StartCoroutine(onHover.Cancel(this));
        }
        
        [Button]
        public void ImmediateCancel()
        {
            onHover.ImmediateCancel(this);
        }
    }
}