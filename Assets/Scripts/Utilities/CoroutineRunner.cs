using UnityEngine;

namespace CarXTowerDefence.Utilities
{
    public sealed class CoroutineRunner : MonoBehaviour
    {
        public void Awake() =>
            DontDestroyOnLoad(this);
    }
}
