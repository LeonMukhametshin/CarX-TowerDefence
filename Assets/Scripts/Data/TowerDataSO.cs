using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CarXTowerDefence.Gameplay.Data
{
    [CreateAssetMenu(fileName = "New Tower Data", menuName = "Scriptable Objects/Tower/ DATA")]
    public class TowerDataSO : ScriptableObject
    {
        [field: SerializeReference] public List<TowerComponentData> componentDatas { get; private set; }

        public T GetData<T>() =>
            componentDatas.OfType<T>().FirstOrDefault();

        public void AddData(TowerComponentData data)
        {
            if (componentDatas.FirstOrDefault(t => t.GetType() == data.GetType()) != null)
            {
                return;
            }

            componentDatas.Add(data);
        }
    }
}