using Unity.Collections;
using Unity.Jobs;
using UnityEngine;

namespace HelloJobSystem.Scripts
{
    public class HelloJobSystemEntry : MonoBehaviour
    {
        private void Start()
        {
            var job = new HelloJobSystemJob
            {
                a = 1,
                b = 2,
                results = new NativeArray<int>(1, Allocator.TempJob),
            };
            
            Debug.Log(job.results[0]);

            JobHandle handle = job.Schedule();
            
            handle.Complete();
            
            Debug.Log(job.results[0]);

            job.results.Dispose();
        }
    }
}