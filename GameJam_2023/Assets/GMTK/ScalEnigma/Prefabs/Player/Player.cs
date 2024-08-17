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
        private CharacterController controller;
        private Vector3 playerVelocity;

        [Space]
        [SerializeField] bool groundedPlayer;
        [SerializeField] float playerSpeed = 2.0f;
        [SerializeField] float jumpHeight = 1.0f;
        [SerializeField] float gravityValue = -9.81f;

        private void Awake()
        {
            controller = GetComponent<CharacterController>();
        }

        void Update()
        {
            groundedPlayer = controller.isGrounded;
            if (groundedPlayer && playerVelocity.y < 0)
            {
                playerVelocity.y = 0f;
            }

            Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            controller.Move(move * Time.deltaTime * playerSpeed);

            if (move != Vector3.zero)
            {
                gameObject.transform.forward = move;
            }

            // Changes the height position of the player..
            if (Input.GetButtonDown("Jump") && groundedPlayer)
            {
                playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
            }

            playerVelocity.y += gravityValue * Time.deltaTime;
            controller.Move(playerVelocity * Time.deltaTime);
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

