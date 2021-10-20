using Unity.Entities;
using UnityEngine;

namespace PERotateCubes.Scripts
{
    public class PERotateCubeSpawnerCreateSystem : SystemBase
    {
        protected override void OnUpdate()
        {
            if (Input.GetKeyUp(KeyCode.Space))
            {
                Debug.Log("Key Up");
            }
        }
    }
}