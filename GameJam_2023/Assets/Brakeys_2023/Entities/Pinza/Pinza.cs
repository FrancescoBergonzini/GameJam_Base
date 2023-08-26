using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameJamCore.Brakeys_2023
{
    public class Pinza : GameEntity
    {
        [SerializeField] Rigidbody2D _rdb;

        [Header("Horizontal state")]
        [SerializeField] float horizontalSpeed = 1f;
        [SerializeField] float horizontalStateY;
        [SerializeField] Vector2 minMaxX;

        [Space, Header("Going down state")]
        [SerializeField] LayerMask raycastMask;
        [SerializeField] float verticalSpeed = 1f;
        [SerializeField] float sphereCastCheckRadius = 0.1f;
        [SerializeField] float stopOffset = 0.1f;
        Tween activeMoveDownTween = null;

        [SerializeField] float cookiesCheckRadius;
        [SerializeField] LayerMask layerBiscotti;
        [SerializeField] Transform cookiesCheckTransform;

        [SerializeField] Transform leftClaw;
        [SerializeField] Transform rightClaw;
        [SerializeField] float clawArmsCloseAngle;
        [SerializeField] float clawArmsOpenAngle;

        List<Biscotto> grabbedCookies = new List<Biscotto>();


        float horizontalProgress = 0.5f;
        float horizontalDirection = 1;

        public enum PinzaState
        {
            horizontalMovement,
            goingDown,
            goingUp,
        }

        PinzaState currentState = PinzaState.horizontalMovement;

        bool PlayerInput => Input.anyKeyDown || Input.GetMouseButtonDown(0);

        // Start is called before the first frame update
        void Start()
        {
            ToggleClawArms(true);
        }

        // Update is called once per frame
        void Update()
        {
            switch (currentState)
            {
                case PinzaState.horizontalMovement:
                    HorizontalMovement();
                    break;
                case PinzaState.goingDown:
                    MovingDown();
                    break;
                case PinzaState.goingUp:

                    break;
            }
        }
        private void HorizontalMovement()
        {
            if (PlayerInput)
            {
                currentState = PinzaState.goingDown;
                GoDown();
                return;
            }

            horizontalProgress += horizontalDirection * Time.deltaTime * horizontalSpeed;

            var ease = -(Mathf.Cos(Mathf.PI * horizontalProgress) - 1) / 2;
            var xPosition = Mathf.Lerp(minMaxX.x, minMaxX.y, ease);

            transform.position = new Vector2(xPosition, horizontalStateY);

            if (horizontalProgress >= 1 || horizontalProgress <= 0)
            {
                horizontalProgress = Mathf.Clamp(horizontalProgress, 0, 1);
                horizontalDirection *= -1;
            }
        }

        private void MovingDown()
        {
            if (PlayerInput)
            {
                StopMovingDown();
            }
        }

        private void StopMovingDown()
        {
            ToggleClawArms(false);

            CheckCookiesInside();
            currentState = PinzaState.goingUp;
            if (activeMoveDownTween.active) activeMoveDownTween.Kill();
            _rdb.DOMoveY(horizontalStateY, Mathf.Abs(transform.position.y - horizontalStateY) / verticalSpeed).
                OnComplete(() =>
                {
                    ProcessCookiesInside();
                    ToggleClawArms(true);
                    currentState = PinzaState.horizontalMovement;
                }).
                SetEase(Ease.Linear);
        }

        void ToggleClawArms(bool open)
        {
            var angle = open ? clawArmsOpenAngle : clawArmsCloseAngle;
            leftClaw.DOLocalRotate(new Vector3(0, 0, -angle), .5f);
            rightClaw.DOLocalRotate(new Vector3(0, 0, angle), .5f);
        }

        private void CheckCookiesInside()
        {
            List<Collider2D> cookies = new List<Collider2D>();
            ContactFilter2D contactFilter = new ContactFilter2D()
            {
                layerMask = layerBiscotti,
                useLayerMask = true
            };
            var cookiesCount = Physics2D.OverlapCircle(cookiesCheckTransform.position, cookiesCheckRadius, contactFilter, cookies);
            Debug.Log(cookiesCount);
            if (cookiesCount > 0)
            {
                foreach (var cookie in cookies)
                {
                    if (cookie.TryGetComponent(out Biscotto biscotto))
                    {
                        grabbedCookies.Add(biscotto);
                        biscotto.Grab(this);
                    }
                }
            }
        }

        private void ProcessCookiesInside()
        {
            if (grabbedCookies.Count <= 0) return;

            foreach (var cookie in grabbedCookies)
            {
                GameManager.instance.current_raw_score += cookie.ConvertIntegrityToScore();
                cookie.ProcessGrab();
            }
            grabbedCookies.Clear();
        }

        private void GoDown()
        {
            List<RaycastHit2D> hits = new List<RaycastHit2D>();
            ContactFilter2D contactFilter = new ContactFilter2D()
            {
                useLayerMask = true,
                layerMask = raycastMask
            };

            var castResult = Physics2D.CircleCast(transform.position, sphereCastCheckRadius, Vector2.down, contactFilter, hits);
            if (castResult > 0)
            {
                activeMoveDownTween = _rdb.DOMoveY(hits[0].point.y + stopOffset, hits[0].distance / verticalSpeed)
                    .OnComplete(StopMovingDown).SetEase(Ease.Linear);
            }
        }


        public void KillMe()
        {
            StopAllCoroutines();
            DOTween.KillAll();

            Destroy(this.gameObject);
        }

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            List<RaycastHit2D> hit = new List<RaycastHit2D>();
            ContactFilter2D contactFilter = new ContactFilter2D()
            {
                useLayerMask = true,
                layerMask = raycastMask
            };
            var castResult = Physics2D.CircleCast(transform.position, sphereCastCheckRadius, Vector2.down, contactFilter, hit);
            if (castResult > 0)
            {
                Gizmos.DrawWireSphere(hit[0].point, sphereCastCheckRadius);
            }

            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(cookiesCheckTransform.position, cookiesCheckRadius);
        }
#endif
    }
}