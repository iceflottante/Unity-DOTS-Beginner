using System;
using UnityEngine;

namespace MBSpaceShooter.Scripts
{
    public class MBSSRotator : MonoBehaviour
    {
        public float speed;

        private void Update()
        {
            float delta = Time.deltaTime;
            transform.Rotate(0f, speed * delta, speed * delta);
        }
    }
}