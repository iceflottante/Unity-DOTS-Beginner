using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;


namespace MBSpaceShooter.Scripts
{
    public class MBSSGameManager : MonoBehaviour
    {
        public GameObject cube;

        public Vector3 boundary;

        public int batchAmount;

        private int cubeAmount = 1;

        public Text cubeAmountText;

        private void Start()
        {
            Application.targetFrameRate = 60;
        }

        void Update()
        {
            if (Input.GetKeyUp(KeyCode.Space))
            {
                StartCoroutine(SpawnCube((batchAmount)));
            }
        }

        private IEnumerator SpawnCube(int num)
        {
            for (int i = 0; i < num; i += 1)
            {
                Vector3 spawnPosition = new Vector3(Random.Range(-boundary.x, boundary.x), Random.Range(-boundary.y, boundary.y), boundary.z);
                Instantiate(cube, spawnPosition, Quaternion.identity);
                // yield return new WaitForSeconds(0);
            }
            
            AddCubeAmount(num);
            
            yield return new WaitForSeconds(0);
        }

        private void AddCubeAmount(int num)
        {
            cubeAmount += num;
            cubeAmountText.text = cubeAmount.ToString();
        }
    }
}
