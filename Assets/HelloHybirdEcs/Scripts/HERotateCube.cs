using System;
using Unity.Entities;
using UnityEngine;

namespace HelloHybirdEcs.Scripts
{
    public class HERotateCube: MonoBehaviour
    {
        public float speed;
    }

    
    class RotatorSystem : ComponentSystem
    {
        protected override void OnUpdate()
        {
            float delta = Time.DeltaTime;

            Entities
                .WithAll<Transform, HERotateCube>()
                .ForEach((Entity entity, Transform transform, HERotateCube rotator) =>
                {
                    transform.Rotate(0f, rotator.speed * delta, 0f);
                });

        }
    }
}