using System;
using Unity.Burst;
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
                .WithBurst(FloatMode.Default, FloatPrecision.Standard, true)
                .ForEach((Entity entity, int entityInQueryIndex, in PERotateCubesSpawnerComponent spawner, in LocalToWorld location) =>
                {
                    var random = new Unity.Mathematics.Random(1);
                    for (var x = 0; x < spawner.Amount; x++)
                    {
                        // 创建新的 entity
                        var instance = commandBuffer.Instantiate(entityInQueryIndex, spawner.Prefab);
                        var position = math.transform(location.Value,
                            new float3(random.NextFloat(-spawner.Boundary.x, spawner.Boundary.x),
                                random.NextFloat(-spawner.Boundary.y, spawner.Boundary.y), spawner.Boundary.z));

                        commandBuffer.SetComponent( entityInQueryIndex, instance, new Translation { Value = position } );
                        commandBuffer.SetComponent(entityInQueryIndex, instance, new PERotateCubeComponent { Speed = random.NextFloat(0.1f, 2.0f) });
                    }

                    // Destroy this entity?
                    // 创建完立刻销毁此 Entity，不然似乎会多次执行创建 1000 次，可以注释这行就能看到问题
                    commandBuffer.DestroyEntity(entityInQueryIndex, entity);
                })
                .ScheduleParallel();
            
            // NOTE:
            // ArgumentException: The previously scheduled job PERotateCubesSpawnerSystem:OnUpdate_LambdaJob0 writes to the Unity.Entities.EntityCommandBuffer OnUpdate_LambdaJob0.JobData.commandBuffer.
            // You must call JobHandle.Complete() on the job PERotateCubesSpawnerSystem:OnUpdate_LambdaJob0, before you can write to the Unity.Entities.EntityCommandBuffer safely.
            entityCommandBufferSystem.AddJobHandleForProducer(Dependency);
        }
    }
}
