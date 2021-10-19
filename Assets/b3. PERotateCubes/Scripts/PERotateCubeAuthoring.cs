using System;
using Unity.Entities;
using UnityEngine;

namespace PERotateCubes.Scripts
{
    public class PERotateCubeAuthoring : MonoBehaviour, IConvertGameObjectToEntity
    {
        public void Convert(Entity entity, EntityManager entityManager, GameObjectConversionSystem conversionSystem)
        {
            entityManager.AddComponentData(entity, new PERotateCubeComponent { Speed = 0.0f });
        }
    }
}