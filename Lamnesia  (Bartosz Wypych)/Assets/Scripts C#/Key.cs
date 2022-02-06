using UnityEngine;

public class Key : MonoBehaviour
{
    [SerializeField]
    private string valueToPair;

    public bool canOpen(GameObject door)
    {
        if (door.GetComponent<Door>().canOpenDoor(valueToPair)) return true;
        else return false;
    }
}
