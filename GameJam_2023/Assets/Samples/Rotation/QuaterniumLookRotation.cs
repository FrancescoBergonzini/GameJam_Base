using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameJamCore.Samples.Rotation
{
    public class QuaterniumLookRotation : MonoBehaviour
    {
        [SerializeField] Transform target;
        [SerializeField] Vector3 upwards;
        private void Update()
        {
            transform.rotation = Quaternion.LookRotation(target.position - transform.position, upwards);

            //alternativa
            //transform.LookAt(target, worldUp: upwards);

        }
    }
}
