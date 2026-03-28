using CarXTowerDefence.Gameplay.Bootstrap;
using CarXTowerDefence.UI;
using System;
using UnityEngine;

namespace CarXTowerDefence
{
    public class LevelEntryPoint : MonoBehaviour
    {
        public event Action goToMainMenuRequested;

        [SerializeField] private LevelView m_levelView;
        [SerializeField] private GameBootstrapper m_gameBootstrapper;

        public void Run()
        {
            m_levelView.onBackButtonClicked += HandleBackClicked;
            m_levelView.Show();
            m_gameBootstrapper.Initialize();
        }

        private void OnDestroy() =>
            m_levelView.onBackButtonClicked -= HandleBackClicked;

        private void HandleBackClicked() => 
            goToMainMenuRequested?.Invoke();
    }
}
