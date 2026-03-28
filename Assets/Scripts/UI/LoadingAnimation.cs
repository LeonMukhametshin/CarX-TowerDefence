using System.Collections;
using TMPro;
using UnityEngine;

namespace CarXTowerDefence.UI
{
    public class LoadingAnimation : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI m_loadingText;
        [SerializeField][Min(0)] private float m_animationSpeed;

        private string[] m_animationFrames = { "Загрузка....", "Загрузка...", "Загрузка..", "Загрузка." };
        private Coroutine m_animationCoroutine;

        private void OnEnable() => 
            StartAnimation();

        private void OnDisable() => 
            StopAnimation();

        public void StartAnimation()
        {
            if (m_animationCoroutine != null)
            {
                StopCoroutine(m_animationCoroutine);
            }
            m_animationCoroutine = StartCoroutine(Animate());
        }

        public void StopAnimation()
        {
            if (m_animationCoroutine != null)
            {
                StopCoroutine(m_animationCoroutine);
                m_animationCoroutine = null;
            }
        }

        private IEnumerator Animate()
        {
            int index = 0;
            int direction = 1;

            while (true)
            {
                m_loadingText.text = m_animationFrames[index];

                yield return new WaitForSeconds(m_animationSpeed);

                index += direction;

                if (index >= m_animationFrames.Length)
                {
                    index = 0;
                }
            }
        }
    }
}