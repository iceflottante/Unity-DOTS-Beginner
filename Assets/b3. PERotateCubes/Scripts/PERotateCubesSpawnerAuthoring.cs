using System;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

namespace PERotateCubes.Scripts
{
    public class PERotateCubesSpawnerAuthoring : MonoBehaviour, IConvertGameObjectToEntity, IDeclareReferencedPrefabs
    {
        public GameObject Prefab;

        public Vector3 Boundary;
        
        public void DeclareReferencedPrefabs(List<GameObject> lists)
        {
            lists.Add(Prefab);
        }

        public void Convert(Entity entity, EntityManager entityManager, GameObjectConversionSystem conversionSystem)
        {
            entityManager.AddComponentData(
                entity,
                new PERotateCubesSpawnerComponent {
                    Amount = 1000,
                    Prefab = conversionSystem.GetPrimaryEntity(Prefab),
                    Boundary = Boundary,
                    Times = 1,
                }
            );
        }
    }
}