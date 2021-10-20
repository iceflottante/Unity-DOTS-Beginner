using System;
using System.Threading;
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
        public uint Times;
    }
    
    public class PERotateCubesSpawnerSystem : SystemBase
    {
        private BeginInitializationEntityCommandBufferSystem entityCommandBufferSystem;
        
        // 这个写在这里会导致随机数不对……
        // private static readonly Unity.Mathematics.Random random = new Unity.Mathematics.Random(1);
        
        protected override void OnCreate()
        {
            entityCommandBufferSystem = World.GetOrCreateSystem<BeginInitializationEntityCommandBufferSystem>();
        }

        protected override void OnUpdate()
        {
            if (!Input.GetKeyUp(KeyCode.Space))
            {
                return;
            }

            var commandBuffer = entityCommandBufferSystem.CreateCommandBuffer()
                .AsParallelWriter();

            Entities
                .WithBurst(FloatMode.Default, FloatPrecision.Standard, true)
                .ForEach((Entity entity, int entityInQueryIndex, in PERotateCubesSpawnerComponent spawner,
                    in LocalToWorld location) =>
                {
                    var random = new Unity.Mathematics.Random(spawner.Times);
                    
                    for (var x = 0; x < spawner.Amount; x++)
                    {
                        // 创建新的 entity
                        var instance = commandBuffer.Instantiate(entityInQueryIndex, spawner.Prefab);
                        var position = math.transform(location.Value,
                            new float3(random.NextFloat(-spawner.Boundary.x, spawner.Boundary.x),
                                random.NextFloat(-spawner.Boundary.y, spawner.Boundary.y), spawner.Boundary.z));

                        commandBuffer.SetComponent(entityInQueryIndex, instance,
                            new Translation { Value = position });
                        commandBuffer.SetComponent(entityInQueryIndex, instance,
                            new PERotateCubeComponent { Speed = random.NextFloat(1.0f, 3.0f) });
                        // commandBuffer.SetComponent(entityInQueryIndex, instance, new PERotateCubeComponent { Speed = 2.0f });
                    }

                    // Destroy this entity?
                    // 创建完立刻销毁此 Entity，不然 OnUpdate 会多次执行，从而多次创建 1000 次，可以注释这行就能看到问题
                    // commandBuffer.DestroyEntity(entityInQueryIndex, entity);
                    
                    // 为下一轮做准备
                    commandBuffer.SetComponent(entityInQueryIndex, entity, new PERotateCubesSpawnerComponent { Amount = spawner.Amount, Boundary = spawner.Boundary, Prefab = spawner.Prefab, Times = spawner.Times + 1 });
                })
                .ScheduleParallel();
            
            // NOTE:
            // ArgumentException: The previously scheduled job PERotateCubesSpawnerSystem:OnUpdate_LambdaJob0 writes to the Unity.Entities.EntityCommandBuffer OnUpdate_LambdaJob0.JobData.commandBuffer.
            // You must call JobHandle.Complete() on the job PERotateCubesSpawnerSystem:OnUpdate_LambdaJob0, before you can write to the Unity.Entities.EntityCommandBuffer safely.
            entityCommandBufferSystem.AddJobHandleForProducer(Dependency);
            
            CounterDisplay.Add(1000);
        }
    }
}
