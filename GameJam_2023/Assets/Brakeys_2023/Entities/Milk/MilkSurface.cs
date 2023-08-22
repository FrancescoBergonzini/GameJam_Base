using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

namespace GameJamCore.Brakeys_2023
{
    public class MilkSurface : GameEntity
    {
        [SerializeField] float forceDamping = 1;
        [SerializeField] float springConstant = 1;

        float currentRotation;
        float velocity;

        private void Start()
        {
            AddForce(5, 1);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="force"></param>
        /// <param name="direction">Direzione nella quale muovere il liquido, -1 verso sinistra, 1 verso destra</param>
        public void AddForce(float force, float direction)
        {
            currentRotation += force;
        }

        private void Update()
        {
            var displacement = 0 - currentRotation;
            var springForce = springConstant * displacement;

            // Calculate damping force
            var dampingForce = -forceDamping * velocity;

            // Calculate total force
            var totalForce = springForce + dampingForce;

            // Apply force using Euler integration
            float deltaTime = Time.deltaTime;
            velocity += (totalForce * deltaTime);
            currentRotation += (velocity * deltaTime);

            // Update object's position
            transform.eulerAngles = new Vector3(0,0,currentRotation);
        }
    }
}
