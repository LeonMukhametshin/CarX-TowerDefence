using CarXTowerDefence.Gameplay.Data;
using CarXTowerDefence.Gameplay.Projectiles.Configs;
using CarXTowerDefence.Gameplay.Towers.Aiming;
using CarXTowerDefence.Gameplay.Towers.Data;
using System;
using UnityEngine;
using CarXTowerDefence.Gameplay.Towers.Shooting;
using CarXTowerDefence.Gameplay.Towers.AimStrategy;

namespace CarXTowerDefence.Gameplay.Towers
{
    public sealed class TowerFactory
    {
        private readonly Transform m_container;

        public TowerFactory(Transform container = null)
        {
            m_container = container;
        }

        public TowerController Create(TowerDataSO config, Vector3 position, Quaternion rotation)
        {
            if (config is null)
            {
                throw new NullReferenceException($"{config} is null");
            }

            var prefab = CreateTower(config);

            var towerInstance = GameObject.Instantiate(prefab, position, rotation, m_container);

            var targeting = CreateTargeting(config);
            var aiming = CreateAimer(config);
            var shooter = CreateShooter(config);
            var aimStratage = SelectAimStrategy(config);

            var towerConfig = BuildConfig(config);

            towerInstance.Initialize(towerConfig, targeting, aiming, shooter, aimStratage);

            return towerInstance;
        }

        private ITargeting CreateTargeting(TowerDataSO config)
        {
            var targetingData = config.GetData<TowerTargetingData>();

            if (targetingData is null)
            {
                throw new System.Exception("TowerTargetingData is missing");
            }

            return new Targeting(targetingData.detectRadius, targetingData.targetLayer);
        }

        private IAimer CreateAimer(TowerDataSO config)
        {
            var aimingData = config.GetData<TowerAimingData>();

            if (aimingData == null)
            {
                throw new System.Exception("TowerAimingData is missing");
            }

            return new TowerAimer(aimingData.rotationSpeedPerSeconds);
        }

        private IShooter CreateShooter(TowerDataSO config)
        {
            var shooterData = config.GetData<TowerShooterData>();

            if (shooterData is null)
            {
                throw new System.Exception("TowerShooterData is missing");
            }
            var casterInstance = shooterData.caster.CreateInstance();

            return new TowerShooter(shooterData, casterInstance);
        }

        private IAimStrategy SelectAimStrategy(TowerDataSO config)
        {
            var projectileData = config.GetData<TowerShooterData>().projectileConfig;

            switch (projectileData)
            {
                case LinearProjectileConfig linear: return new LinerialAimStrategy();
                case ParabolaProjectileConfig parabola: return new BallisticAimStratagy();
                default:
                    throw new System.Exception("The targeting strategy is not enabled");
            }
        }

        private TowerController CreateTower(TowerDataSO config)
        {
            var towerData = config.GetData<TowerPrefabData>();

            if(towerData is null)
            {
                throw new System.Exception("TowerPrefabData is missing");
            }

            return towerData.tower;
        }

        private TowerSettings BuildConfig(TowerDataSO data)
        {
            var targeting = data.GetData<TowerTargetingData>();
            var shooter = data.GetData<TowerShooterData>();

            if (targeting == null || shooter == null)
            {
                throw new System.Exception("Missing required TowerData");
            }

            return new TowerSettings(shooter.shootInterval, targeting.detectRadius, shooter.projectileConfig.speed);
        }
    }
}