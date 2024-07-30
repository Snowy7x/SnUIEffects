/*
 * MIT License © 2024 Snowy (aka Snowy7x) - See LICENSE file for details.
 * X (Twitter): https://twitter.com/Snowy7x7
 * GitHub: https://github.com/Snowy7x
*/

// A TEMP SCRIPT FOR TESTING PURPOSES

using System;
using UnityEngine;

namespace Snowy.UI
{
    [Serializable]
    public class Tab
    {
        public SnButton actionButton;
        public GameObject content;
        public GameObject activeIndicator;
    }
    
    public class Tabs : MonoBehaviour
    {
        [SerializeField] private Tab[] tabs;
        private int m_index;
        
        private void Start()
        {
            for (var i = 0; i < tabs.Length; i++)
            {
                var index = i;
                tabs[i].actionButton.OnClick.AddListener(() => SetIndex(index));
            }
            SetIndex(0);
        }
        
        public void SetIndex(int index)
        {
            if (index < 0 || index >= tabs.Length)
            {
                return;
            }
            m_index = index;
            for (var i = 0; i < tabs.Length; i++)
            {
                tabs[i].content.SetActive(i == m_index);
                tabs[i].activeIndicator.SetActive(i == m_index);
            }
        }
    }
}