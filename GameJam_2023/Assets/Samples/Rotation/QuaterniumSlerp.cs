using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameJamCore.Samples.Rotation
{

    public class QuaterniumSlerp : MonoBehaviour
    {
        public Transform target;

        private float rotationStartTime;
        private Quaternion startRotation;

        public Vector3 upwards;
        public float time;
        private void Update()
        {
            if(Time.time - rotationStartTime < time)
            {
                Quaternion targetRotation = Quaternion.LookRotation(target.position - transform.position, upwards);
                transform.rotation = Quaternion.Slerp(startRotation, targetRotation, Time.time - rotationStartTime);

            }
            else
            {
                rotationStartTime = Time.time;
                startRotation = transform.rotation;
            }
        }
    }
}



