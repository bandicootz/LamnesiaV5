using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    public Animation doorhere;
    void OnTriggerStay(Collider other)
    {
        if (Input.GetKey(KeyCode.E))
            doorhere.Play();
    }
}
