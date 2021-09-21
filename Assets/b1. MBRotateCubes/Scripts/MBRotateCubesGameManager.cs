using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;


namespace MBRotateCubes.Scripts
{
    public class MBRotateCubesGameManager : MonoBehaviour
    {
        public GameObject cube;

        public Vector3 boundary;

        public int batchAmount;


        private void Start()
        {
            Application.targetFrameRate = 60;
            CounterDisplay.Set(1);
        }

        private void Update()
        {
            if (Input.GetKeyUp(KeyCode.Space))
            {
                StartCoroutine(SpawnCubes(batchAmount));
            }
        }

        private IEnumerator SpawnCubes(int num)
        {
            for (int i = 0; i < num; i += 1)
            {
                var position = new Vector3(Random.Range(-boundary.x, boundary.x), Random.Range(-boundary.y, boundary.y), boundary.z);
                Instantiate(cube, position, Quaternion.identity);
                // yield return new WaitForSeconds(0);
            }
            
            CounterDisplay.Add(num);
            
            yield return new WaitForSeconds(0);
        }
    }
}
