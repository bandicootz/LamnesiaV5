using UnityEngine;

namespace Lamnesia.InGame
{
    public class Key : MonoBehaviour
    {
        [SerializeField] private string valueToPair;

        public bool CanOpen(GameObject door)
        {
            if (door.GetComponent<Door>().canOpenDoor(valueToPair)) return true;
            else return false;
        }
    }
}