using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameJamCore;
using HighlightPlus;
using Animancer;

namespace ScalEnigma
{
    public class Player : BasePlayer
    {

        [Space]
        [SerializeField] bool groundedPlayer = false;
        [SerializeField] float playerSpeed = 2.0f;
        [SerializeField] float jumpHeight = 1.0f;

        [Space]
        [SerializeField] float left_rot = 145;
        [SerializeField] float right_rot = 210;

        [Space]
        public ClipTransition walk;
        public ClipTransition idle;
        public ClipTransition jump;


        private void OnTriggerEnter(Collider other)
        {
            if(other.gameObject.tag == "Ground")
            {
                groundedPlayer = true;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.tag == "Ground")
            {
                groundedPlayer = false;
            }
        }

        void Update()
        {
            float hor = Input.GetAxisRaw("Horizontal");

            this.GetRigidbody().velocity = new Vector3(hor * playerSpeed, this.GetRigidbody().velocity.y, this.GetRigidbody().velocity.z);

            if(hor > 0)
            {
                this.transform.rotation = Quaternion.Euler(0, left_rot, 0);
            }
            else if(hor < 0) 
            {
                this.transform.rotation = Quaternion.Euler(0, right_rot, 0);
            }


            if (Input.GetKeyDown(KeyCode.Space) && groundedPlayer)
            {
                this.GetRigidbody().AddForce(Vector3.up * jumpHeight, ForceMode.Impulse);

            }

            if(GetRigidbody().velocity.y > 0.2 || GetRigidbody().velocity.y < -0.2)
            {
                GetAnimancer().Play(jump);
            }
            else
            {
                if(GetRigidbody().velocity.x == 0)
                {
                    GetAnimancer().Play(idle);
                }
                else
                {
                    GetAnimancer().Play(walk);
                }
            }
        }

        #region Helpers

        HighlightEffect highlight;

        public HighlightEffect GetHighlightEffect()
        {
            if(highlight == null)
            {
                highlight = GetComponent<HighlightEffect>();
            }

            return highlight;
        }

        AnimancerComponent animancer;

        public AnimancerComponent GetAnimancer()
        {
            if(animancer == null)
            {
                animancer = GetComponentInChildren<AnimancerComponent>();
            }

            return animancer;
        }

        Rigidbody rdb;

        public Rigidbody GetRigidbody()
        {
            if (rdb == null)
            {
                rdb = GetComponent<Rigidbody>();
            }

            return rdb;
        }


        #endregion

    }
}

