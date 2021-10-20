using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace PERotateCubes.Scripts
{
    public struct PERotateCubeComponent : IComponentData
    {
        public float Speed;
    }
    
    public class PERotateCubeSystem : SystemBase
    {
        private static readonly float3 axisDirection = new float3(1.0f, 1.0f, 0.0f);
        
        protected override void OnUpdate()
        {
            var deltaTime = Time.DeltaTime;
            
            Entities
                .ForEach((ref Rotation rotation, in PERotateCubeComponent customize) =>
                {
                    rotation.Value = math.mul(math.normalize(rotation.Value), quaternion.AxisAngle(axisDirection, customize.Speed * deltaTime ));
                })
                .ScheduleParallel();
        }
    }
}