using UnityEngine;

namespace Lamnesia.InGame
{
    public class Key : MonoBehaviour
    {
        [SerializeField] private int valueToPair;

        public bool CanOpen(GameObject door)
        {
            if (door.GetComponent<Door>().CanOpenDoor(valueToPair)) return true;
            else return false;
        }
    }
}