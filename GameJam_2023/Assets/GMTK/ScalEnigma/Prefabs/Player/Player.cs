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
        public ClipTransition walk;
        public ClipTransition idle;
        public ClipTransition jump;

        [Space]
        public Rigidbody _rdb;


        private void Awake()
        {
            _rdb = GetComponent<Rigidbody>();
        }


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
            float hor = Input.GetAxis("Horizontal");

            Debug.Log(hor);

            this._rdb.velocity = new Vector3(hor * playerSpeed, 0, 0);
        }

        #region Helpers

        public HighlightEffect highlight;

        public HighlightEffect GetHighlightEffect()
        {
            if(highlight == null)
            {
                highlight = GetComponent<HighlightEffect>();
            }

            return highlight;
        }

        public AnimancerComponent animancer;

        public AnimancerComponent GetAnimancer()
        {
            if(animancer == null)
            {
                animancer = GetComponentInChildren<AnimancerComponent>();
            }

            return animancer;
        }


        #endregion

    }
}

