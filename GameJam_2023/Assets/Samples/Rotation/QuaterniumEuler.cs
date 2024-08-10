using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameJamCore.Samples.Rotation
{
    public class QuaterniumEuler : MonoBehaviour
    {
        //Questo codice allinea la rotazione con la direzione di movimento.
        //Usa gli Euleri per creare nuove rotazioni, mai per modificarne di esistenti.

        public void Update()
        {
            float moveX = Input.GetAxisRaw("Horizontal");
            float moveY = Input.GetAxisRaw("Vertical");

            Vector2 movement = new Vector2(moveX, moveY);

            movement.Normalize();

            this.GetComponent<Rigidbody>().velocity = movement;

            if (movement != Vector2.zero)
            {
                float angle = Mathf.Atan2(-movement.x, movement.y) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Euler(0, 0, angle);
            }
        }
    }
}
