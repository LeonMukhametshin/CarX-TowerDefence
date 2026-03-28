using CarXTowerDefence.Gameplay.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;

namespace CarXTowerDefence
{
    [CustomEditor(typeof(TowerDataSO))]
    public class TowerConfigEditor : Editor
    {
        private static List<Type> dataConfigTypes = new();

        private TowerDataSO dataSO;

        private void OnEnable()
        {
            dataSO = target as TowerDataSO;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            foreach (var item in dataConfigTypes)
            {
                if (GUILayout.Button(item.Name))
                {
                    var component = (TowerComponentData)Activator.CreateInstance(item);

                    if(component is null)
                    {
                        return;
                    }

                    dataSO.AddData(component);
                }
            }
        }

        [DidReloadScripts]
        private static void OnRecompile()
        {
            var assemlies = AppDomain.CurrentDomain.GetAssemblies();
            var types = assemlies.SelectMany(assembly => assembly.GetTypes());
            var filteredTipes = types.Where(
                type => type.IsSubclassOf(typeof(TowerComponentData))
                && !type.ContainsGenericParameters
                && type.IsClass);

            dataConfigTypes = filteredTipes.ToList();
        }
    }
}