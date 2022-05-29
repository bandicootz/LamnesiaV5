using UnityEngine;

namespace Lamnesia.InGame
{
    public class Door : MonoBehaviour
    {
        [SerializeField] private int _value;

        public bool CheckPair(int keyValue)
        {
            if (_value == keyValue) return true;
            else
            {
                Debug.Log("Sorry bro, wrong key");
                return false;
            }
        }

        public bool canOpenDoor(int keyValue)
        {
            if (CheckPair(keyValue))
            {
                OpenDoor();
                Debug.Log("You can open door");
                return true;
            }
            else
            {
                Debug.Log("You can't open a door with this key");
                return false;
            }
        }

        private void OpenDoor()
        {
            gameObject.SetActive(false);
        }
    }
}