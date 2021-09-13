using UnityEngine;

namespace MBRotateMultipleCubes.Scripts
{
    public class MBRotateMultipleCubesRotator : MonoBehaviour
    {
        public float speed;

        private void Update()
        {
            float delta = Time.deltaTime;
            transform.Rotate(0f, speed * delta, speed * delta);
        }
    }
}