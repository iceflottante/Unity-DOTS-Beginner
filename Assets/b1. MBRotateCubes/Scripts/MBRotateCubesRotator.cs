using UnityEngine;

namespace MBRotateCubes.Scripts
{
    public class MBRotateCubesRotator : MonoBehaviour
    {
        public float speed;

        private void Update()
        {
            float delta = Time.deltaTime;
            transform.Rotate(0f, speed * delta, speed * delta);
        }
    }
}