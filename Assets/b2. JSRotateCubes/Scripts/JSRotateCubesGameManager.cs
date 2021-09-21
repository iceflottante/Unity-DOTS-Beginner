using System.Collections.Generic;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine;
using UnityEngine.Jobs;
using Random = UnityEngine.Random;

namespace JSRotateCubes.Scripts
{
    public class JSRotateCubesGameManager : MonoBehaviour
    {
        public GameObject cube;
        
        public Vector3 boundary;

        public int batchAmount;

        private readonly List<Transform> transforms = new List<Transform>();
        
        private void Start()
        {
            Application.targetFrameRate = 60;
            transforms.Add(GameObject.Find("SimpleCube").transform);
            CounterDisplay.Set(1);
        }

        private void Update()
        {
            if (Input.GetKeyUp(KeyCode.Space))
            {
                SpawnCubes(batchAmount);
            }
            
            ExecuteJobs(Time.deltaTime, CounterDisplay.Count);
        }

        private void SpawnCubes(int num)
        {
            for (int i = 0; i < num; i += 1)
            {
                var position = new Vector3(Random.Range(-boundary.x, boundary.x), Random.Range(-boundary.y, boundary.y), boundary.z);
                var target = Instantiate(cube, position, Quaternion.identity);
                transforms.Add(target.transform);
            }
            CounterDisplay.Add(num);
        }

        private void ExecuteJobs(float deltaTime, int len)
        {
            // var results = new NativeArray<Quaternion>(len, Allocator.TempJob);

            JSRotateCubesJob job = new JSRotateCubesJob
            {
                delta = deltaTime,
                speed = 100f,
                // results = results,
            };

            var accesses = new TransformAccessArray(transforms.Count);
            accesses.SetTransforms(transforms.ToArray());
            var handle = job.Schedule(accesses);

            handle.Complete();
            // results.Dispose();
            accesses.Dispose();
        }
    }
}
