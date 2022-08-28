using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseButton : MonoBehaviour
{
    public GameObject objectToClose;

    public void CloseMenu()
    {
        objectToClose.SetActive(false);
    }
}
