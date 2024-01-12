using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Matthias.Utilities
{
    public class TabButtonGroup : MonoBehaviour
    {
        private List<TabButton> _tabButtons = new List<TabButton>();

        public void SelectTab(TabButton selectedTabButton)
        {
            if (!_tabButtons.Contains(selectedTabButton))
                return;
            
            foreach (TabButton tabButton in _tabButtons)
            {
                tabButton.Hide();
            }
            
            selectedTabButton.Show();
        }

        public void RegisterTabButton(TabButton tabButton)
        {
            if (!_tabButtons.Contains(tabButton))
                _tabButtons.Add(tabButton);
        }
        
        public void UnregisterTabButton(TabButton tabButton)
        {
            if (_tabButtons.Contains(tabButton))
                _tabButtons.Remove(tabButton);
        }
    }
}
