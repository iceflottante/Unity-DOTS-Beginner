using System;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

namespace PERotateCubes.Scripts
{
    public struct PERotateCubesSpawnerComponent : IComponentData
    {
        public int Amount;
        public Vector3 Boundary;
        public Entity Prefab;
    }
    
    public class PERotateCubesSpawnerSystem : SystemBase
    {
        private BeginInitializationEntityCommandBufferSystem entityCommandBufferSystem;
        
        protected override void OnCreate()
        {
            entityCommandBufferSystem = World.GetOrCreateSystem<BeginInitializationEntityCommandBufferSystem>();
        }

        protected override void OnUpdate()
        {
            var commandBuffer = entityCommandBufferSystem.CreateCommandBuffer()
                .AsParallelWriter();
            
            Entities
                .ForEach((Entity entity, int entityInQueryIndex, in PERotateCubesSpawnerComponent spawner, in LocalToWorld location) =>
                {
                    for (var x = 0; x < spawner.Amount; x++)
                    {
                        // 创建新的 entity
                        var instance = commandBuffer.Instantiate(entityInQueryIndex, spawner.Prefab);

                        commandBuffer.SetComponent(
                            entityInQueryIndex,
                            instance,
                            new Translation
                            {
                                Value = math.transform(
                                    location.Value,
                                    new float3(
                                        noise.cnoise(new float2(-spawner.Boundary.x, spawner.Boundary.x)),
                                        noise.cnoise(new float2(-spawner.Boundary.y, spawner.Boundary.y)),
                                        spawner.Boundary.z
                                    )
                                )
                            }
                        );

                        commandBuffer.SetComponent(entityInQueryIndex, instance, new PERotateCubeComponent { Speed = noise.cnoise(new float2(0.1f, 2.0f)) });
                    }
                    
                    // Destroy this entity?
                })
                .ScheduleParallel();
        }
    }
}
