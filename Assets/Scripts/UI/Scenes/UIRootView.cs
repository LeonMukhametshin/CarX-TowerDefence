using UnityEngine;

namespace CarXTowerDefence.UI
{
    public class UIRootView : MonoBehaviour
    {
        [SerializeField] private GameObject m_loadingScreen;

        public void ShowLoadingScreen() => 
            m_loadingScreen.SetActive(true);

        public void HideLoadingScreen() => 
            m_loadingScreen.SetActive(false);
    }
}