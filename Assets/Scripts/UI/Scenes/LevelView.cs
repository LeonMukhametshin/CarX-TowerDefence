using System;
using UnityEngine;
using UnityEngine.UI;

namespace CarXTowerDefence.UI
{
    public class LevelView : MonoBehaviour
    {
        public event Action onBackButtonClicked;

        [SerializeField] private Button m_backButton;
        [SerializeField] private GameObject m_levelPanel;

        public void Show() => m_levelPanel.SetActive(true);
        public void Hide() => m_levelPanel.SetActive(false);

        private void Awake()
        {
            m_backButton.onClick.AddListener(() => onBackButtonClicked?.Invoke());
        }

        private void OnDestroy()
        {
            m_backButton.onClick.RemoveAllListeners();
        }
    }
}
