using TMPro;
using UnityEngine;

namespace CarXTowerDefence.Gameplay.Monsters.Config
{
    public class StatView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI m_statText;
        [SerializeField] private string m_format;

        private Stat m_stat;

        public void Construct(Stat stat)
        {
            m_stat = stat;

            SetValue(m_stat.value);
            Subscribe();
        }

        private void Subscribe() => 
            m_stat.valueChanged += SetValue;
        
        private void OnDestroy()
        {
            if (m_stat != null)
            {
                m_stat.valueChanged -= SetValue;
                m_stat = null;
            }
        }

        private void SetValue(float value)
        {
            m_format ??= string.Empty;
            m_statText.text = string.Format(m_format, value.ToString());
        }
    }
}