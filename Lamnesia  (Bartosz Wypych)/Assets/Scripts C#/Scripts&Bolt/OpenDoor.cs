using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    public Animation doorhere;
    void OnTriggerStay()
    {
        if (Input.GetKey(KeyCode.E))
            doorhere.Play();
    }
}
