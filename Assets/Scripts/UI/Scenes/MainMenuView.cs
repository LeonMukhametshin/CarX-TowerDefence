using CarXTowerDefence.Infrastructure;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace CarXTowerDefence.UI
{
    public class MainMenuView : MonoBehaviour
    {
        public event Action<string> OnLevelSelected;

        [SerializeField] private Button m_ballisticButton;
        [SerializeField] private Button m_linearButton;
        [SerializeField] private Button m_quitButton;

        [SerializeField] private GameObject m_mainMenuPanel;

        public void Show() => m_mainMenuPanel.SetActive(true);
        public void Hide() => m_mainMenuPanel.SetActive(false);

        private void Awake()
        {
            m_ballisticButton.onClick.AddListener(() => OnLevelSelected?.Invoke(SceneNames.LEVEL_BALLISTIC));
            m_linearButton.onClick.AddListener(() => OnLevelSelected?.Invoke(SceneNames.LEVEL_LINEAR));
            m_quitButton.onClick.AddListener(QuitGame);
        }

        private void OnDestroy()
        {
            m_ballisticButton.onClick.RemoveAllListeners();
            m_linearButton.onClick.RemoveAllListeners();
            m_quitButton.onClick.RemoveAllListeners();
        }

        private void QuitGame()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }
    }
}