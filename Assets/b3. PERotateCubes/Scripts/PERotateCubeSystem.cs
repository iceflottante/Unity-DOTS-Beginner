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
        protected override void OnUpdate()
        {
            Entities
                .ForEach((ref Rotation rotation, in PERotateCubeComponent customize) =>
                {
                    rotation.Value = math.mul(math.normalize(rotation.Value), quaternion.AxisAngle(math.up(), customize.Speed));
                })
                .ScheduleParallel();
        }
    }
}