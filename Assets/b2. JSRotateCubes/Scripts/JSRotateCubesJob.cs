using Unity.Collections;
using Unity.Jobs;
using UnityEngine;
using UnityEngine.Jobs;

namespace JSRotateCubes.Scripts
{
    public struct JSRotateCubesJob : IJobParallelForTransform
    {
        [ReadOnly]
        public float speed;

        [ReadOnly]
        public float delta;

        // public NativeArray<Quaternion> results;

        public void Execute(int index, TransformAccess trans)
        {
            trans.rotation = trans.rotation * Quaternion.Euler(0, speed * delta, speed * delta);
        }
    }
}