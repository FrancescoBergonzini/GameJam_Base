using DG.Tweening;
using GameJamCore;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ScalEnigma
{
    public class Door : MonoBehaviour
    {
        public bool Open;

        [SerializeField]
        public Transform door_frame;
        public float target_rot = 145f;
        public float duration = 1f;

        [Space]
        public SoundData open_sfx;


        private void OnTriggerStay(Collider other)
        {
            if(other.gameObject.layer == Layers.Player)
            {
                //check player size...
                if (other.GetComponent<Player>().size != GetObjectDoor().current_size)
                    return;

                if (!Open)
                {
                    if (DoorCr != null)
                        return;

                    DoorCr = StartCoroutine(OpenDoor());

                }

            }
        }



        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.layer == Layers.Player)
            {
                if (Open)
                {
                    if (DoorCr != null)
                        DOTween.Kill(DoorCr);

                    DoorCr = StartCoroutine(CloseDoor());
                }
            }
        }

        Coroutine DoorCr = null;
        IEnumerator OpenDoor()
        {
            GameManagerBase.Instance.PlaySound(open_sfx);
            door_frame.DORotate(new Vector3(0,target_rot,0), duration);

            yield return new WaitForSeconds(duration);

            Open = true;
            DoorCr = null;
        }

        IEnumerator CloseDoor()
        {
            door_frame.DORotate(new Vector3(0, 0, 0), duration);

            yield return new WaitForSeconds(duration);

            Open = false;
            DoorCr = null;
        }


        public BaseObject objectDoor;
        public BaseObject GetObjectDoor()
        {
            if(objectDoor == null)
            {
                objectDoor = GetComponent<BaseObject>();    
            }

            return objectDoor;
        }



    }
}

