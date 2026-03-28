using CarXTowerDefence.Gameplay.Effects;
using System.Collections.Generic;
using UnityEngine;

namespace CarXTowerDefence.Gameplay.Projectiles.Configs
{
    public abstract class ProjectileConfig : ScriptableObject
    {
        [field: SerializeField] public GameObject view { get; private set; }
        [field: SerializeField][field: Min(1f)] public float speed { get; private set; } = 10f;

        [SerializeReferenceDropdown][SerializeReference] private IEffect[] m_effects;

        public IReadOnlyList<IEffect> effects => m_effects;
    }
}