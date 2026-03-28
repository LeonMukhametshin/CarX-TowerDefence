using CarXTowerDefence.UI;
using CarXTowerDefence.Utilities;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CarXTowerDefence.Infrastructure
{
    public class GameEntryPoint
    {
        private static GameEntryPoint m_instance;
        private CoroutineRunner m_coroutines;
        private UIRootView m_uiRoot;
        private bool m_isLoading;

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        public static void AutostartGame()
        {
            Application.targetFrameRate = 60;
            m_instance = new GameEntryPoint();
            m_instance.RunGame();
        }

        private GameEntryPoint()
        {
            m_coroutines = new GameObject("[COROUTINES]").AddComponent<CoroutineRunner>();
            GameObject.DontDestroyOnLoad(m_coroutines.gameObject);

            var prefabUIRoot = Resources.Load<UIRootView>("UIRoot");
            if (prefabUIRoot != null)
            {
                m_uiRoot = GameObject.Instantiate(prefabUIRoot);
                GameObject.DontDestroyOnLoad(m_uiRoot.gameObject);
            }
        }

        private void RunGame()
        {
#if UNITY_EDITOR
            string sceneName = SceneManager.GetActiveScene().name;
            if (sceneName == SceneNames.LEVEL_BALLISTIC)
            {
                m_coroutines.StartCoroutine(LoadAndStartLevel(SceneNames.LEVEL_BALLISTIC));
                return;
            }
            if (sceneName == SceneNames.LEVEL_LINEAR)
            {
                m_coroutines.StartCoroutine(LoadAndStartLevel(SceneNames.LEVEL_LINEAR));
                return;
            }
#endif
            m_coroutines.StartCoroutine(LoadAndStartMainMenu());
        }

        private IEnumerator LoadAndStartLevel(string levelName)
        {
            if (m_isLoading) yield break;
            m_isLoading = true;

            if (m_uiRoot != null) m_uiRoot.ShowLoadingScreen();

            yield return LoadScene(levelName);

            var levelEntryPoint = GameObject.FindFirstObjectByType<LevelEntryPoint>();
            if (levelEntryPoint == null)
            {
                if (m_uiRoot != null) m_uiRoot.HideLoadingScreen();
                m_isLoading = false;
                yield break;
            }

            yield return new WaitForSeconds(2f);

            levelEntryPoint.goToMainMenuRequested += () =>
            {
                m_coroutines.StartCoroutine(LoadAndStartMainMenu());
            };

            levelEntryPoint.Run();

            if (m_uiRoot != null) m_uiRoot.HideLoadingScreen();
            m_isLoading = false;
        }

        private IEnumerator LoadAndStartMainMenu()
        {
            if (m_isLoading) yield break;
            m_isLoading = true;

            if (m_uiRoot != null) m_uiRoot.ShowLoadingScreen();

            yield return LoadScene(SceneNames.MAIN_MENU);

            var mainMenuEntryPoint = GameObject.FindFirstObjectByType<MainMenuEntryPoint>();
            if (mainMenuEntryPoint == null)
            {
                Debug.LogError("MainMenuEntryPoint not found");
                if (m_uiRoot != null) m_uiRoot.HideLoadingScreen();
                m_isLoading = false;
                yield break;
            }

            mainMenuEntryPoint.Run();
            mainMenuEntryPoint.GoToLevelRequested += (levelName) =>
            {
                m_coroutines.StartCoroutine(LoadAndStartLevel(levelName));
            };

            if (m_uiRoot != null) m_uiRoot.HideLoadingScreen();
            m_isLoading = false;
        }

        private IEnumerator LoadScene(string sceneName)
        {
            yield return SceneManager.LoadSceneAsync(sceneName);
        }
    }
}