using System;
using Unity.Entities;
using UnityEngine;

namespace HelloPureEcs.Scripts
{
    public class PEHelloCubeAuthoring : MonoBehaviour, IConvertGameObjectToEntity
    {
        public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
        {
            // initialize
            dstManager.AddComponentData(entity, new RotationSpeedComponent { Speed = UnityEngine.Random.Range(0.1f, 2.0f) });
        }
    }
}