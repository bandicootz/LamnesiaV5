using UnityEngine;

namespace Lamnesia.InGame
{
    public class Door : MonoBehaviour
    {
        [SerializeField] private int _value;
        
        public AudioClip interactSound;

        public bool CheckPair(int keyValue)
        {
            if (_value == keyValue) return true;
            else
            {
                Debug.Log("Sorry bro, wrong key");
                return false;
            }
        }

        public bool CanOpenDoor(int keyValue)
        {
            if (CheckPair(keyValue))
            {
                OpenDoor();
                return true;
            }
            return false;
        }

        private void OpenDoor()
        {
            //gameObject.SetActive(false);
            gameObject.GetComponent<Animation>().Play();
        }
    }
}