using UnityEngine;

namespace CarXTowerDefence.Gameplay.Monsters
{
    [CreateAssetMenu(fileName = "Monster Config", menuName = "ScriptableObject/Monster")]
    public sealed class MonsterConfig : ScriptableObject
    {
        [field: SerializeField] public GameObject monsterPrefab { get; private set; }
        [field: SerializeField] public MonsterStatsData baseStats { get; private set; }
    }
}