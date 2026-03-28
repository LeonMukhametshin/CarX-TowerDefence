using CarXTowerDefence.UI;
using System;
using UnityEngine;

namespace CarXTowerDefence
{
    public class MainMenuEntryPoint : MonoBehaviour
    {
        public event Action<string> GoToLevelRequested;

        [SerializeField] private MainMenuView m_mainMenuView;

        public void Run()
        {
            m_mainMenuView.OnLevelSelected += HandleLevelSelected;
            m_mainMenuView.Show();
        }

        private void OnDestroy() =>
            m_mainMenuView.OnLevelSelected -= HandleLevelSelected;

        private void HandleLevelSelected(string levelName) => 
            GoToLevelRequested?.Invoke(levelName);
    }
}