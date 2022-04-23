using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interaction : MonoBehaviour
{
    [SerializeField] private UnityEvent interactAction;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            interactAction.Invoke();
        }
    }
}